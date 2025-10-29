using System;

namespace ServiceLayer.DTO.ResponseDTO
{
    public class SubjectResponseDTO
    {
        public Guid SubjectID { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public Guid StudentID { get; set; }
        public Guid ClassID { get; set; }
        public Guid UserID { get; set; }
        public Guid TeacherID { get; set; }
    }
}
