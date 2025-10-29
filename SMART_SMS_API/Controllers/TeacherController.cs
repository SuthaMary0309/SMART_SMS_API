using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DTO.RequestDTO;
using ServiceLayer.ServiceInterFace;
using System;
using System.Threading.Tasks;

namespace SmartSMS.Controllers
{
    [Route("api/teacher")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        // 🟣 Add new teacher (parameters-based)
        [HttpPost("add")]
        public async Task<IActionResult> AddTeacher(string teacherName, string phoneNo, string address, string email, Guid userID)
        {
            var teacherDto = new TeacherRequestDTO
            {
                TeacherName = teacherName,
                PhoneNo = phoneNo,
                Address = address,
                Email = email,
                UserID = userID
            };

            var newTeacher = await _teacherService.AddTeacherAsync(teacherDto);
            return Ok(newTeacher);
        }

        // 🟢 Get all teachers
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllTeachers()
        {
            var teachers = await _teacherService.GetAllTeachersAsync();
            return Ok(teachers);
        }

        // 🟡 Get teacher by ID
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetTeacherById(Guid id)
        {
            var teacher = await _teacherService.GetTeacherByIdAsync(id);
            if (teacher == null)
                return NotFound(new { message = "Teacher not found" });

            return Ok(teacher);
        }

        // 🔵 Update teacher (parameters-based)
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateTeacher(Guid id, string teacherName, string phoneNo, string address, string email, Guid userID)
        {
            var teacherDto = new TeacherRequestDTO
            {
                TeacherName = teacherName,
                PhoneNo = phoneNo,
                Address = address,
                Email = email,
                UserID = userID
            };

            var updated = await _teacherService.UpdateTeacherAsync(id, teacherDto);
            if (updated == null)
                return NotFound(new { message = "Teacher not found to update" });

            return Ok(updated);
        }

        // 🔴 Delete teacher
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteTeacher(Guid id)
        {
            var deleted = await _teacherService.DeleteTeacherAsync(id);
            if (!deleted)
                return NotFound(new { message = "Teacher not found" });

            return Ok(new { message = "Teacher deleted successfully" });
        }
    }
}
