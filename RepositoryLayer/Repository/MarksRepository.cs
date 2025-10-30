using Microsoft.EntityFrameworkCore;
using RepositoryLayer.AppDbContext;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repository
{
    public class MarksRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public MarksRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Marks> AddMarks(Marks marks)
        {
            var result = await _dbContext.Marks.AddAsync(marks);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<IEnumerable<Marks>> GetAllMarks()
        {
            return await _dbContext.Marks.ToListAsync();
        }

        public async Task<Marks?> GetMarksById(Guid id)
        {
            return await _dbContext.Marks.FindAsync(id);
        }

        public async Task<Marks?> UpdateMarks(Marks marks)
        {
            var existing = await _dbContext.Marks.FindAsync(marks.MarksId);
            if (existing == null) return null;

            existing.Grade = marks.Grade;
            existing.Mark = marks.Mark;
            existing.ExamID = marks.ExamID;
            existing.StudentID = marks.StudentID;

            await _dbContext.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteMarks(Guid id)
        {
            var existing = await _dbContext.Marks.FindAsync(id);
            if (existing == null) return false;

            _dbContext.Marks.Remove(existing);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
    

