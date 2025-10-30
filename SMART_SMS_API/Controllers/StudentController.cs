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

        // 🟣 Add new student (parameters-based)
        [HttpPost("add")]
        public async Task<IActionResult> AddStudent(string studentName, int phoneNo, string address, string email, Guid userID, Guid classID)
        {
            var studentDto = new StudentRequestDTO
            {
                StudentName = studentName,
                PhoneNo = phoneNo,
                Address = address,
                Email = email,
                UserID = userID,
                ClassID = classID
            };

            var added = await _studentService.AddStudentAsync(studentDto);
            return Ok(added);
        }

        // 🔵 Update student (parameters-based)
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateStudent(Guid id, string studentName, int phoneNo, string address, string email, Guid userID, Guid classID)
        {
            var studentDto = new StudentRequestDTO
            {
                StudentName = studentName,
                PhoneNo = phoneNo,
                Address = address,
                Email = email,
                UserID = userID,
                ClassID = classID
            };

            var updated = await _studentService.UpdateStudentAsync(id, studentDto);
            if (updated == null)
                return NotFound(new { message = "Student not found to update" });

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
