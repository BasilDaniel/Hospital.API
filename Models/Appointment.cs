using System;

namespace Hospital.API.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public string Note { get; set; }
        public TimeSpan Duration { get; set; }

        public Staff Staff { get; set; }
        public int? StaffId { get; set; }
        public Patient Patient { get; set; }
        public int? PatientId { get; set; }
    }
}