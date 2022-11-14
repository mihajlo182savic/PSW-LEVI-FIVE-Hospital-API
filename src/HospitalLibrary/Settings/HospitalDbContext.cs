﻿using EntityFramework.Exceptions.PostgreSQL;
using HospitalLibrary.Allergens;
using HospitalLibrary.Doctors;
using HospitalLibrary.Feedbacks;
using HospitalLibrary.Patients;
using HospitalLibrary.Shared.Model;
using HospitalLibrary.Appointments;
using HospitalLibrary.BloodStorages;
using HospitalLibrary.Buildings;
using HospitalLibrary.Floors;
using HospitalLibrary.Map;
using HospitalLibrary.Feedbacks.Dtos;
using HospitalLibrary.Hospitalizations;
using HospitalLibrary.MedicalRecords;
using HospitalLibrary.Medicines;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Therapies;
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
        public DbSet<Rooms.Model.RoomEquipment> RoomEquipment { get; set; }
        public DbSet<Hospitalization> Hospitalizations { get; set; }
        public DbSet<Therapy> Therapies { get; set; }
        public DbSet<Bed> Beds { get; set; }

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
            modelBuilder.Entity<Rooms.Model.RoomEquipment>().ToTable("RoomEquipment");
            modelBuilder.Entity<Bed>().ToTable("Beds");
            
            modelBuilder.Entity<Therapy>()
                .HasDiscriminator<string>("therapy_type")
                .HasValue<BloodTherapy>("blood")
                .HasValue<MedicineTherapy>("medicine");
        }
    }
}