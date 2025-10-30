using RepositoryLayer.Entity;
using RepositoryLayer.RepoInterFace;
using ServiceLayer.DTO.RequestDTO;
using ServiceLayer.ServiceInterFace;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceLayer.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // 🟣 Add new user
        public async Task<User> AddUserAsync(UserRequestDTO request)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = request.UserName,
                Age = request.Age,
                Email = request.Email,
                Role = request.Role,
                CreatedAt = DateTime.UtcNow
            };

            return await _userRepository.AddUser(user);
        }

        // 🟢 Get all users
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsers();
        }

        // 🟡 Get user by ID
        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _userRepository.GetUserById(id);
        }

        // 🔵 Update user
        public async Task<User?> UpdateUserAsync(Guid id, UserRequestDTO request)
        {
            var existing = await _userRepository.GetUserById(id);
            if (existing == null) return null;

            existing.UserName = request.UserName;
            existing.Age = request.Age;
            existing.Email = request.Email;
            existing.Role = request.Role;

            return await _userRepository.UpdateUser(existing);
        }

        // 🔴 Delete user
        public async Task<bool> DeleteUserAsync(Guid id)
        {
            return await _userRepository.DeleteUser(id);
        }
    }
}
