using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    public class StudentExam
    {
        public int ID { get; set; }
        public Guid StudentID { get; set; } = Guid.NewGuid();
        public Guid ExamID { get; set; } = Guid.NewGuid();
    }
}
