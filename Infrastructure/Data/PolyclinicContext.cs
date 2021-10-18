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
             modelBuilder.Entity<SubmissionForm>()
                .HasOne(s => s.ApplicationUser)
                .WithMany()
                // zbog sql servera ovdje moraš koristiti no action, u dating1 ti
                // je to restrict za oba like
                .OnDelete(DeleteBehavior.NoAction); 

             modelBuilder.Entity<Appointment>()
                .HasOne(s => s.Patient)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction); 

             modelBuilder.Entity<Examination>()
                .HasOne(s => s.Patient)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction); 

            /*  modelBuilder.Entity<DoctorSpecialization1>()
                .HasKey(x => new { x.Doctor1Id, x.Specialization1Id }); 
 */
           /*  modelBuilder.Entity<Appointment>()
                .HasKey(x => new { x.PatientId, x.ApplicationUserId }); */

            base.OnModelCreating(modelBuilder);
        }
        // ovo je za novi
        #region 
        public DbSet<Appointment1> Appointments1 { get; set; }
        public DbSet<Doctor1> Doctors1 { get; set; }
        public DbSet<DoctorSpecialization1> DoctorSpecializations { get; set; }
        public DbSet<DoctorPublication> DoctorPublications { get; set; }
        public DbSet<DoctorProfessionalAssociation> DoctorProfessionalAssociations { get; set; }
        public DbSet<Office1> Offices { get; set; }
        public DbSet<Publication1> Publications { get; set; }
        public DbSet<ProfessionalAssociation> ProfessionalAssociations { get; set; }
        public DbSet<Patient1> Patients1 { get; set; }
        public DbSet<Specialization1> Specializations { get; set; }
        public DbSet<Subspecialization1> Subspecializations { get; set; }
        #endregion 





        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Examination> Examinations { get; set; }
        public DbSet<MedicalChart> MedicalCharts { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PrefferedTimeOfExamination> PrefferedTimeOfExaminations { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<SubmissionForm> SubmissionForms { get; set; }
    }
}



