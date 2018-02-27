using System;
using Hospital.API.Models;

namespace Hospital.API.Dtos
{
    public class StaffUpdate
    {
        public string FamilyName { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public DateTime Birthdate { get; set; }
    }
}