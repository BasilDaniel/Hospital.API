using System.Collections.Generic;
using System.Linq;
using Hospital.API.Models;
using Newtonsoft.Json;

namespace Hospital.API.Data
{
    public class Seed
    {
        private readonly DataContext _context;
        public Seed(DataContext context)
        {
            _context = context;

        }

        public void SeedData()
        {
            // _context.Staffs.RemoveRange(_context.Staffs);
            // _context.SaveChanges();
            // _context.Positions.RemoveRange(_context.Positions);
            // _context.SaveChanges();

            if(!_context.Admins.Any())
            {
                    //Departments
                var departmentData = System.IO.File.ReadAllText("Data/DepartmentSeedData.json");
                var departments = JsonConvert.DeserializeObject<List<Department>>(departmentData);
                foreach (var department in departments)
                {
                _context.Departments.Add(department);
                }
                _context.SaveChanges();

                //Positions
                var positonData = System.IO.File.ReadAllText("Data/PositionSeedData.json");
                var positions = JsonConvert.DeserializeObject<List<Position>>(positonData);
                foreach (var position in positions)
                {
                _context.Positions.Add(position);
                }
                _context.SaveChanges();

                //Diseases
                var diseaseData = System.IO.File.ReadAllText("Data/DiseaseSeedData.json");
                var diseases = JsonConvert.DeserializeObject<List<Disease>>(diseaseData);
                foreach (var disease in diseases)
                {
                _context.Diseases.Add(disease);
                }
                _context.SaveChanges();

                //Patients
                var patientData = System.IO.File.ReadAllText("Data/PatientSeedData.json");
                var patients = JsonConvert.DeserializeObject<List<Patient>>(patientData);
                foreach (var patient in patients)
                {
                    // create the password hash
                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHash("пароль", out passwordHash, out passwordSalt);

                    patient.PasswordHash = passwordHash;
                    patient.PasswordSalt = passwordSalt;
                    patient.Name = patient.Name.ToLower();

                _context.Patients.Add(patient);
                }
                _context.SaveChanges();

                //Staffs
                var staffData = System.IO.File.ReadAllText("Data/StaffSeedData.json");
                var staffs = JsonConvert.DeserializeObject<List<Staff>>(staffData);
                foreach (var staff in staffs)
                {
                    // create the password hash
                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHash("пароль", out passwordHash, out passwordSalt);

                    staff.PasswordHash = passwordHash;
                    staff.PasswordSalt = passwordSalt;
                    staff.Name = staff.Name.ToLower();

                    _context.Staffs.Add(staff);
                }
                _context.SaveChanges();

                //Admins
                var adminData = System.IO.File.ReadAllText("Data/AdminSeedData.json");
                var admins = JsonConvert.DeserializeObject<List<Admin>>(adminData);
                foreach (var admin in admins)
                {
                    // create the password hash
                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHash("admin", out passwordHash, out passwordSalt);

                    admin.PasswordHash = passwordHash;
                    admin.PasswordSalt = passwordSalt;
                    admin.Name = admin.Name.ToLower();

                    _context.Admins.Add(admin);
                }
                _context.SaveChanges();

                //Appointments
                var appointmentData = System.IO.File.ReadAllText("Data/AppointmentSeedData.json");
                var appointments = JsonConvert.DeserializeObject<List<Appointment>>(appointmentData);
                foreach (var appointment in appointments)
                {
                _context.Appointments.Add(appointment);
                }
                _context.SaveChanges();

                //PatientsDiseases
                var patientDiseaseData = System.IO.File.ReadAllText("Data/PatientDiseaseSeedData.json");
                var patientDiseases = JsonConvert.DeserializeObject<List<PatientDisease>>(patientDiseaseData);
                foreach (var patientDisease in patientDiseases)
                {
                _context.PatientDiseases.Add(patientDisease);
                }
                _context.SaveChanges();
            }

           
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
           using (var hmac = new System.Security.Cryptography.HMACSHA512())
           {
               passwordSalt = hmac.Key;
               passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
           }
        }
    }
}