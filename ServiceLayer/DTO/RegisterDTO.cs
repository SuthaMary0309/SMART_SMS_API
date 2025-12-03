using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTO
{
    public class RegisterDTO
    {
        public string UserName { get; set; }         // optional, but we will override with auto username
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Role { get; set; }             // Admin, Teacher, Parent, Student
        public string? AdmissionNumber { get; set; } // optional: if provided use it, else generate
    }


}
