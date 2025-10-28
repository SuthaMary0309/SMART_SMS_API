using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    public class Parent
    {
        public Guid ParentID { get; set; } = Guid.Empty;
        public string ParentName { get; set; } = string.Empty;
        public int PhoneNo { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public Guid UserID { get; set; } = Guid.Empty;
        public Guid StudentID { get; set; } = Guid.Empty;
    }
}
