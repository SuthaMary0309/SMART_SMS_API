using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    public class Marks
    {
        public Guid MarksId { get; set; } = Guid.Empty;
        public int Grade { get; set; }
        public int Mark { get; set; }
        public Guid StudentID { get; set; } = Guid.Empty;
        public Guid ExamID { get; set; } = Guid.Empty;
        public Exam Exam { get; set; }
        public Student? Student { get; set; }

    }
}
