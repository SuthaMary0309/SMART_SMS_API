using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    public class TeacherClass
    {
        public int ID { get; set; }
        public Guid TeacherID { get; set; } = Guid.NewGuid();
        public Guid ClassID { get; set; } = Guid.NewGuid();
    }
}
