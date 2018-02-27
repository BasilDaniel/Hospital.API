using System;

namespace Hospital.API.Dtos
{
    public class AdminAppointmentCreate
    {
        public DateTime DateTime { get; set; }
        public string Note { get; set; }
        public TimeSpan Duration { get; set; }
        public int? StaffId { get; set; }
        public int? PatientId { get; set; }
    }
}