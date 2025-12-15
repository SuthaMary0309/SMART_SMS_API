using Microsoft.AspNetCore.Http;
using RepositoryLayer.AppDbContext;
using RepositoryLayer.Entity;
using RepositoryLayer.RepoInterFace;
using RepositoryLayer.RepositoryInterface;
using ServiceLayer.DTO;
using ServiceLayer.DTO.RequestDTO;
using ServiceLayer.ServiceInterFace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceLayer.Service
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IAuthRepository _authRepo;
        private readonly IEmailService _email;
        private readonly ICloudinaryService _cloudinaryService;

        public StudentService(
            IStudentRepository studentRepository,
            IAuthRepository authRepo,
            IEmailService email,
            ICloudinaryService cloudinaryService)
        {
            _studentRepository = studentRepository;
            _authRepo = authRepo;
            _email = email;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<Student> AddStudentAsync(StudentRequestDTO request, IFormFile? profileImage = null)
        {
            var studentId = Guid.NewGuid();
            string? profileUrl = null;
            string? profileImagePublicId = null;

            // Upload profile image to Cloudinary if provided
            if (profileImage != null)
            {
                var allowedTypes = new[] { "image/jpeg", "image/png", "image/webp", "image/jpg" };
                if (!allowedTypes.Contains(profileImage.ContentType.ToLower()))
                {
                    throw new ArgumentException("Invalid image type. Only JPEG, PNG, and WebP are allowed.");
                }

                var publicId = $"students/{studentId}-{Guid.NewGuid():N}";
                var uploadResult = await _cloudinaryService.UploadImageAsync(profileImage, publicId, 600, 600);
                
                if (uploadResult.Error != null)
                {
                    throw new Exception($"Failed to upload image: {uploadResult.Error.Message}");
                }

                profileUrl = uploadResult.SecureUrl?.ToString();
                profileImagePublicId = uploadResult.PublicId;
            }

            // Create user account for the student
            var (user, password) = await _authRepo.CreateUserAuto(request.Email, "Student");

            var student = new Student
            {
                StudentID = studentId,
                StudentName = request.StudentName,
                PhoneNo = request.PhoneNo,
                Address = request.Address,
                Email = request.Email,
                ClassID = request.ClassID,
                ProfileURL = profileUrl,
                ProfileImagePublicId = profileImagePublicId,
                UserID = user.UserID
            };
            await _studentRepository.AddStudent(student);

            // Send email with login credentials
            await _email.SendEmailAsync(new EmailDTO
            {
                To = request.Email,
                Subject = "Student Login",
                Body = $"Username: {user.UserName}, Password: {password}"
            });


            return student;
        }




        public async Task<Student?> UpdateStudentAsync(Guid id, StudentRequestDTO request)
        {
            var existing = await _studentRepository.GetStudentById(id);
            if (existing == null) return null;

            existing.StudentName = request.StudentName;
            existing.PhoneNo = request.PhoneNo;
            existing.Address = request.Address;
            existing.Email = request.Email;
            existing.ClassID = request.ClassID;
            

            return await _studentRepository.UpdateStudent(existing);
        }

        public async Task<bool> DeleteStudentAsync(Guid id)
        {
            return await _studentRepository.DeleteStudent(id);
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            // Fetch all students from repository
            var students = await _studentRepository.GetAllStudents();
            return students;
        }


        public async Task<Student?> GetStudentByIdAsync(Guid id)
        {
            return await _studentRepository.GetStudentById(id);
        }


        public async Task<Student?> UpdateProfileImageAsync(Guid id, string profileUrl, string publicId)
        {
            return await _studentRepository.UpdateProfileImage(id, profileUrl, publicId);
        }

        public async Task<Student?> UpdateProfileImageWithFileAsync(Guid id, IFormFile profileImage)
        {
            var existing = await _studentRepository.GetStudentById(id);
            if (existing == null) return null;

            // Validate image type
            var allowedTypes = new[] { "image/jpeg", "image/png", "image/webp", "image/jpg" };
            if (!allowedTypes.Contains(profileImage.ContentType.ToLower()))
            {
                throw new ArgumentException("Invalid image type. Only JPEG, PNG, and WebP are allowed.");
            }

            // Delete old image from Cloudinary if exists
            if (!string.IsNullOrEmpty(existing.ProfileImagePublicId))
            {
                await _cloudinaryService.DeleteImageAsync(existing.ProfileImagePublicId);
            }

            // Upload new image to Cloudinary
            var publicId = $"students/{id}-{Guid.NewGuid():N}";
            var uploadResult = await _cloudinaryService.UploadImageAsync(profileImage, publicId, 600, 600);

            if (uploadResult.Error != null)
            {
                throw new Exception($"Failed to upload image: {uploadResult.Error.Message}");
            }

            // Update student with new image URL
            return await _studentRepository.UpdateProfileImage(
                id,
                uploadResult.SecureUrl?.ToString() ?? string.Empty,
                uploadResult.PublicId
            );
        }

        public async Task<string?> GetProfileImageUrlAsync(Guid id)
        {
            var student = await _studentRepository.GetStudentById(id);
            return student?.ProfileURL;
        }
    }
}
