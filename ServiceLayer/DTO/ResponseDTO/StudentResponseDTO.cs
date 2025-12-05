using System;

namespace ServiceLayer.DTO.ResponseDTO
{
    public class StudentResponseDTO
    {
        public Guid StudentID { get; set; }
        public string StudentName { get; set; }
        public string Email { get; set; }
        public int PhoneNo { get; set; }
        public string Address { get; set; }
        public string ClassName { get; set; }
    }
}
