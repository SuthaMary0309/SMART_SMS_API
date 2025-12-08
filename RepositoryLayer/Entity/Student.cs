using System;
using System.ComponentModel.DataAnnotations;

namespace RepositoryLayer.Entity
{
    public class Student
    {
        public Guid StudentID { get; set; } = Guid.NewGuid();

        [Required]
        public string StudentName { get; set; } = string.Empty;

        [Required]
        public string PhoneNo { get; set; } = string.Empty;  // MUST BE STRING

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public Guid UserID { get; set; }
        public Guid ClassID { get; set; }
    }
}
