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
    public class ParentRepository :IParentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ParentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Parent> AddParent(Parent parent)
        {
            var result = await _dbContext.Parents.AddAsync(parent);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<IEnumerable<Parent>> GetAllParents()
        {
            return await _dbContext.Parents.ToListAsync();
        }

        public async Task<Parent?> GetParentById(Guid id)
        {
            return await _dbContext.Parents.FindAsync(id);
        }

        public async Task<Parent?> UpdateStudent(Parent parent)
        {
            var existing = await _dbContext.Parents.FindAsync(parent.ParentID);
            if (existing == null) return null;

            existing.ParentName = parent.ParentName;
            existing.PhoneNo = parent.PhoneNo;
            existing.Address = parent.Address;
            existing.Email = parent.Email;
            existing.UserID = parent.UserID;
            existing.StudentID = parent.StudentID;

            await _dbContext.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteParent(Guid id)
        {
            var existing = await _dbContext.Parents.FindAsync(id);
            if (existing == null) return false;

            _dbContext.Parents.Remove(existing);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public Task<Parent?> UpdateParent(Parent parent)
        {
            throw new NotImplementedException();
        }
    }
}
    

