using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTO
{

    public class AttendanceRequestDTO
    {
        public Guid StudentId { get; set; }
        public Guid TeacherId { get; set; }
        public Guid ClassId { get; set; }

        // Accept date as yyyy-MM-dd string, then parse
        public string Date { get; set; }
        public string Time { get; set; }   // optional "HH:mm"
        public string Status { get; set; } // "Present" / "Absent"

        // optional for sheet readability
        public string StudentName { get; set; }
        public string TeacherName { get; set; }
        public string ClassName { get; set; }
    }
}
    


    
