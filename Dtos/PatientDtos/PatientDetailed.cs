using System;
using System.Collections.Generic;

namespace Hospital.API.Dtos
{
    public class PatientDetailed
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string FamilyName { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public DateTime Birthdate { get; set; }

        public ICollection<PatientDiseasesList> PatientDiseases{ get; set; }
        
        public ICollection<PatientAppointmentsDetailed> Appointments { get; set; }
    }
}