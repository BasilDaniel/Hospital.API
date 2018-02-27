using System.Collections.Generic;

namespace Hospital.API.Dtos
{
    public class PatientStaffDetailed
    {
        public string FamilyName { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public SharedPositionsList Position { get; set; }
        public SharedDepartmentsList Department { get; set; }
        public ICollection<PatientStaffAppointmentsDetailed> Appointments { get; set; }
    }
}