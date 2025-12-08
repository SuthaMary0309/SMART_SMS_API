using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DTO.RequestDTO;
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
        public async Task<IActionResult> AddExam([FromBody] ExamRequestDTO dto)
        {
            var result = await _examService.AddExamAsync(dto);
            return Ok(result);
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
        public async Task<IActionResult> UpdateExam(Guid id, [FromBody] ExamRequestDTO dto)
        {
            var result = await _examService.UpdateExamAsync(id, dto);
            return Ok(result);
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
