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
            await _dbContext.Students.AddAsync(student);
            await _dbContext.SaveChangesAsync();
            return student;
        }

        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            return await _dbContext.Students
                                   .Include(s => s.Class)
                                   .ToListAsync();
        }

        public async Task<Student?> GetStudentById(Guid id)
        {
            return await _dbContext.Students
                                   .Include(s => s.Class)
                                   .FirstOrDefaultAsync(s => s.StudentID == id);
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
            existing.UserID = student.UserID;

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
    }
}
