using System;

namespace Hospital.API.Dtos
{
    public class SharedPatientDiseasesList
    {
        public int Id { get; set; }
        public DateTime? Diagnosed { get; set; }
        public DateTime? Cured { get; set; }
        public string Note { get; set; }
        public SharedDiseaseDetailed Disease { get; set; }
    }
}