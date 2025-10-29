using Microsoft.EntityFrameworkCore;
using RepositoryLayer.AppDbContext;
using RepositoryLayer.Entity;
using RepositoryLayer.RepoInterFace;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoryLayer.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SubjectRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Subject> AddSubject(Subject subject)
        {
            var result = await _dbContext.Subjects.AddAsync(subject);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<IEnumerable<Subject>> GetAllSubjects()
        {
            return await _dbContext.Subjects.ToListAsync();
        }

        public async Task<Subject?> GetSubjectById(Guid id)
        {
            return await _dbContext.Subjects.FindAsync(id);
        }

        public async Task<Subject?> UpdateSubject(Subject subject)
        {
            var existing = await _dbContext.Subjects.FindAsync(subject.SubjectID);
            if (existing == null) return null;

            existing.SubjectName = subject.SubjectName;
            existing.StudentID = subject.StudentID;
            existing.ClassID = subject.ClassID;
            existing.UserID = subject.UserID;
            existing.TeacherID = subject.TeacherID;

            await _dbContext.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteSubject(Guid id)
        {
            var existing = await _dbContext.Subjects.FindAsync(id);
            if (existing == null) return false;

            _dbContext.Subjects.Remove(existing);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
