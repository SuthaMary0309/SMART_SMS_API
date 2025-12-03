using System;
using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.DTO.RequestDTO
{
    public class StudentRequestDTO
    {
        [Required]
        public string StudentName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public int PhoneNo { get; set; }

        public Guid ClassID { get; set; }

    }
}
