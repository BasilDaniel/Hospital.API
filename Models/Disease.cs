using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hospital.API.Models
{
    public class Disease
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<PatientDisease> PatientDiseases { get; set; }

        public Disease()
        {
            PatientDiseases = new List<PatientDisease>();
        }
    }
}