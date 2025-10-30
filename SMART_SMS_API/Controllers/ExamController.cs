using Microsoft.AspNetCore.Mvc;
using ServiceLayer.ServiceInterFace;
using System;
using System.Threading.Tasks;

namespace SMART_SMS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExamController : ControllerBase
    {
        private readonly IExamService _examService;

        public ExamController(IExamService examService)
        {
            _examService = examService;
        }

        // 🟣 Add new Exam (parameters-based)
        [HttpPost("add")]
        public async Task<IActionResult> AddExam(Guid classID, Guid subjectID, string examName, DateTime examDate)
        {
            var examDto = new ServiceLayer.DTO.RequestDTO.ExamRequestDTO
            {
                ClassID = classID,
                SubjectID = subjectID,
                ExamName = examName,
                ExamDate = examDate
            };

            var exam = await _examService.AddExamAsync(examDto);
            return Ok(exam);
        }

        // 🟢 Get all Exams
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllExams()
        {
            var exams = await _examService.GetAllExamsAsync();
            return Ok(exams);
        }

        // 🟡 Get Exam by ID
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetExamById(Guid id)
        {
            var exam = await _examService.GetExamByIdAsync(id);
            if (exam == null)
                return NotFound(new { message = "Exam not found" });

            return Ok(exam);
        }

        // 🔵 Update Exam (parameters-based)
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateExam(Guid id, Guid classID, Guid subjectID, string examName, DateTime examDate)
        {
            var examDto = new ServiceLayer.DTO.RequestDTO.ExamRequestDTO
            {
                ClassID = classID,
                SubjectID = subjectID,
                ExamName = examName,
                ExamDate = examDate
            };

            var updated = await _examService.UpdateExamAsync(id, examDto);
            if (updated == null)
                return NotFound(new { message = "Exam not found" });

            return Ok(updated);
        }

        // 🔴 Delete Exam
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteExam(Guid id)
        {
            var deleted = await _examService.DeleteExamAsync(id);
            if (!deleted)
                return NotFound(new { message = "Exam not found or already deleted" });

            return Ok(new { message = "Exam deleted successfully" });
        }
    }
}
