using System;
using Hospital.API.Models;

namespace Hospital.API.Dtos
{
    public class SharedAppointmentsList
    {
        public int Id { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public int Duration { get; set; }
         public SharedStaffsList Staff { get; set; }
    }
}