using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTO.RequestDTO
{
    internal class UserRequestDTO
    {
        [Required]
        [StringLength(50)]
        public string UserName { get; set; } = string.Empty;

        [Range(5, 100, ErrorMessage = "Age must be between 5 and 100")]
        public int Age { get; set; }
    }
}
