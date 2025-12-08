// Controllers/ReportController.cs
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.ServiceInterFace;
using System;
using System.Threading.Tasks;

namespace SMART_SMS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _report;

        public ReportController(IReportService report)
        {
            _report = report;
        }

        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> StudentReport(Guid studentId)
        {
            var r = await _report.GenerateStudentReportAsync(studentId);
            if (r == null) return NotFound();
            return Ok(r);
        }

        [HttpGet("exam/{examId}")]
        public async Task<IActionResult> ExamReport(Guid examId)
        {
            var r = await _report.GenerateExamReportAsync(examId);
            if (r == null) return NotFound();
            return Ok(r);
        }

        [HttpGet("class/{classId}/exam/{examId}")]
        public async Task<IActionResult> ClassPerformance(Guid classId, Guid examId)
        {
            var r = await _report.GenerateClassPerformanceAsync(classId, examId);
            if (r == null) return NotFound();
            return Ok(r);
        }
    }
}
