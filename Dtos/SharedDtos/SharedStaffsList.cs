using System;
using System.Collections.Generic;
using Hospital.API.Models;

namespace Hospital.API.Dtos
{
    public class SharedStaffsList
    {
        public int Id { get; set; }
        public string FamilyName { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public SharedPositionsList Position { get; set; }
        public SharedDepartmentsList Department { get; set; }
        
    }
}