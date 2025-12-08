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
        public async Task<IActionResult> AddStudent([FromBody] StudentRequestDTO request)
        {
            return Ok(await _studentService.AddStudentAsync(request));
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
    }
}
