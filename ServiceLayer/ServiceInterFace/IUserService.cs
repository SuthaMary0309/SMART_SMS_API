using RepositoryLayer.Entity;
using ServiceLayer.DTO.RequestDTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceInterFace
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(Guid id);
        Task<User> AddUserAsync(UserRequestDTO request);
        Task<User?> UpdateUserAsync(Guid id, UserRequestDTO request);
        Task<bool> DeleteUserAsync(Guid id);
    }
}
