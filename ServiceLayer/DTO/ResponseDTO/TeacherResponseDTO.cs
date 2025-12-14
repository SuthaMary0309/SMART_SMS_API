using System;

namespace ServiceLayer.DTO.ResponseDTO
{
    public class TeacherResponseDTO
    {
        public Guid TeacherID { get; set; }
        public string TeacherName { get; set; } = string.Empty;
        public string PhoneNo { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        
    }
}
