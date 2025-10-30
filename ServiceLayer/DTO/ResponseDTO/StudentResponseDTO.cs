using System;

namespace ServiceLayer.DTO.ResponseDTO
{
    public class StudentResponseDTO
    {
        public Guid StudentID { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public int PhoneNo { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Guid UserID { get; set; }
        public Guid ClassID { get; set; }
    }
}
