using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.AppDbContext;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SMART_SMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public ReportController(ApplicationDbContext db)
        {
            _db = db;
        }

        // -------------------------------------------------------
        // STUDENT REPORT - FIXED VERSION
        // -------------------------------------------------------
        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetStudentReport(Guid studentId)
        {
            var student = await _db.Students.FirstOrDefaultAsync(s => s.StudentID == studentId);
            if (student == null)
                return NotFound("Student not found");

            var classInfo = await _db.Classes.FirstOrDefaultAsync(c => c.ClassId == student.ClassID);

            // Get Marks + Exam + Subject directly using navigation
            var marks = await _db.Marks
                .Where(m => m.StudentID == studentId)
                .Include(m => m.Exam)
                    .ThenInclude(e => e.Subject)
                .ToListAsync();

            var reportMarks = marks.Select(m => new
            {
                subjectName = m.Exam?.Subject?.SubjectName ?? "Subject Missing",
                examName = m.Exam?.ExamName ?? "Exam Missing",
                mark = m.Mark
            }).ToList();

            return Ok(new
            {
                studentName = student.StudentName,
                className = classInfo?.ClassName ?? "-",
                highest = marks.Any() ? marks.Max(m => m.Mark) : 0,
                lowest = marks.Any() ? marks.Min(m => m.Mark) : 0,
                average = marks.Any() ? marks.Average(m => m.Mark) : 0,
                marks = reportMarks
            });
        }

        // -------------------------------------------------------
        // EXAM REPORT
        // -------------------------------------------------------
        [HttpGet("exam/{examId}")]
        public async Task<IActionResult> GetExamReport(Guid examId)
        {
            var exam = await _db.Exams
                .Include(e => e.Subject)
                .FirstOrDefaultAsync(e => e.ExamID == examId);

            if (exam == null)
                return NotFound("Exam not found");

            var marks = await _db.Marks.Where(m => m.ExamID == examId).ToListAsync();
            var studentIds = marks.Select(m => m.StudentID).Distinct().ToList();

            var students = await _db.Students
                .Where(s => studentIds.Contains(s.StudentID))
                .ToListAsync();

            var reportStudents = marks.Select(m => new
            {
                studentName = students.FirstOrDefault(s => s.StudentID == m.StudentID)?.StudentName ?? "Unknown",
                mark = m.Mark
            }).ToList();

            return Ok(new
            {
                examName = exam.ExamName,
                subjectName = exam.Subject?.SubjectName ?? "Unknown Subject",
                highest = marks.Any() ? marks.Max(m => m.Mark) : 0,
                lowest = marks.Any() ? marks.Min(m => m.Mark) : 0,
                average = marks.Any() ? marks.Average(m => m.Mark) : 0,
                students = reportStudents
            });
        }

        // -------------------------------------------------------
        // CLASS PERFORMANCE REPORT
        // -------------------------------------------------------
        [HttpGet("class/{classId}/exam/{examId}")]
        public async Task<IActionResult> GetClassPerformance(Guid classId, Guid examId)
        {
            var classInfo = await _db.Classes
                .FirstOrDefaultAsync(c => c.ClassId == classId);

            var exam = await _db.Exams
                .Include(e => e.Subject)
                .FirstOrDefaultAsync(e => e.ExamID == examId);

            if (classInfo == null || exam == null)
                return NotFound("Class or Exam not found");

            var studentsInClass = await _db.Students
                .Where(s => s.ClassID == classId)
                .ToListAsync();

            var studentIds = studentsInClass.Select(s => s.StudentID).ToList();

            var marks = await _db.Marks
                .Where(m => m.ExamID == examId && studentIds.Contains(m.StudentID))
                .ToListAsync();

            var reportData = marks.Select(m => new
            {
                studentName = studentsInClass.FirstOrDefault(s => s.StudentID == m.StudentID)?.StudentName ?? "Unknown",
                mark = m.Mark
            }).ToList();

            double passPercent = 0;
            if (marks.Count > 0)
            {
                var passedCount = marks.Count(m => m.Mark >= 40);
                passPercent = (passedCount * 100.0) / marks.Count;
            }

            return Ok(new
            {
                className = classInfo.ClassName,
                examName = exam.ExamName,
                subjectName = exam.Subject?.SubjectName ?? "Unknown Subject",
                highest = marks.Any() ? marks.Max(m => m.Mark) : 0,
                lowest = marks.Any() ? marks.Min(m => m.Mark) : 0,
                average = marks.Any() ? marks.Average(m => m.Mark) : 0,
                passPercentage = passPercent,
                students = reportData
            });
        }
    }
}
