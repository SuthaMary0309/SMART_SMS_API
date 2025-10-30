using RepositoryLayer.Entity;
using RepositoryLayer.RepoInterFace;
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

        public TeacherService(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<IEnumerable<Teacher>> GetAllTeachersAsync()
        {
            return await _teacherRepository.GetAllTeachersAsync();
        }

        public async Task<Teacher?> GetTeacherByIdAsync(Guid id)
        {
            return await _teacherRepository.GetTeacherByIdAsync(id);
        }

        public async Task<Teacher> AddTeacherAsync(TeacherRequestDTO request)
        {
            var teacher = new Teacher
            {
                TeacherName = request.TeacherName,
                PhoneNo = request.PhoneNo,
                Address = request.Address,
                Email = request.Email,
                UserID = request.UserID
            };

            return await _teacherRepository.AddTeacherAsync(teacher);
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
                UserID = request.UserID
            };

            return await _teacherRepository.UpdateTeacherAsync(teacher);
        }

        public async Task<bool> DeleteTeacherAsync(Guid id)
        {
            return await _teacherRepository.DeleteTeacherAsync(id);
        }
    }
}
