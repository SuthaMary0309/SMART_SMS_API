using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoryLayer.RepoInterFace
{
    public interface IUserRepository
    {
        Task<User> AddUser(User user);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User?> GetUserById(Guid id);
        Task<User?> UpdateUser(User user);
        Task<bool> DeleteUser(Guid id);
    }
}
