using System;

namespace Hospital.API.Dtos
{
    public class PatientAppointmentCreate
    {
        public DateTime DateTime { get; set; }
        public int? StaffId { get; set; }
        public int? PatientId { get; set; }
    }
}