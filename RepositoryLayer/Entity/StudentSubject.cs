using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    public class StudentSubject
    {
        public int ID { get; set; }
        public Guid StudentID { get; set; } = Guid.NewGuid();
        public Guid SubjectID { get; set; } = Guid.NewGuid();
    }
}
