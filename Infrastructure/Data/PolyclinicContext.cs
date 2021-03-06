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

              /*   modelBuilder.Entity<MedicalRecord>()
                .HasOne(s => s.Patient)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction); 

              modelBuilder.Entity<MedicalRecord>()
                .HasOne(s => s.Office)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);     */

              modelBuilder.Entity<Rating>()
                .HasOne(s => s.Patient)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);   

              modelBuilder.Entity<DoctorSpecialization2>()
                .HasKey(x => new { x.Doctor1Id, x.Specialization1Id }); 
 
           /*  modelBuilder.Entity<Appointment>()
                .HasKey(x => new { x.PatientId, x.ApplicationUserId }); */

              modelBuilder.Entity<MedicalRecord1>(entity =>
            {
                entity.HasKey(z => z.Appointment1Id);
                entity.HasOne(p => p.Appointment)
                     .WithOne(a => a.MedicalRecord1)
                     .HasForeignKey<MedicalRecord1>(a => a.Appointment1Id);
            });

            base.OnModelCreating(modelBuilder);
        }
        // ovo je za novi
        #region 
        public DbSet<Appointment1> Appointments1 { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Doctor1> Doctors1 { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<DoctorSpecialization1> DoctorSpecializations { get; set; }
        public DbSet<DoctorSpecialization2> DoctorSpecializations2 { get; set; }
        public DbSet<DoctorPublication> DoctorPublications { get; set; }
        public DbSet<DoctorProfessionalAssociation> DoctorProfessionalAssociations { get; set; }
        public DbSet<HospitalAffiliation> HospitalAffiliations { get; set; }
       // public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<MedicalRecord1> MedicalRecords1 { get; set; }
        public DbSet<Office1> Offices { get; set; }
        public DbSet<Publication1> Publications { get; set; }
        public DbSet<ProfessionalAssociation> ProfessionalAssociations { get; set; }
        public DbSet<Patient1> Patients1 { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Specialization1> Specializations { get; set; }
        public DbSet<Subspecialization1> Subspecializations { get; set; }
        #endregion 





        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Examination> Examinations { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PrefferedTimeOfExamination> PrefferedTimeOfExaminations { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<SubmissionForm> SubmissionForms { get; set; }
    }
}



