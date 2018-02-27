using System.Collections.Generic;
using System.Threading.Tasks;
using Hospital.API.Helpers;
using Hospital.API.Models;

namespace Hospital.API.Data
{
    public interface IDatingRepository
    {
        void Add<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;
        Task<bool> SaveAll();
        Task<IEnumerable<Admin>> GetAdmins();
        Task<Admin> GetAdmin(int id);
        Task<PagedList<Staff>> GetStaffs(UserParams userParams);
        Task<Staff> GetStaff(int id);
        Task<PagedList<Patient>> GetPatients(UserParams userParams);
        Task<Patient> GetPatient(int id);
        Task<PagedList<Appointment>> GetAppointments(UserParams userParams);
        Task<Appointment> GetAppointment(int id);
        Task<IEnumerable<Department>> GetDepartments();
        Task<Department> GetDepartment(int id);
        Task<PagedList<Disease>> GetDiseases(UserParams userParams);
        Task<Disease> GetDisease(int id);
        Task<PagedList<PatientDisease>> GetPatientDiseases(UserParams userParams);
        Task<PatientDisease> GetPatientDisease(int id);        
        Task<IEnumerable<Position>> GetPositions();
        Task<Position> GetPosition(int id);
    }
}