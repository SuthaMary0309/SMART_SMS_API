using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    public class StudentTeacher
    {
        public int ID { get; set; }
        public Guid TeacherID { get; set; } = Guid.NewGuid();
        public Guid StudentID { get; set; } = Guid.NewGuid();
    }
}
