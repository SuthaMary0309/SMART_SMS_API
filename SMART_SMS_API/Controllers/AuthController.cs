using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.AppDbContext;
using RepositoryLayer.Entity;
using RepositoryLayer.RepositoryInterface;
using ServiceLayer.DTO;
using ServiceLayer.ServiceInterFace;

namespace SMART_SMS_API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        private readonly ApplicationDbContext _context;
        private readonly IJwtService _jwtService;
        private readonly ILogger<AuthController> _logger;
        // inject an email sender if you have one (IEmailService) — placeholder shown

        public AuthController(IAuthRepository authRepo, IJwtService jwtService, ILogger<AuthController> logger, ApplicationDbContext context)
        {
            _authRepo = authRepo;
            _jwtService = jwtService;
            _logger = logger;
            _context = context;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDTO dto)
        {
            try
            {
                // Basic server-side validation
                if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password) || string.IsNullOrWhiteSpace(dto.Role))
                    return BadRequest(new { message = "Required fields missing." });

                if (dto.Password.Length < 6)
                    return BadRequest(new { message = "Password must be at least 6 characters." });

                if (dto.Password != dto.ConfirmPassword)
                    return BadRequest(new { message = "Passwords do not match." });

                if (await _authRepo.IsEmailTaken(dto.Email))
                    return BadRequest(new { message = "Email already registered." });

                var user = new User
                {
                    Email = dto.Email,
                    Role = dto.Role,
                 
                };

                var created = await _authRepo.Register(user, dto.Password);

                // Return created user basic info (do NOT return password hashes)
                return Ok(new { message = "User Registered Successfully", user = new { created.UserID, created.UserName, created.Email, created.Role } });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Registration failed");
                return StatusCode(500, new { message = "Registration failed", detail = ex.Message });
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest(new { message = "Email and password required." });

            var user = await _authRepo.Login(dto.Email, dto.Password);
            if (user == null)
                return Unauthorized(new { message = "Invalid Email/Password" });

            var token = _jwtService.GenerateJwtToken(user);

            // Role-based response with additional details
            if (user.Role == "Student")
            {
                var student = await _context.Students.FirstOrDefaultAsync(s => s.UserID == user.UserID);
                return Ok(new 
                { 
                    token, 
                    role = user.Role, 
                    username = user.UserName,
                    studentID = student?.StudentID,
                    studentName = student?.StudentName ?? user.UserName,
                    name = student?.StudentName ?? user.UserName
                });
            }
            else if (user.Role == "Teacher")
            {
                var teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.UserID == user.UserID);
                return Ok(new 
                { 
                    token, 
                    role = user.Role, 
                    username = user.UserName,
                    teacherID = teacher?.TeacherID,
                    name = teacher?.TeacherName ?? user.UserName
                });
            }
            else if (user.Role == "Parent")
            {
                var parent = await _context.Parents.FirstOrDefaultAsync(p => p.UserID == user.UserID);
                return Ok(new 
                { 
                    token, 
                    role = user.Role, 
                    username = user.UserName,
                    parentID = parent?.ParentID,
                    name = parent?.ParentName ?? user.UserName
                });
            }
            
            // Admin or other roles
            return Ok(new { token, role = user.Role, username = user.UserName, name = user.UserName });
        }

        // Forgot password - request a reset token
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.Email))
                    return BadRequest(new { message = "Email required." });

                var token = await _authRepo.CreatePasswordResetToken(dto.Email);

           

                return Ok(new { message = "Reset token generated. Check email.", token }); 
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Forgot password failed");
                return StatusCode(500, new { message = "Operation failed" });
            }
        }

        // Reset password endpoint
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Token) || string.IsNullOrWhiteSpace(dto.NewPassword))
                return BadRequest(new { message = "Token and new password required." });

            if (dto.NewPassword.Length < 6)
                return BadRequest(new { message = "Password must be at least 6 characters." });

            var result = await _authRepo.ResetPassword(dto.Token, dto.NewPassword);
            if (!result)
                return BadRequest(new { message = "Invalid or expired token." });

            return Ok(new { message = "Password reset successful." });
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetStats()
        {
            var studentsCount = await _context.Students.CountAsync();
            var teachersCount = await _context.Teachers.CountAsync();
            var examsCount = await _context.Exams.CountAsync();
            var parentsCount = await _context.Parents.CountAsync();

            var stats = new
            {
                Students = studentsCount,
                Teachers = teachersCount,
                Exams = examsCount,
                Parents = parentsCount
            };

            return Ok(stats);
        }
    }
}