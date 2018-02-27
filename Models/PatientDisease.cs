using System;

namespace Hospital.API.Models
{
    public class PatientDisease
    {
        public int Id { get; set; }
        public DateTime? Diagnosed { get; set; }
        public DateTime? Cured { get; set; }
        public string Note { get; set; }
        public int? PatientId { get; set; }
        public Patient Patient { get; set; }
        public int? DiseaseId { get; set; }
        public Disease Disease { get; set; }
    }
}