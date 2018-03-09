using System;

namespace Hospital.API.Dtos
{
    public class SharedPatientDiseasesList
    {
        public int Id { get; set; }
        public DateTimeOffset? Diagnosed { get; set; }
        public DateTimeOffset? Cured { get; set; }
        public string Note { get; set; }
        public SharedDiseaseDetailed Disease { get; set; }
    }
}