using System;
using System.Threading.Tasks;
using Hospital.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;

        }


        // Staff
        public async Task<bool> StaffExists(string login)
        {
            if (await _context.Staffs.AnyAsync(x => x.Login == login))
                return true;

            return false;
        }

        public async Task<Staff> RegisterStaff(Staff staff, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            staff.PasswordHash = passwordHash;
            staff.PasswordSalt = passwordSalt;

            await _context.Staffs.AddAsync(staff);
            await _context.SaveChangesAsync();

            return staff;
        }

        public async Task<Staff> LoginStaff(string name, string password)
        {
            var staff = await _context.Staffs.FirstOrDefaultAsync(x => x.Name == name);
            
            if (staff == null)
                return null;

            if (!VerifyPasswordHash(password, staff.PasswordHash, staff.PasswordSalt))
                return null;

            return staff;
        }


        // Patient
        public async Task<bool> PatientExists(string login)
        {
            if (await _context.Patients.AnyAsync(x => x.Login == login))
                return true;

            return false;
        }

        public async Task<Patient> RegisterPatient(Patient patient, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            patient.PasswordHash = passwordHash;
            patient.PasswordSalt = passwordSalt;

            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();

            return patient;
        }

        public async Task<Patient> LoginPatient(string name, string password)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(x => x.Name == name);
            
            if (patient == null)
                return null;

            if (!VerifyPasswordHash(password, patient.PasswordHash, patient.PasswordSalt))
                return null;

            return patient;
        }


        // Admin
         public async Task<bool> AdminExists(string login)
        {
            if (await _context.Admins.AnyAsync(x => x.Login == login))
                return true;

            return false;
        }

        public async Task<Admin> RegisterAdmin(Admin admin, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            admin.PasswordHash = passwordHash;
            admin.PasswordSalt = passwordSalt;

            await _context.Admins.AddAsync(admin);
            await _context.SaveChangesAsync();

            return admin;
        }

        public async Task<Admin> LoginAdmin(string name, string password)
        {
            var admin = await _context.Admins.FirstOrDefaultAsync(x => x.Name == name);
            
            if (admin == null)
                return null;

            if (!VerifyPasswordHash(password, admin.PasswordHash, admin.PasswordSalt))
                return null;

            return admin;
        }



        // Password
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
           using (var hmac = new System.Security.Cryptography.HMACSHA512())
           {
               passwordSalt = hmac.Key;
               passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
           }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
           {
               var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
               for(int i = 0; i < computedHash.Length; i++)
               {
                   if(computedHash[i] != passwordHash[i]) return false;
               }
               return true;
           }
        }
    }
}