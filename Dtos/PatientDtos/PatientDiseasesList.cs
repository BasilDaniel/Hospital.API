using System;

namespace Hospital.API.Dtos
{
    public class PatientDiseasesList
    {
        public DateTimeOffset? Diagnosed { get; set; }
        public DateTimeOffset? Cured { get; set; }
        public PatientDiseaseDetailed Disease { get; set; }
    }
}