using System;
using Hospital.API.Models;

namespace Hospital.API.Dtos
{
    public class PatientAppointmentsDetailed
    {
        
        public DateTimeOffset DateTime { get; set; }
         public SharedStaffsList Staff { get; set; }
    }
}