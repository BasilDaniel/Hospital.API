using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.API.Helpers;
using Hospital.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital.API.Data
{
    public class DatingRepository : IDatingRepository
    {
        private readonly DataContext _context;
        public DatingRepository(DataContext context)
        {
            _context = context;

        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        //Admins
        public async Task<Admin> GetAdmin(int id)
        {
            var admin = await _context.Admins.Include(d => d.Department).Include(p => p.Position).FirstOrDefaultAsync(u => u.Id == id);

            return admin;
        }

        public async Task<IEnumerable<Admin>> GetAdmins()
        {
            var admins = await _context.Admins.ToListAsync();

            return admins;
        }

        //Patients
        public async Task<Patient> GetPatient(int id)
        {
           var patient = await _context.Patients
            .Include(a => a.PatientDiseases).ThenInclude(d => d.Disease)
            .Include(a => a.Appointments).ThenInclude(s => s.Staff).ThenInclude(d => d.Department)
            .Include(a => a.Appointments).ThenInclude(s => s.Staff).ThenInclude(p => p.Position)
            .FirstOrDefaultAsync(u => u.Id == id);

            return patient;
        }

        public async Task<PagedList<Patient>> GetPatients(UserParams userParams)
        {
            var patients = _context.Patients.AsQueryable();
            string[] userParamsArray;

            if (!string.IsNullOrEmpty(userParams.Name))
            {
                userParamsArray = userParams.Name.Split((string[]) null, StringSplitOptions.RemoveEmptyEntries);
                if(userParamsArray.Length == 1){
                    patients = patients.Where(u => 
                    u.FamilyName.StartsWith(userParamsArray[0]) || 
                    u.Name.StartsWith(userParamsArray[0]) ||
                    u.MiddleName.StartsWith(userParamsArray[0])
                    );
                }
                else if(userParamsArray.Length == 2){
                    patients = patients.Where(u => 
                    u.FamilyName.StartsWith(userParamsArray[0]) || 
                    u.Name.StartsWith(userParamsArray[0]) ||
                    u.MiddleName.StartsWith(userParamsArray[0]) || 
                    u.FamilyName.StartsWith(userParamsArray[1]) || 
                    u.Name.StartsWith(userParamsArray[1]) ||
                    u.MiddleName.StartsWith(userParamsArray[1])
                    );
                }
                else if(userParamsArray.Length >= 3){
                    patients = patients.Where(u => 
                    u.FamilyName.StartsWith(userParamsArray[0]) || 
                    u.Name.StartsWith(userParamsArray[0]) ||
                    u.MiddleName.StartsWith(userParamsArray[0]) || 
                    u.FamilyName.StartsWith(userParamsArray[1]) || 
                    u.Name.StartsWith(userParamsArray[1]) ||
                    u.MiddleName.StartsWith(userParamsArray[1]) ||
                    u.FamilyName.StartsWith(userParamsArray[2]) || 
                    u.Name.StartsWith(userParamsArray[2]) ||
                    u.MiddleName.StartsWith(userParamsArray[2])
                    );
                }  
                else if(userParams.PatientId != null){
                    patients = patients.Where(i => i.Id == userParams.PatientId);
                }   
            }

            return await PagedList<Patient>.CreateAsync(patients, userParams.PageNumber, userParams.PageSize);
        }

        //Staffs
        public async Task<Staff> GetStaff(int id)
        {
            var staff = await _context.Staffs
            .Include(d => d.Department)
            .Include(p => p.Position)
            .Include(a => a.Appointments)
            .FirstOrDefaultAsync(u => u.Id == id);

            return staff;
        }

        public async Task<PagedList<Staff>> GetStaffs(UserParams userParams)
        {
            var staffs = _context.Staffs
            .Include(d => d.Department)
            .Include(p => p.Position)
            .AsQueryable();

            if (!string.IsNullOrEmpty(userParams.Position) || !string.IsNullOrEmpty(userParams.Department) || userParams.StaffId != null)
            {
                Console.WriteLine(1);
                if(userParams.StaffId != null){
                    staffs = staffs.Where(i => i.Id == userParams.StaffId);
                }
                else if(string.IsNullOrEmpty(userParams.Position))
                {
                    Console.WriteLine(2);
                    staffs = staffs.Where(u => u.Department.Name == userParams.Department);
                }
                else if(string.IsNullOrEmpty(userParams.Department))
                {
                    Console.WriteLine(3);
                    staffs = staffs.Where(u => u.Position.Name == userParams.Position);
                }                
                else                
                {
                    Console.WriteLine(4);
                    staffs = staffs.Where(u => u.Department.Name == userParams.Department || u.Position.Name == userParams.Position);
                }
            } 

            return await PagedList<Staff>.CreateAsync(staffs, userParams.PageNumber, userParams.PageSize);
        }

        //Appointments
         public async Task<Appointment> GetAppointment(int id)
        {
            var appointment = await _context.Appointments
            .Include(s => s.Staff).ThenInclude(p => p.Position)
            .Include(s => s.Staff).ThenInclude(d => d.Department)
            .Include(p => p.Patient)
            .FirstOrDefaultAsync(u => u.Id == id);

            return appointment;
        }

        public async Task<PagedList<Appointment>> GetAppointments(UserParams userParams)
        {
            var appointments = _context.Appointments
            .Include(s => s.Staff).ThenInclude(p => p.Position)
            .Include(p => p.Patient)
            .AsQueryable();

            if (userParams.DateTime.Year > 0001)
            {
                appointments = appointments.Where(u => u.DateTime == userParams.DateTime);
            } 
            else if(userParams.StaffId != null)
            {
                appointments = appointments.Where(s => s.StaffId == userParams.StaffId);
            }
            else if(userParams.PatientId != null)
            {
                appointments = appointments.Where(p => p.PatientId == userParams.PatientId);
            }

            return await PagedList<Appointment>.CreateAsync(appointments, userParams.PageNumber, userParams.PageSize);
        }

        //Departments
         public async Task<Department> GetDepartment(int id)
        {
            var department = await _context.Departments
            .Include(s => s.Staffs).ThenInclude(p => p.Position)
            .FirstOrDefaultAsync(u => u.Id == id);

            return department;
        }

        public async Task<IEnumerable<Department>> GetDepartments()
        {
            var departments = await _context.Departments
            .ToListAsync();

            return departments;
        }

        //Diseases
         public async Task<Disease> GetDisease(int id)
        {
            var disease = await _context.Diseases
            .Include(p => p.PatientDiseases).ThenInclude(p => p.Patient)
            .FirstOrDefaultAsync(u => u.Id == id);

            return disease;
        }

        public async Task<PagedList<Disease>> GetDiseases(UserParams userParams)
        {
            var diseases = _context.Diseases
            .AsQueryable();

            if (!string.IsNullOrEmpty(userParams.Name))
            {
                diseases = diseases.Where(u => 
                    u.Name.StartsWith(userParams.Name)
                );
            }

            return await PagedList<Disease>.CreateAsync(diseases, userParams.PageNumber, userParams.PageSize);
        }

        //PatientDiseases

         public async Task<PatientDisease> GetPatientDisease(int id)
        {
            var patientDisease = await _context.PatientDiseases
            .Include(d => d.Disease)
            .FirstOrDefaultAsync(u => u.Id == id);

            return patientDisease;
        }
        public async Task<PagedList<PatientDisease>> GetPatientDiseases(UserParams userParams)
        {
            var patientDiseases = _context.PatientDiseases
            .Include(p => p.Patient)
            .Include(d => d.Disease)
            .AsQueryable();

            if (userParams.PatientId != null)
            {
                patientDiseases = patientDiseases.Where(p => p.PatientId == userParams.PatientId);
            }

            return await PagedList<PatientDisease>.CreateAsync(patientDiseases, userParams.PageNumber, userParams.PageSize);
        }

        //Positions
         public async Task<Position> GetPosition(int id)
        {
            var position = await _context.Positions
            .Include(s => s.Staffs)
            .FirstOrDefaultAsync(u => u.Id == id);

            return position;
        }

        public async Task<IEnumerable<Position>> GetPositions()
        {
            var positions = await _context.Positions
            .ToListAsync();

            return positions;
        }

        public async Task<bool> SaveAll()
        {
            return await _context
            .SaveChangesAsync() > 0;
        }
    }
}