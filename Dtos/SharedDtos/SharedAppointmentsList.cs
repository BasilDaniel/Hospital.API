using System;
using Hospital.API.Models;

namespace Hospital.API.Dtos
{
    public class SharedAppointmentsList
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public TimeSpan Duration { get; set; }
         public SharedStaffsList Staff { get; set; }
    }
}