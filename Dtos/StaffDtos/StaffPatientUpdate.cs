using System;

namespace Hospital.API.Dtos
{
    public class StaffPatientUpdate
    {
        public string FamilyName { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public DateTime Birthdate { get; set; }
    }
}