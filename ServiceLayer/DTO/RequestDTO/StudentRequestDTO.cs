using System;
using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.DTO.RequestDTO
{
    public class StudentRequestDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string StudentName { get; set; } = string.Empty;

        [Required]
        [Phone]
        public int PhoneNo { get; set; }

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public Guid UserID { get; set; }

        [Required]
        public Guid ClassID { get; set; }
    }
}
