using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.RepositoryInterface
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);

       
        Task<User?> Login(string email, string password);
        // New:
        Task<string> CreatePasswordResetToken(string email); // returns token (or null / throw)
        Task<bool> ResetPassword(string token, string newPassword);
        Task<bool> IsEmailTaken(string email);
    }
}
