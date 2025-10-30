using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DTO.RequestDTO;
using ServiceLayer.ServiceInterFace;
using System;
using System.Threading.Tasks;

namespace SmartSMS.Controllers
{
    [Route("api/subject")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        // 🟢 Get all subjects
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllSubjects()
        {
            var subjects = await _subjectService.GetAllSubjectsAsync();
            return Ok(subjects);
        }

        // 🟡 Get subject by ID
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetSubjectById(Guid id)
        {
            var subject = await _subjectService.GetSubjectByIdAsync(id);
            if (subject == null)
                return NotFound(new { message = "Subject not found" });

            return Ok(subject);
        }

        // 🟣 Add new subject (parameters-based)
        [HttpPost("add")]
        public async Task<IActionResult> AddSubject(string subjectName, Guid studentID, Guid classID, Guid userID, Guid teacherID)
        {
            var subjectDto = new SubjectRequestDTO
            {
                SubjectName = subjectName,
                StudentID = studentID,
                ClassID = classID,
                UserID = userID,
                TeacherID = teacherID
            };

            var added = await _subjectService.AddSubjectAsync(subjectDto);
            return Ok(added);
        }

        // 🔵 Update subject (parameters-based)
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateSubject(Guid id, string subjectName, Guid studentID, Guid classID, Guid userID, Guid teacherID)
        {
            var subjectDto = new SubjectRequestDTO
            {
                SubjectName = subjectName,
                StudentID = studentID,
                ClassID = classID,
                UserID = userID,
                TeacherID = teacherID
            };

            var updated = await _subjectService.UpdateSubjectAsync(id, subjectDto);
            if (updated == null)
                return NotFound(new { message = "Subject not found to update" });

            return Ok(updated);
        }

        // 🔴 Delete subject
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteSubject(Guid id)
        {
            var result = await _subjectService.DeleteSubjectAsync(id);
            if (!result)
                return NotFound(new { message = "Subject not found or already deleted" });

            return Ok(new { message = "Subject deleted successfully" });
        }
    }
}
