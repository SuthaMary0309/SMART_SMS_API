using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTO.ResponseDTO
{
    public class ParentResponseDTO
    {
        public Guid ParentID { get; set; } = Guid.Empty;
        public string ParentName { get; set; } = string.Empty;
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public Guid UserID { get; set; } = Guid.Empty;
        public Guid StudentID { get; set; } = Guid.Empty;
    }
}
