using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace RepositoryLayer.Entity
{


    public class User
    {
        public Guid UserID { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string UserName { get; set; } = string.Empty;

       
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

       
        
        public byte[] PasswordHash { get; set; } = new byte[0];


        public byte[] PasswordSalt { get; set; } = new byte[0];

        [Required]
        [StringLength(20)]
        public string Role { get; set; } = "User";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        // New fields for reset-password
        public string? ResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }

        // Optionally store AdmissionNumber (string to allow leading zeros)
        public string? AdmissionNumber { get; set; }

    }

}
