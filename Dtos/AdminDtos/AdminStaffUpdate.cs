using System;

namespace Hospital.API.Dtos
{
    public class AdminStaffUpdate
    {
        
        public string FamilyName { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public DateTimeOffset Birthdate { get; set; }
        public TimeSpan AppointmentDuration { get; set; }
        public int? PositionId { get; set; }
        public int? DepartmentId { get; set; }
    }
}