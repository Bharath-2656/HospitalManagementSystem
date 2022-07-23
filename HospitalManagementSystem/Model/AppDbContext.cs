using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Model
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Appoinment> Appoinments { get; set; }

    }
}

