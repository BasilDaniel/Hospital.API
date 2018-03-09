using System;

namespace Hospital.API.Dtos
{
    public class SharedAppointmentDetailed
    {
        public int Id { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public string Note { get; set; }
        public TimeSpan Duration { get; set; }
        public SharedStaffsList Staff { get; set; }
        public SharedPatientsList Patient { get; set; }
    }
}