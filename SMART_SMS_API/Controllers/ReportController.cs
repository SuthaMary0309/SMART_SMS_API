using Microsoft.AspNetCore.Mvc;
using ServiceLayer.ServiceInterFace;
using System;
using System.Threading.Tasks;
using RepositoryLayer.AppDbContext;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SMART_SMS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly ApplicationDbContext _db;

        public ReportController(IReportService reportService, ApplicationDbContext db)
        {
            _reportService = reportService;
            _db = db;
        }

        // Dropdown helpers
        [HttpGet("students")]
        public async Task<IActionResult> GetAllStudents()
        {
            var list = await _db.Students
                .Select(s => new { studentId = s.StudentID, studentName = s.StudentName, classId = s.ClassID })
                .ToListAsync();
            return Ok(list);
        }

        [HttpGet("exams")]
        public async Task<IActionResult> GetAllExams()
        {
            var list = await _db.Exams
                .Select(e => new { examId = e.ExamID, examName = e.ExamName, subjectId = e.SubjectID })
                .ToListAsync();
            return Ok(list);
        }

        [HttpGet("classes")]
        public async Task<IActionResult> GetAllClasses()
        {
            var list = await _db.Classes
                .Select(c => new { classId = c.ClassId, className = c.ClassName })
                .ToListAsync();
            return Ok(list);
        }

        // Main student report endpoint
        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetStudentReport(Guid studentId)
        {
            var dto = await _reportService.GenerateStudentReportAsync(studentId);
            if (dto == null) return NotFound("Student not found");
            return Ok(dto);
        }
    }
}
