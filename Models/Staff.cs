using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hospital.API.Models
{
    public class Staff
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
        public TimeSpan AppointmentDuration { get; set; }
        public Position Position { get; set; }
        public int? PositionId { get; set; }
        public Department Department { get; set; }
        public int? DepartmentId { get; set; }
        
        public ICollection<Appointment> Appointments { get; set; }

        public Staff()
        {
            Appointments = new Collection<Appointment>();
        }

    }
}