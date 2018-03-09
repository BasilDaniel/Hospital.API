using System;

namespace Hospital.API.Dtos
{
    public class SharedPatientDiseaseUpdate
    {
        public DateTimeOffset? Diagnosed { get; set; }
        public DateTimeOffset? Cured { get; set; }
        public string Note { get; set; }
        public int? DiseaseId { get; set; }
    }
}