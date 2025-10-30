using Microsoft.AspNetCore.Mvc;
using ServiceLayer.ServiceInterFace;
using System;
using System.Threading.Tasks;

namespace SMART_SMS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarksController : ControllerBase
    {
        private readonly IMarksService _marksService;

        public MarksController(IMarksService marksService)
        {
            _marksService = marksService;
        }

        // 🟣 Add new Marks (parameters-based)
        [HttpPost("add")]
        public async Task<IActionResult> AddMarks(int grade, int mark, Guid studentID, Guid examID)
        {
            var marksDto = new ServiceLayer.DTO.RequestDTO.MarksRequestDTO
            {
                Grade = grade,
                Mark = mark,
                StudentID = studentID,
                ExamID = examID
            };

            var marks = await _marksService.AddMarksAsync(marksDto);
            return Ok(marks);
        }

        // 🟢 Get all Marks
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllMarks()
        {
            var marks = await _marksService.GetAllMarksAsync();
            return Ok(marks);
        }

        // 🟡 Get Marks by ID
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetMarksById(Guid id)
        {
            var marks = await _marksService.GetMarksByIdAsync(id);
            if (marks == null)
                return NotFound(new { message = "Marks not found" });

            return Ok(marks);
        }

        // 🔵 Update Marks (parameters-based)
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateMarks(Guid id, int grade, int mark, Guid studentID, Guid examID)
        {
            var marksDto = new ServiceLayer.DTO.RequestDTO.MarksRequestDTO
            {
                Grade = grade,
                Mark = mark,
                StudentID = studentID,
                ExamID = examID
            };

            var updated = await _marksService.UpdateMarksAsync(id, marksDto);
            if (updated == null)
                return NotFound(new { message = "Marks not found" });

            return Ok(updated);
        }

        // 🔴 Delete Marks
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteMarks(Guid id)
        {
            var deleted = await _marksService.DeleteMarksAsync(id);
            if (!deleted)
                return NotFound(new { message = "Marks not found or already deleted" });

            return Ok(new { message = "Marks deleted successfully" });
        }
    }
}
