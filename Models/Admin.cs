using System;

namespace Hospital.API.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Login { get; set; }
        public string FamilyName { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public DateTime Birthdate { get; set; }

        public Position Position { get; set; }
        public int? PositionId { get; set; }

        public Department Department { get; set; }
        public int? DepartmentId { get; set; }
    }
}