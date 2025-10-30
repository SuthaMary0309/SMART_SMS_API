using System;
using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.DTO.RequestDTO
{
    public class TeacherRequestDTO
    {
        [Required]
        public string TeacherName { get; set; } = string.Empty;

        [Required]
        public string PhoneNo { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public Guid UserID { get; set; }
    }
}
