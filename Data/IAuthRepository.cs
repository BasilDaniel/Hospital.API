using System.Threading.Tasks;
using Hospital.API.Models;

namespace Hospital.API.Data
{
    public interface IAuthRepository
    {
         Task<Staff> RegisterStaff(Staff staff, string password);
         Task<Staff> LoginStaff(string name, string password);
         Task<bool> StaffExists(string login);


         Task<Patient> RegisterPatient(Patient patient, string password);
         Task<Patient> LoginPatient(string name, string password);
         Task<bool> PatientExists(string login);

         Task<Admin> RegisterAdmin(Admin admin, string password);
         Task<Admin> LoginAdmin(string name, string password);
         Task<bool> AdminExists(string login);
    }
}