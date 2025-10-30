using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTO.ResponseDTO
{
    public class MarksResponseDTO
    {
        public Guid MarksId { get; set; } = Guid.Empty;
        public int Grade { get; set; }
        public int Mark { get; set; }
        public Guid StudentID { get; set; } = Guid.Empty;
        public Guid ExamID { get; set; } = Guid.Empty;
    }
}
