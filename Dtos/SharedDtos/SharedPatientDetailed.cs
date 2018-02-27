using System;
using System.Collections.Generic;
using Hospital.API.Models;

namespace Hospital.API.Dtos
{
    public class SharedPatientDetailed
    {
        public int Id { get; set; }
        public string FamilyName { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public DateTime Birthdate { get; set; }

        public ICollection<SharedPatientDiseasesList> PatientDiseases{ get; set; }
        
        public ICollection<SharedAppointmentDetailed> Appointments { get; set; }

    }
}