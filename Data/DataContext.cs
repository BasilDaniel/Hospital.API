using Hospital.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Hospital.API.Data
{
    public class DataContext : DbContext
    {
        public  DataContext(DbContextOptions<DataContext> options) : base(options) {}
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<PatientDisease> PatientDiseases { get; set; }
    }
}