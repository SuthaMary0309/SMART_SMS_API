using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTO.ResponseDTO
{
    public class ClassResponseDTO
    {
        public Guid ClassId { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public string Grade { get; set; }
    }
}
