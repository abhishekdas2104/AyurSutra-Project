
using Microsoft.EntityFrameworkCore;
using AyurSutra.Models;

namespace AyurSutra.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Therapy> Therapies { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<TreatmentRecord> TreatmentRecords { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
    }
}
