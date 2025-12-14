using RepositoryLayer.AppDbContext;
using RepositoryLayer.Entity;
using RepositoryLayer.RepoInterFace;
using RepositoryLayer.RepositoryInterface;
using ServiceLayer.DTO;
using ServiceLayer.DTO.RequestDTO;
using ServiceLayer.ServiceInterFace;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceLayer.Service
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IAuthRepository _authRepo;
        private readonly IEmailService _email;

        public StudentService(
            IStudentRepository studentRepository,
            IAuthRepository authRepo,
            IEmailService email)
        {
            _studentRepository = studentRepository;
            _authRepo = authRepo;
            _email = email;
        }

        public async Task<Student> AddStudentAsync(StudentRequestDTO dto)
        {
            var (user, password) =
                await _authRepo.CreateUserAuto(dto.Email, "Student");

            var student = new Student
            {
                StudentName = dto.StudentName,
                PhoneNo = dto.PhoneNo,
                Address = dto.Address,
                Email = dto.Email,
                ClassID = dto.ClassID,
                UserID = user.UserID
            };

            await _studentRepository.AddStudent(student); // ✅

            await _email.SendEmailAsync(new EmailDTO
            {
                To = dto.Email,
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

    }
}
