using Microsoft.AspNetCore.Mvc;
using ServiceLayer.ServiceInterFace;
using System;
using System.Threading.Tasks;

namespace SmartSMS.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // 🟣 Add new user (parameters-based)
        [HttpPost("add")]
        public async Task<IActionResult> AddUser(string userName, int age, string email, string role)
        {
            var userDto = new ServiceLayer.DTO.RequestDTO.UserRequestDTO
            {
                UserName = userName,
                Age = age,
                Email = email,
                Role = role
            };

            var user = await _userService.AddUserAsync(userDto);
            return Ok(user);
        }

        // 🟢 Get all users
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        // 🟡 Get user by ID
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound(new { message = "User not found" });

            return Ok(user);
        }

        // 🔵 Update user (parameters-based)
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, string userName, int age, string email, string role)
        {
            var userDto = new ServiceLayer.DTO.RequestDTO.UserRequestDTO
            {
                UserName = userName,
                Age = age,
                Email = email,
                Role = role
            };

            var updated = await _userService.UpdateUserAsync(id, userDto);
            if (updated == null)
                return NotFound(new { message = "User not found" });

            return Ok(updated);
        }

        // 🔴 Delete user
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var deleted = await _userService.DeleteUserAsync(id);
            if (!deleted)
                return NotFound(new { message = "User not found or already deleted" });

            return Ok(new { message = "User deleted successfully" });
        }
    }
}
