using RepositoryLayer.Entity;
using ServiceLayer.DTO.RequestDTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceInterFace
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<Student?> GetStudentByIdAsync(Guid id);
        Task<Student> AddStudentAsync(StudentRequestDTO request);
        Task<Student?> UpdateStudentAsync(Guid id, StudentRequestDTO request);
        Task<bool> DeleteStudentAsync(Guid id);
    }
}
