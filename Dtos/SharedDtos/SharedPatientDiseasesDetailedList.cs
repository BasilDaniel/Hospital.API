using System;

namespace Hospital.API.Dtos
{
    public class SharedPatientDiseasesDetailedList
    {
        public int Id { get; set; }
        public DateTimeOffset? Diagnosed { get; set; }
        public DateTimeOffset? Cured { get; set; }
        public string Note { get; set; }
        public int? PatientId { get; set; }
        public int? DiseaseId { get; set; }
        public SharedPatientsList Patient { get; set; }
        public SharedDiseaseDetailed Disease { get; set; }
    }
}