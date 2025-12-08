// DTOs/ReportDTOs.cs
using System;
using System.Collections.Generic;

namespace ServiceLayer.DTO.ResponseDTO
{
    public class StudentMarkDTO
    {
        public Guid MarksId { get; set; }
        public Guid ExamID { get; set; }
        public string ExamName { get; set; } = string.Empty;
        public Guid SubjectID { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public int Mark { get; set; }
        public int Grade { get; set; }
    }

    public class StudentReportDTO
    {
        public Guid StudentID { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public Guid ClassID { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public List<StudentMarkDTO> Marks { get; set; } = new();
        public int Total => Marks.Count == 0 ? 0 : Marks.Sum(m => m.Mark);
        public double Average => Marks.Count == 0 ? 0 : Marks.Average(m => m.Mark);
        public int Highest => Marks.Count == 0 ? 0 : Marks.Max(m => m.Mark);
        public int Lowest => Marks.Count == 0 ? 0 : Marks.Min(m => m.Mark);
        public int SubjectsCount => Marks.Count;
        public string Result(int passMark = 40) => Marks.All(m => m.Mark >= passMark) ? "Pass" : "Fail";
    }

    public class ExamStudentDTO
    {
        public Guid StudentID { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public int Mark { get; set; }
    }

    public class ExamReportDTO
    {
        public Guid ExamID { get; set; }
        public string ExamName { get; set; } = string.Empty;
        public DateTime ExamDate { get; set; }
        public Guid SubjectID { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public List<ExamStudentDTO> Students { get; set; } = new();
        public double Average => Students.Count == 0 ? 0 : Students.Average(s => s.Mark);
        public int Highest => Students.Count == 0 ? 0 : Students.Max(s => s.Mark);
        public int Lowest => Students.Count == 0 ? 0 : Students.Min(s => s.Mark);
        public int TotalStudents => Students.Count;
        public int PassedCount(int passMark = 40) => Students.Count(s => s.Mark >= passMark);
    }

    public class ClassPerformanceDTO
    {
        public Guid ClassID { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public Guid ExamID { get; set; }
        public string ExamName { get; set; } = string.Empty;
        public List<ExamStudentDTO> Students { get; set; } = new();
        public double Average => Students.Count == 0 ? 0 : Students.Average(s => s.Mark);
        public int Highest => Students.Count == 0 ? 0 : Students.Max(s => s.Mark);
        public int Lowest => Students.Count == 0 ? 0 : Students.Min(s => s.Mark);
        public int TotalStudents => Students.Count;
        public int PassedCount(int passMark = 40) => Students.Count(s => s.Mark >= passMark);
        public double PassPercentage(int passMark = 40) => TotalStudents == 0 ? 0 : (PassedCount(passMark) * 100.0 / TotalStudents);
    }
}
