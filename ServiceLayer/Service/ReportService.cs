using Microsoft.EntityFrameworkCore;
using RepositoryLayer.AppDbContext;
using ServiceLayer.DTO.ResponseDTO;
using ServiceLayer.ServiceInterFace;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceLayer.Service
{
    public class ReportService : IReportService
    {
        private readonly ApplicationDbContext _db;
        public ReportService(ApplicationDbContext db) => _db = db;

        public async Task<StudentReportDTO?> GenerateStudentReportAsync(Guid studentId)
        {
            var student = await _db.Students
                .Include(s => s.Class)
                .FirstOrDefaultAsync(s => s.StudentID == studentId);

            if (student == null) return null;

            var marks = await (from m in _db.Marks
                               join e in _db.Exams on m.ExamID equals e.ExamID
                               join subj in _db.Subjects on e.SubjectID equals subj.SubjectID into sj
                               from subj in sj.DefaultIfEmpty()
                               where m.StudentID == studentId
                               select new StudentMarkDTO
                               {
                                   marksId = m.MarksId,
                                   examId = e.ExamID,
                                   examName = e.ExamName,
                                   subjectId = subj != null ? subj.SubjectID : Guid.Empty,
                                   subjectName = subj != null ? subj.SubjectName : string.Empty,
                                   mark = m.Mark,
                                   grade = m.Grade
                               }).ToListAsync();

            return new StudentReportDTO
            {
                studentId = student.StudentID,
                studentName = student.StudentName,
                classId = student.ClassID,
                className = student.Class?.ClassName ?? string.Empty,
                email = student.Email ?? string.Empty,
                phone = student.PhoneNo ?? string.Empty,
                marks = marks
            };
        }
    }
}
