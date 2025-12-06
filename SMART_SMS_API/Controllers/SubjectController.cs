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
        public async Task<IActionResult> AddSubject([FromBody] SubjectRequestDTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _subjectService.AddSubjectAsync(request);
            return Ok(created);
        }


        // 🔵 Update subject (parameters-based)
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateSubject(Guid id, [FromBody] SubjectRequestDTO request)
        {
            var updated = await _subjectService.UpdateSubjectAsync(id, request);

            if (updated == null)
                return NotFound(new { message = "Subject not found" });

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
