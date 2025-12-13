using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DTO.RequestDTO;
using ServiceLayer.ServiceInterFace;
using System;
using System.Threading.Tasks;

namespace SMART_SMS_API.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllStudents()
        {
            return Ok(await _studentService.GetAllStudentsAsync());
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetStudentById(Guid id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
                return NotFound(new { message = "Student not found" });

            return Ok(student);
        }

        [HttpPost("add")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddStudent([FromForm] AddStudentFormRequest formRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var request = new StudentRequestDTO
                {
                    StudentName = formRequest.StudentName,
                    PhoneNo = formRequest.PhoneNo,
                    Address = formRequest.Address,
                    Email = formRequest.Email,
                    ClassID = formRequest.ClassID
                };

                var student = await _studentService.AddStudentAsync(request, formRequest.ProfileImage);
                return Ok(student);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while adding the student", error = ex.Message });
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateStudent(Guid id, [FromBody] StudentRequestDTO request)
        {
            var updated = await _studentService.UpdateStudentAsync(id, request);
            if (updated == null)
                return NotFound(new { message = "Student not found" });

            return Ok(updated);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            var deleted = await _studentService.DeleteStudentAsync(id);
            if (!deleted)
                return NotFound(new { message = "Not found" });

            return Ok(new { message = "Student deleted successfully" });
        }

        [HttpGet("get/{id}/profile-image")]
        public async Task<IActionResult> GetProfileImage(Guid id)
        {
            var imageUrl = await _studentService.GetProfileImageUrlAsync(id);
            if (imageUrl == null)
            {
                var student = await _studentService.GetStudentByIdAsync(id);
                if (student == null)
                    return NotFound(new { message = "Student not found" });
                
                return NotFound(new { message = "Profile image not found", imageUrl = (string?)null });
            }

            return Ok(new { imageUrl });
        }

        [HttpPut("update/{id}/profile-image")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateProfileImage(Guid id, [FromForm] IFormFile profileImage)
        {
            if (profileImage == null || profileImage.Length == 0)
            {
                return BadRequest(new { message = "Profile image is required" });
            }

            try
            {
                var updated = await _studentService.UpdateProfileImageWithFileAsync(id, profileImage);
                if (updated == null)
                    return NotFound(new { message = "Student not found" });

                return Ok(new 
                { 
                    message = "Profile image updated successfully",
                    imageUrl = updated.ProfileURL,
                    student = updated
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the profile image", error = ex.Message });
            }
        }

    }
}
