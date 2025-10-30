using Microsoft.EntityFrameworkCore;
using RepositoryLayer.AppDbContext;
using RepositoryLayer.Entity;
using RepositoryLayer.RepoInterFace;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoryLayer.Repository
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TeacherRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Teacher> AddTeacherAsync(Teacher teacher)
        {
            var result = await _dbContext.Teachers.AddAsync(teacher);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<IEnumerable<Teacher>> GetAllTeachersAsync()
        {
            return await _dbContext.Teachers.ToListAsync();
        }

        public async Task<Teacher?> GetTeacherByIdAsync(Guid id)
        {
            return await _dbContext.Teachers.FindAsync(id);
        }

        public async Task<Teacher?> UpdateTeacherAsync(Teacher teacher)
        {
            var existing = await _dbContext.Teachers.FindAsync(teacher.TeacherID);
            if (existing == null) return null;

            _dbContext.Entry(existing).CurrentValues.SetValues(teacher);
            await _dbContext.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteTeacherAsync(Guid id)
        {
            var teacher = await _dbContext.Teachers.FindAsync(id);
            if (teacher == null) return false;

            _dbContext.Teachers.Remove(teacher);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
