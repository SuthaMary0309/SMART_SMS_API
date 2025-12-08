// Service/ReportService.cs
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

        public ReportService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<StudentReportDTO?> GenerateStudentReportAsync(Guid studentId)
        {
            var student = await _db.Students.FirstOrDefaultAsync(s => s.StudentID == studentId);
            if (student == null) return null;

            var cls = await _db.Classes.FirstOrDefaultAsync(c => c.ClassId == student.ClassID);

            var marks = await (from m in _db.Marks
                               join e in _db.Exams on m.ExamID equals e.ExamID
                               join subj in _db.Subjects on e.SubjectID equals subj.SubjectID into sj
                               from subj in sj.DefaultIfEmpty()
                               where m.StudentID == studentId
                               select new StudentMarkDTO
                               {
                                   MarksId = m.MarksId,
                                   ExamID = e.ExamID,
                                   ExamName = e.ExamName,
                                   SubjectID = subj != null ? subj.SubjectID : Guid.Empty,
                                   SubjectName = subj != null ? subj.SubjectName : string.Empty,
                                   Mark = m.Mark,
                                   Grade = m.Grade
                               }).ToListAsync();

            return new StudentReportDTO
            {
                StudentID = student.StudentID,
                StudentName = student.StudentName,
                ClassID = student.ClassID,
                ClassName = cls?.ClassName ?? string.Empty,
                Marks = marks
            };
        }

        public async Task<ExamReportDTO?> GenerateExamReportAsync(Guid examId)
        {
            var exam = await _db.Exams.FirstOrDefaultAsync(e => e.ExamID == examId);
            if (exam == null) return null;

            var subj = await _db.Subjects.FirstOrDefaultAsync(s => s.SubjectID == exam.SubjectID);

            var studentMarks = await (from m in _db.Marks
                                      join st in _db.Students on m.StudentID equals st.StudentID
                                      where m.ExamID == examId
                                      select new ExamStudentDTO
                                      {
                                          StudentID = st.StudentID,
                                          StudentName = st.StudentName,
                                          Mark = m.Mark
                                      }).ToListAsync();

            return new ExamReportDTO
            {
                ExamID = exam.ExamID,
                ExamName = exam.ExamName,
                ExamDate = exam.ExamDate,
                SubjectID = exam.SubjectID,
                SubjectName = subj?.SubjectName ?? string.Empty,
                Students = studentMarks
            };
        }

        public async Task<ClassPerformanceDTO?> GenerateClassPerformanceAsync(Guid classId, Guid examId)
        {
            var cls = await _db.Classes.FirstOrDefaultAsync(c => c.ClassId == classId);
            var exam = await _db.Exams.FirstOrDefaultAsync(e => e.ExamID == examId);
            if (cls == null || exam == null) return null;

            var studentMarks = await (from st in _db.Students
                                      join m in _db.Marks on st.StudentID equals m.StudentID
                                      where st.ClassID == classId && m.ExamID == examId
                                      select new ExamStudentDTO
                                      {
                                          StudentID = st.StudentID,
                                          StudentName = st.StudentName,
                                          Mark = m.Mark
                                      }).ToListAsync();

            return new ClassPerformanceDTO
            {
                ClassID = classId,
                ClassName = cls.ClassName,
                ExamID = examId,
                ExamName = exam.ExamName,
                Students = studentMarks
            };
        }
    }
}
