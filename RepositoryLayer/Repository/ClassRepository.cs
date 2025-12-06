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
    public class ClassRepository : IClassRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ClassRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Class> AddClass(Class @class)
        {
            var result = await _dbContext.Classes.AddAsync(@class);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<IEnumerable<Class>> GetAllClasses()
        {
            return await _dbContext.Classes.ToListAsync();
        }

        public async Task<Class?> GetClassById(Guid id)
        {
            return await _dbContext.Classes.FindAsync(id);
        }

        public async Task<Class?> UpdateClass(Class @class)
        {
            var existing = await _dbContext.Classes.FindAsync(@class.ClassId);
            if (existing == null) return null;

            existing.ClassName = @class.ClassName;
            existing.Grade = @class.Grade;

            await _dbContext.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteClass(Guid id)
        {
            var existing = await _dbContext.Classes.FindAsync(id);
            if (existing == null) return false;

            _dbContext.Classes.Remove(existing);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }

}


