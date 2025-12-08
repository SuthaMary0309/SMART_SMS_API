using System;

namespace RepositoryLayer.Entity
{
    public class Parent
    {
        public Guid ParentID { get; set; } = Guid.NewGuid();
        public string ParentName { get; set; } = string.Empty;
        public string PhoneNo { get; set; } = string.Empty;   // string
        public string Address { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Guid? UserID { get; set; }      // nullable - set from JWT claims
        public Guid? StudentID { get; set; }   // nullable - linked to student
    }
}
