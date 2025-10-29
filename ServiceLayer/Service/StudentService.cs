using RepositoryLayer.Entity;
using RepositoryLayer.RepoInterFace;
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

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _studentRepository.GetAllStudents();
        }

        public async Task<Student?> GetStudentByIdAsync(Guid id)
        {
            return await _studentRepository.GetStudentById(id);
        }

        public async Task<Student> AddStudentAsync(StudentRequestDTO request)
        {
            var student = new Student
            {
                StudentID = Guid.NewGuid(),
                StudentName = request.StudentName,
                PhoneNo = request.PhoneNo,
                Address = request.Address,
                Email = request.Email,
                UserID = request.UserID,
                ClassID = request.ClassID
            };

            return await _studentRepository.AddStudent(student);
        }

        public async Task<Student?> UpdateStudentAsync(Guid id, StudentRequestDTO request)
        {
            var existing = await _studentRepository.GetStudentById(id);
            if (existing == null) return null;

            existing.StudentName = request.StudentName;
            existing.PhoneNo = request.PhoneNo;
            existing.Address = request.Address;
            existing.Email = request.Email;
            existing.UserID = request.UserID;
            existing.ClassID = request.ClassID;

            return await _studentRepository.UpdateStudent(existing);
        }

        public async Task<bool> DeleteStudentAsync(Guid id)
        {
            return await _studentRepository.DeleteStudent(id);
        }
    }
}
