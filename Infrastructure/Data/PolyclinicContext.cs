using Core.Entities;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class PolyclinicContext : IdentityDbContext<ApplicationUser, ApplicationRole, int, 
        IdentityUserClaim<int>, ApplicationUserRole, IdentityUserLogin<int>, 
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public PolyclinicContext(DbContextOptions<PolyclinicContext> options) : base(options)
        {          
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DoctorDepartment>()
                .HasKey(x => new { x.DoctorId, x.DepartmentId });

            modelBuilder.Entity<PolyclinicDepartment>()
                .HasKey(x => new { x.PolyclinicId, x.DepartmentId });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DoctorDepartment> DoctorDepartments { get; set; }
        public DbSet<MedicalChart> MedicalCharts { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Polyclinic> Polyclinics { get; set; }
        public DbSet<PrefferedTimeOfExamination> PrefferedTimeOfExaminations { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<SubmissionForm> SubmissionForms { get; set; }
    }
}



