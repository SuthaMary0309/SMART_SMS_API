using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    public class Attendance
    {
        public Guid AttendanceId { get; set; } = Guid.Empty;

        public Guid ClassId { get; set; } = Guid.Empty;
        public Guid StudentId { get; set; } = Guid.Empty;
        public Guid TeacherId { get; set; } = Guid.Empty;
        public DateTime Date { get; set; }
        public DateTime Day { get; set; }
        public DateTime Time { get; set; }


    }
}
