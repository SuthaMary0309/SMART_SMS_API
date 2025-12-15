using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using ServiceLayer.ServiceInterFace;

namespace SMART_SMS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly ICloudinaryService _cloudinary;

        public ProfileController(IStudentService studentService, ICloudinaryService cloudinary)
        {
            _studentService = studentService;
            _cloudinary = cloudinary;
        }

        [HttpPost("{id}/upload")]
        public async Task<IActionResult> UploadProfileImage(Guid id, IFormFile file)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null) return NotFound("Student not found");

            var allowed = new[] { "image/jpeg", "image/png", "image/webp" };
            if (file == null || !allowed.Contains(file.ContentType)) return BadRequest("Invalid image");

            var publicId = $"students/{id}-{Guid.NewGuid():N}";
            var uploadResult = await _cloudinary.UploadImageAsync(file, publicId, 600, 600);
            if (uploadResult.Error != null) return StatusCode(500, uploadResult.Error.Message);

            // Delete old image if exists
            if (!string.IsNullOrEmpty(student.ProfileImagePublicId))
                await _cloudinary.DeleteImageAsync(student.ProfileImagePublicId);

            // Update profile image
            await _studentService.UpdateProfileImageAsync(id, uploadResult.SecureUrl?.ToString(), uploadResult.PublicId);

            return Ok(new { imageUrl = uploadResult.SecureUrl?.ToString() });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfile(Guid id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null) return NotFound();

            return Ok(new
            {
                student.StudentID,
                student.StudentName,
                student.Email,
                student.PhoneNo,
                student.Address,
                student.ClassID,
                student.ProfileURL
            });
        }
    }
}
