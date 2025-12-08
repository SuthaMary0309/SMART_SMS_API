using Microsoft.EntityFrameworkCore;
using RepositoryLayer.AppDbContext;
using RepositoryLayer.Entity;
using RepositoryLayer.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repository
{
    public class ExamRepository :IExamRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ExamRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Exam> AddExam(Exam exam)
        {
            var result = await _dbContext.Exams.AddAsync(exam);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<IEnumerable<Exam>> GetAllExams()
        {
            return await _dbContext.Exams.ToListAsync();
        }

        public async Task<Exam?> GetExamById(Guid id)
        {
            return await _dbContext.Exams.FindAsync(id);
        }

        public async Task<Exam?> UpdateExam(Exam exam)
        {
            var existing = await _dbContext.Exams.FindAsync(exam.ExamID);
            if (existing == null) return null;

            existing.ExamName = exam.ExamName;
            existing.ExamDate = exam.ExamDate;
            existing.SubjectID = exam.SubjectID;
            existing.ClassID = exam.ClassID;

            await _dbContext.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteExam(Guid id)
        {
            var existing = await _dbContext.Exams.FindAsync(id);
            if (existing == null) return false;

            _dbContext.Exams.Remove(existing);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
    

