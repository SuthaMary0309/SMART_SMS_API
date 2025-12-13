using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoryLayer.RepoInterFace
{
    public interface IStudentRepository
    {
        Task<Student> AddStudent(Student student);
        Task<IEnumerable<Student>> GetAllStudents();
        Task<Student?> GetStudentById(Guid id);
        Task<Student?> UpdateStudent(Student student);
        Task<bool> DeleteStudent(Guid id);
        Task SaveChangesAsync();

        Task<Student?> UpdateProfileImage(Guid id, string profileUrl, string publicId);
    }
}
