using AutoMapper;
using Hospital.API.Dtos;
using Hospital.API.Models;

namespace Hospital.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //DTO For Admin
            CreateMap<Admin, AdminsList>();
            CreateMap<Admin, AdminDetailed>();
            CreateMap<AdminUpdate, Admin>();
            CreateMap<AdminStaffUpdate, Staff>();
            CreateMap<AdminPatientUpdate, Patient>();
            CreateMap<AdminAppointmentUpdate, Appointment>();
            CreateMap<AdminDepartmentUpdate, Department>();
            CreateMap<AdminPositionUpdate, Position>();
            CreateMap<AdminDiseaseUpdate, Disease>();
            CreateMap<AdminAppointmentCreate, Appointment>();
            CreateMap<AdminDepartmentCreate, Department>();
            CreateMap<AdminPositionCreate, Position>();
            CreateMap<AdminDiseaseCreate, Disease>();

            //DTO For Shared
            CreateMap<SharedPatientDiseaseUpdate, PatientDisease>();
            CreateMap<PatientDisease, SharedPatientDiseasesDetailedList>();
            CreateMap<Position, SharedPositionDetailed>();
            CreateMap<Department, SharedDepartmentDetailed>();
            CreateMap<Patient, SharedPatientDetailed>();
            CreateMap<Staff, SharedStaffsList>();
            CreateMap<Staff, SharedStaffDetailed>();
            CreateMap<Appointment, SharedAppointmentDetailed>();
            CreateMap<Disease, SharedDiseaseDetailed>();
            CreateMap<Disease, SharedDiseasesList>();
            CreateMap<PatientDisease, SharedPatientDiseasesList>();
            CreateMap<Appointment, SharedAppointmentsList>();
            CreateMap<Patient, SharedPatientsList>();
            CreateMap<Position, SharedPositionsList>();
            CreateMap<Department, SharedDepartmentsList>();
            CreateMap<SharedPatientDiseaseCreate, PatientDisease>();
            
            //DTO For Staff
            CreateMap<Staff, PatientStaffDetailed>();
            CreateMap<StaffUpdate, Staff>();
            CreateMap<StaffPatientUpdate, Patient>();
            
            //DTO For Patient
            CreateMap<Patient, PatientDetailed>();
            CreateMap<PatientDisease, PatientDiseasesList>();
            CreateMap<Disease, PatientDiseaseDetailed>();
            CreateMap<Appointment, PatientAppointmentsDetailed>();
            CreateMap<Appointment, PatientStaffAppointmentsDetailed>();
            CreateMap<PatientUpdate, Patient>();
            CreateMap<PatientAppointmentCreate, Appointment>();
            
        }
    }
}