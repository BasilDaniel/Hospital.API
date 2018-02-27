using System;

namespace Hospital.API.Dtos
{
    public class SharedPatientDiseaseUpdate
    {
        public DateTime? Diagnosed { get; set; }
        public DateTime? Cured { get; set; }
        public string Note { get; set; }
        public int? DiseaseId { get; set; }
    }
}