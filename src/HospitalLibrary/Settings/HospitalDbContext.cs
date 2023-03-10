using EntityFramework.Exceptions.PostgreSQL;
using HospitalLibrary.Allergens;
using HospitalLibrary.AnnualLeaves;
using HospitalLibrary.Doctors;
using HospitalLibrary.Feedbacks;
using HospitalLibrary.Patients;
using HospitalLibrary.Shared.Model;
using HospitalLibrary.Appointments;
using HospitalLibrary.BloodOrders;
using HospitalLibrary.BloodStorages;
using HospitalLibrary.Buildings;
using HospitalLibrary.Examination;
using HospitalLibrary.Floors;
using HospitalLibrary.Map;
using HospitalLibrary.Hospitalizations;
using HospitalLibrary.Infrastructure.EventSourcing.Events;
using HospitalLibrary.Invitations;
using HospitalLibrary.MedicalRecords;
using HospitalLibrary.Medicines;
using HospitalLibrary.Renovations.Model;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Shared.Model.ValueObjects;
using HospitalLibrary.Symptoms;
using HospitalLibrary.Therapies.Model;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Settings
{
    public class HospitalDbContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<WorkingHours> WorkingHours { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<MapBuilding> MapBuildings { get; set; }
        public DbSet<MapFloor> MapFloors { get; set; }
        public DbSet<MapRoom> MapRooms { get; set; }
        
        public DbSet<Allergen> Allergens { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<BloodStorage> BloodStorage { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<RoomEquipment> RoomEquipment { get; set; }
        public DbSet<Hospitalization> Hospitalizations { get; set; }
        public DbSet<Therapy> Therapies { get; set; }
        public DbSet<Bed> Beds { get; set; }
        public DbSet<Users.User> Users { get; set; }
        public DbSet<AnnualLeave> AnnualLeaves { get; set; }
        
        public DbSet<BloodOrder> BloodOrders { get; set; }

        public DbSet<EquipmentReallocation> EquipmentReallocations { get; set; }
        
        public DbSet<ExaminationReport> ExaminationReports { get; set; }
        
        public DbSet<Symptom> Symptoms { get; set; }
        
        public DbSet<Prescription> Prescriptions { get; set; }
        
        public DbSet<Speciality> Specialities { get; set; }
        
        public DbSet<Advertisement.Advertisement> Advertisement { get; set; }
        public DbSet<Renovations.Model.Renovation> Renovations { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<ExaminationReportDomainEvent> ExaminationReportDomainEvents { get; set; }
    
        public DbSet<SchedulingAppointmentDomainEvent> SchedulingAppointmentDomainEvents { get; set; }
        


        public DbSet<RenovationDomainEvent> RenovationDomainEvents { get; set; }
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseExceptionProcessor();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkingHours>().HasKey(w => new { w.DoctorId, w.Day });
            modelBuilder.Entity<Person>().ToTable("Persons");
            modelBuilder.Entity<Doctor>().ToTable("Doctors");
            modelBuilder.Entity<Patient>().ToTable("Patients");
            modelBuilder.Entity<Appointment>().ToTable("Appointments");
            modelBuilder.Entity<Feedback>().ToTable("Feedbacks");
            modelBuilder.Entity<MapBuilding>().ToTable("MapBuildings");
            modelBuilder.Entity<MapFloor>().ToTable("MapFloors");
            modelBuilder.Entity<MapRoom>().ToTable("MapRooms");
            modelBuilder.Entity<Users.User>().ToTable("Users");
            modelBuilder.Entity<Rooms.Model.RoomEquipment>().ToTable("RoomEquipment");
            modelBuilder.Entity<Bed>().ToTable("Beds");
            modelBuilder.Entity<Allergen>().ToTable("Allergens");
            modelBuilder.Entity<EquipmentReallocation>().ToTable("EquipmentReallocations");
            modelBuilder.Entity<Invitation>().ToTable("Invitations");
            modelBuilder.Entity<ExaminationReportDomainEvent>().ToTable("ExaminationReportDomainEvents");
            modelBuilder.Entity<SchedulingAppointmentDomainEvent>().ToTable("SchedulingAppointmentDomainEvents");
            
            modelBuilder.Entity<Therapy>()
                .HasDiscriminator(t => t.InstanceType)
                .HasValue<BloodTherapy>("blood")
                .HasValue<MedicineTherapy>("medicine");
            modelBuilder.Entity<Person>().HasIndex(p => p.Uid).IsUnique();
            modelBuilder.Entity<Person>().HasIndex(p => p.Email).IsUnique();
            modelBuilder.Entity<Users.User>().HasIndex(u => u.Username).IsUnique();
            modelBuilder.Entity<Users.User>().HasIndex(u => u.ActivationCode).IsUnique();

            modelBuilder.Entity<ExaminationReport>().HasMany(e => e.Prescriptions).WithOne(p => p.ExaminationReport);
            modelBuilder.Entity<ExaminationReport>().HasMany(e => e.Symptoms).WithMany(p => p.ExaminationReports);
            
            modelBuilder.Entity<MapRoom>(x =>
            {
                x.Property(p => p.SecondaryCoordinatesList)
                    .HasConversion(p => (string)p, p => (CoordinatesList)p);
            });
            
            modelBuilder.Entity<RenovationDomainEvent>().ToTable("RenovationDomainEvent");

            // modelBuilder.Entity<Hospitalization>()
            //     .HasOne(h => h.Bed)
            //     .WithMany(h => h.AllHospitalizations);
            // modelBuilder.Entity<Hospitalization>()
            //     .HasOne(h => h.MedicalRecord)
            //     .WithMany(m => m.Hospitalizations);
            // modelBuilder.Entity<Hospitalization>()
            //     .HasMany(h => h.Therapies)
            //     .WithOne(t => t.Hospitalization);
        }
    }
}
