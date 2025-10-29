using System;
using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.DTO.RequestDTO
{
    public class UserRequestDTO
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string UserName { get; set; } = string.Empty;

        [Range(13, 120)]
        public int Age { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Role { get; set; } = "User";
    }
}
