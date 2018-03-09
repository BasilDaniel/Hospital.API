using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hospital.API.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Login { get; set; }
        public string FamilyName { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public DateTimeOffset Birthdate { get; set; }

        public ICollection<PatientDisease> PatientDiseases{ get; set; }
        
        public ICollection<Appointment> Appointments { get; set; }

        public Patient()
        {
            PatientDiseases = new List<PatientDisease>();
            Appointments = new List<Appointment>();
        }
    }
}