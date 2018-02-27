using System;

namespace Hospital.API.Dtos
{
    public class PatientDiseasesList
    {
        public DateTime? Diagnosed { get; set; }
        public DateTime? Cured { get; set; }
        public PatientDiseaseDetailed Disease { get; set; }
    }
}