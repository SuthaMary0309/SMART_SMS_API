using Microsoft.AspNetCore.Mvc;
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
        private readonly IJwtService _jwtService;
        private readonly ILogger<AuthController> _logger;
        // inject an email sender if you have one (IEmailService) — placeholder shown

        public AuthController(IAuthRepository authRepo, IJwtService jwtService, ILogger<AuthController> logger)
        {
            _authRepo = authRepo;
            _jwtService = jwtService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
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
                    //AdmissionNumber = dto.AdmissionNumber // optional
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
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest(new { message = "Email and password required." });

            var user = await _authRepo.Login(dto.Email, dto.Password);
            if (user == null)
                return Unauthorized(new { message = "Invalid Email/Password" });

            var token = _jwtService.GenerateJwtToken(user);
            return Ok(new { token, role = user.Role, username = user.UserName });
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

                // TODO: send token via email
                // For demo return token — in production DO NOT return token in response.
                // Call your email service here: _emailService.SendResetEmail(dto.Email, token);

                return Ok(new { message = "Reset token generated. Check email.", token }); // remove token on production
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
    }
}