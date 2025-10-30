using Microsoft.EntityFrameworkCore;
using RepositoryLayer.AppDbContext;
using RepositoryLayer.Entity;
using RepositoryLayer.RepoInterFace;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoryLayer.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> AddUser(User user)
        {
            var response = await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return response.Entity;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User?> GetUserById(Guid id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public async Task<User?> UpdateUser(User user)
        {
            var existing = await _dbContext.Users.FindAsync(user.Id);
            if (existing == null) return null;

            existing.UserName = user.UserName;
            existing.Age = user.Age;
            existing.Email = user.Email;
            existing.Role = user.Role;

            await _dbContext.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null) return false;

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
