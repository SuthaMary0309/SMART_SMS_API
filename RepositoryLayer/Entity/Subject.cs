using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    public class Subject
    {

        public Guid SubjectID { get; set; } = Guid.Empty;

        public String SubjectName { get; set; } = string.Empty;

        public Guid StudentID { get; set; } = Guid.Empty;

        public Guid ClassID { get; set; } = Guid.Empty;

        public Guid UserID { get; set; } = Guid.Empty;

        public Guid TeacherID { get; set; } = Guid.Empty;
    }
}
