using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceLayer.DTO.ResponseDTO
{
    public class StudentMarkDTO
    {
        public Guid marksId { get; set; }
        public Guid examId { get; set; }
        public string examName { get; set; } = string.Empty;
        public Guid subjectId { get; set; }
        public string subjectName { get; set; } = string.Empty;
        public int mark { get; set; }
        public int grade { get; set; }
    }

    public class StudentReportDTO
    {
        public Guid studentId { get; set; }
        public string studentName { get; set; } = string.Empty;
        public Guid? classId { get; set; }
        public string className { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string phone { get; set; } = string.Empty;
        public List<StudentMarkDTO> marks { get; set; } = new();

        public int total => marks.Count == 0 ? 0 : marks.Sum(m => m.mark);
        public double average => marks.Count == 0 ? 0 : marks.Average(m => m.mark);
        public int highest => marks.Count == 0 ? 0 : marks.Max(m => m.mark);
        public int lowest => marks.Count == 0 ? 0 : marks.Min(m => m.mark);
        public int subjectsCount => marks.Count;
        public string result(int passMark = 40) => marks.All(m => m.mark >= passMark) ? "Pass" : "Fail";
    }
}
