using System;

namespace Hospital.API.Dtos
{
    public class PatientAppointmentCreate
    {
        public DateTimeOffset DateTime { get; set; }
        public int? StaffId { get; set; }
        public int? PatientId { get; set; }
    }
}