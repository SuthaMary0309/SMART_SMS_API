using System;
using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.DTO.RequestDTO
{
    public class SubjectRequestDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string SubjectName { get; set; } = string.Empty;

        [Required]
        public Guid StudentID { get; set; }

        [Required]
        public Guid ClassID { get; set; }

        [Required]
        public Guid UserID { get; set; }

        [Required]
        public Guid TeacherID { get; set; }
    }
}
