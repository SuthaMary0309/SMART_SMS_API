using System;

namespace ServiceLayer.DTO.ResponseDTO
{
    public class ParentResponseDTO
    {
        public Guid ParentID { get; set; }
        public string ParentName { get; set; } = string.Empty;
        public string PhoneNo { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Guid? UserID { get; set; }
        public Guid? StudentID { get; set; }
    }
}
