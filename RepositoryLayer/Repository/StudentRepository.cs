using Microsoft.EntityFrameworkCore;
using RepositoryLayer.AppDbContext;
using RepositoryLayer.Entity;
using RepositoryLayer.RepoInterFace;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoryLayer.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public StudentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Student> AddStudent(Student student)
        {
            var result = await _dbContext.Students.AddAsync(student);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            return await _dbContext.Students.ToListAsync();
        }

        public async Task<Student?> GetStudentById(Guid id)
        {
            return await _dbContext.Students.FindAsync(id);
        }


        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Student?> UpdateStudent(Student student)
        {
            var existing = await _dbContext.Students.FindAsync(student.StudentID);
            if (existing == null) return null;

            existing.StudentName = student.StudentName;
            existing.PhoneNo = student.PhoneNo;
            existing.Address = student.Address;
            existing.Email = student.Email;
            existing.ClassID = student.ClassID;

            await _dbContext.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteStudent(Guid id)
        {
            var existing = await _dbContext.Students.FindAsync(id);
            if (existing == null) return false;

            _dbContext.Students.Remove(existing);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Student?> UpdateProfileImage(Guid id, string profileUrl, string publicId)
        {
            var student = await GetStudentById(id);
            if (student == null) return null;

            student.ProfileURL = profileUrl;
            student.ProfileImagePublicId = publicId;

            _dbContext.Students.Update(student);
            await _dbContext.SaveChangesAsync();
            return student;
        }
    }
}
