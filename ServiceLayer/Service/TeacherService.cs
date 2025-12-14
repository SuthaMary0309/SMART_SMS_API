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
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IAuthRepository _authRepo;
        private readonly IEmailService _email;

        public TeacherService(
            ITeacherRepository teacherRepository,
            IAuthRepository authRepo,
            IEmailService email)
        {
            _teacherRepository = teacherRepository;
            _authRepo = authRepo;
            _email = email;
        }

        public async Task<Teacher> AddTeacherAsync(TeacherRequestDTO dto)
        {
            var (user, password) =
                await _authRepo.CreateUserAuto(dto.Email, "Teacher");

            var teacher = new Teacher
            {
                TeacherName = dto.TeacherName,
                PhoneNo = dto.PhoneNo,
                Address = dto.Address,
                Email = dto.Email,
                UserID = user.UserID
            };

            await _teacherRepository.AddTeacherAsync(teacher); // ✅

            await _email.SendEmailAsync(new EmailDTO
            {
                To = dto.Email,
                Subject = "Teacher Login",
                Body = $"Username: {user.UserName}, Password: {password}"
            });

            return teacher;
        }
        public async Task<IEnumerable<Teacher>> GetAllTeachersAsync()
        {
            return await _teacherRepository.GetAllTeachersAsync();
        }

        public async Task<Teacher?> GetTeacherByIdAsync(Guid id)
        {
            return await _teacherRepository.GetTeacherByIdAsync(id);
        }

        public async Task<Teacher?> UpdateTeacherAsync(Guid id, TeacherRequestDTO request)
        {
            var teacher = new Teacher
            {
                TeacherID = id,
                TeacherName = request.TeacherName,
                PhoneNo = request.PhoneNo,
                Address = request.Address,
                Email = request.Email,
               
            };

            return await _teacherRepository.UpdateTeacherAsync(teacher);
        }

        public async Task<bool> DeleteTeacherAsync(Guid id)
        {
            return await _teacherRepository.DeleteTeacherAsync(id);
        }
    }
}
