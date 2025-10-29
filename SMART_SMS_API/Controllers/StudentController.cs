using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DTO.RequestDTO;
using ServiceLayer.ServiceInterFace;
using System;
using System.Threading.Tasks;

namespace SMART_SMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // 🟢 Get all students
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students);
        }

        // 🟡 Get student by ID
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetStudentById(Guid id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
                return NotFound(new { message = "Student not found" });

            return Ok(student);
        }

        // 🟣 Add new student
        [HttpPost("add")]
        public async Task<IActionResult> AddStudent([FromBody] StudentRequestDTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var added = await _studentService.AddStudentAsync(request);
            return CreatedAtAction(nameof(GetStudentById), new { id = added.StudentID }, added);
        }

        // 🔵 Update student
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateStudent(Guid id, [FromBody] StudentRequestDTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _studentService.UpdateStudentAsync(id, request);
            if (updated == null)
                return NotFound(new { message = "Student not found" });

            return Ok(updated);
        }

        // 🔴 Delete student
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            var result = await _studentService.DeleteStudentAsync(id);
            if (!result)
                return NotFound(new { message = "Student not found or already deleted" });

            return Ok(new { message = "Student deleted successfully" });
        }
    }
}
