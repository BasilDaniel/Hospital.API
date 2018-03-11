using System;
using System.Collections.Generic;
using Hospital.API.Models;

namespace Hospital.API.Dtos
{
    public class SharedStaffDetailed
    {
       public int Id { get; set; }
        public string Login { get; set; }
        public string FamilyName { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public DateTimeOffset Birthdate { get; set; }
        public int AppointmentDuration { get; set; }
        public SharedPositionDetailed Position { get; set; }
        public SharedDepartmentDetailed Department { get; set; }
        public ICollection<SharedAppointmentsList> Appointments { get; set; } 
    }
}