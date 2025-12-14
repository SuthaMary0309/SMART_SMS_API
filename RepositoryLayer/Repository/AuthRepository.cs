using Microsoft.EntityFrameworkCore;
using RepositoryLayer.AppDbContext;
using RepositoryLayer.Entity;
using RepositoryLayer.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _db;

        public AuthRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<(User user, string plainPassword)> CreateUserAuto(string email, string role)

        {
            if (await _db.Users.AnyAsync(u => u.Email == email))
                throw new InvalidOperationException("User already exists for this email");

            var password = GenerateRandomPassword();

            var user = new User
            {
                Email = email,
                Role = role
            };

            // Admission No
            user.AdmissionNumber = await GenerateAdmissionNumber();

            var prefix = role.ToLower() switch
            {
                "student" => "STU",
                "teacher" => "TEA",
                "parent" => "PAR",
                "admin" => "ADM",
                _ => "USR"
            };

            user.UserName = $"SMART_SMS_{prefix}{user.AdmissionNumber}";

            CreatePasswordHash(password, out byte[] hash, out byte[] salt);
            user.PasswordHash = hash;
            user.PasswordSalt = salt;

            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return (user, password); // 🔥 password return pannrom (email send panna)
        }

        private string GenerateRandomPassword()
        {
            return $"SMS@{Guid.NewGuid().ToString("N").Substring(0, 8)}";
        }


        public async Task<User?> Login(string email, string password)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null) return null;

            if (!VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public async Task<bool> IsEmailTaken(string email)
        {
            return await _db.Users.AnyAsync(u => u.Email == email);
        }

        // Create reset token
        public async Task<string> CreatePasswordResetToken(string email)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) throw new InvalidOperationException("No user with that email.");

            // secure random token
            var tokenBytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(tokenBytes);
            }
            var token = Convert.ToBase64String(tokenBytes);

            user.ResetToken = token;
            user.ResetTokenExpires = DateTime.UtcNow.AddHours(1); // token valid for 1 hour
            user.UpdatedAt = DateTime.UtcNow;

            _db.Users.Update(user);
            await _db.SaveChangesAsync();

            return token;
        }

        public async Task<bool> ResetPassword(string token, string newPassword)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.ResetToken == token && u.ResetTokenExpires > DateTime.UtcNow);
            if (user == null) return false;

            CreatePasswordHash(newPassword, out byte[] hash, out byte[] salt);
            user.PasswordHash = hash;
            user.PasswordSalt = salt;

            // clear reset token
            user.ResetToken = null;
            user.ResetTokenExpires = null;
            user.UpdatedAt = DateTime.UtcNow;

            _db.Users.Update(user);
            await _db.SaveChangesAsync();
            return true;
        }

        // Helpers...
        private async Task<string> GenerateAdmissionNumber()
        {
            // Simple approach: 6 digit random not already used
            string candidate;
            var rand = new Random();
            do
            {
                candidate = rand.Next(100000, 999999).ToString();
            } while (await _db.Users.AnyAsync(u => u.AdmissionNumber == candidate));

            return candidate;
        }

        private void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                salt = hmac.Key;
                hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPassword(string password, byte[] hash, byte[] salt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(salt))
            {
                var compute = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return compute.SequenceEqual(hash);
            }
        }

        public Task<User> Register(User user, string password)
        {
            throw new NotImplementedException();
        }
    }
}
