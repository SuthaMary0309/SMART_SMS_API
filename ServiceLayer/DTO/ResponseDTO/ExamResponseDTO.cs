using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTO.ResponseDTO
{
    public class ExamResponseDTO
    {
        public Guid ExamID { get; set; }
        public Guid ClassID { get; set; }
        public Guid SubjectID { get; set; }
        public string ExamName { get; set; }
        public DateTime ExamDate { get; set; }
    }
}
