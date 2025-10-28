using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    public class Student
    {

        public Guid StudentID { get; set; } = Guid.Empty;

        public string StudentName { get; set; } = string.Empty;
        public int PhoneNo { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public Guid UserID { get; set; } = Guid.Empty;
        public Guid ClassID { get; set; } = Guid.Empty;
    }
}
