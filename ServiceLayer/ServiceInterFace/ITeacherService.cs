using RepositoryLayer.Entity;
using ServiceLayer.DTO.RequestDTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceInterFace
{
    public interface ITeacherService
    {
        Task<IEnumerable<Teacher>> GetAllTeachersAsync();
        Task<Teacher?> GetTeacherByIdAsync(Guid id);
        Task<Teacher> AddTeacherAsync(TeacherRequestDTO request);
        Task<Teacher?> UpdateTeacherAsync(Guid id, TeacherRequestDTO request);
        Task<bool> DeleteTeacherAsync(Guid id);
    }
}
