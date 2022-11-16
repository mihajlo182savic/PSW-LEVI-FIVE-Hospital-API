﻿using HospitalLibrary.Allergens;
using HospitalLibrary.Allergens.Interfaces;
using HospitalLibrary.AnnualLeaves;
using HospitalLibrary.AnnualLeaves.Interfaces;
using HospitalLibrary.Appointments;
using HospitalLibrary.Appointments.Interfaces;
using HospitalLibrary.BloodStorages;
using HospitalLibrary.BloodStorages.Interfaces;
using HospitalLibrary.Buildings;
using HospitalLibrary.Buildings.Interfaces;
using HospitalLibrary.Doctors;
using HospitalLibrary.Doctors.Interfaces;
using HospitalLibrary.Feedbacks;
using HospitalLibrary.Feedbacks.Interfaces;
using HospitalLibrary.Floors;
using HospitalLibrary.Floors.Interfaces;
using HospitalLibrary.Hospitalizations;
using HospitalLibrary.Hospitalizations.Interfaces;
using HospitalLibrary.Map;
using HospitalLibrary.Map.Interfaces;
using HospitalLibrary.MedicalRecords;
using HospitalLibrary.MedicalRecords.Interfaces;
using HospitalLibrary.Medicines;
using HospitalLibrary.Medicines.Interfaces;
using HospitalLibrary.Patients;
using HospitalLibrary.Patients.Interfaces;
using HospitalLibrary.Rooms;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Rooms.Repositories;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Shared.Model;
using HospitalLibrary.Therapies;
using HospitalLibrary.Therapies.Interfaces;
using HospitalLibrary.Users;
using HospitalLibrary.Users.Interfaces;

namespace HospitalLibrary.Shared.Repository
{
    public class UnitOfWork: IUnitOfWork
    {

        private HospitalDbContext _dataContext;
        
        private IRoomRepository _roomRepository;
        private IFeedbackRepository _feedbackRepository;
        private IDoctorRepository _doctorRepository;
        private IPatientRepository _patientRepository;
        private IAppointmentRepository _appointmentRepository;
        private IWorkingHoursRepository _workingHoursRepository;
        private IFloorRepository _floorRepository;
        private IBuildingRepository _buildingRepository;
        private IMapBuildingRepository _mapBuildingRepository;
        private IMapFloorRepository _mapFloorRepository;
        private IMapRoomRepository _mapRoomRepository;
        private IAllergenRepository _allergenRepository;
        private IMedicineRepository _medicineRepository;
        private IMedicalRecordRepository _medicalRecordRepository;
        private IHospitalizationRepository _hospitalizationRepository;
        private ITherapyRepository _therapyRepository;
        private IBloodStorageRepository _bloodStorageRepository;
        private IRoomEquipmentRepository _roomEquipmentRepository;
        private IBedRepository _bedRepository;
        private IAnnualLeaveRepository _annualLeaveRepository;
        private IUserRepository _userRepository;

        public UnitOfWork(HospitalDbContext dataContext)
        {
            _dataContext = dataContext;
        }
        public void Dispose()
        {
            _dataContext.Dispose();
        }

        public IRoomRepository RoomRepository => _roomRepository ??=  new RoomRepository(_dataContext);
        public IFeedbackRepository FeedbackRepository => _feedbackRepository ??= new FeedbackRepository(_dataContext);
        public IDoctorRepository DoctorRepository => _doctorRepository ??= new DoctorRepository(_dataContext);
        public IPatientRepository PatientRepository => _patientRepository ??= new PatientRepository(_dataContext);
        public IAppointmentRepository AppointmentRepository => _appointmentRepository ??= new AppointmentRepository(_dataContext);
        public IWorkingHoursRepository WorkingHoursRepository =>
            _workingHoursRepository ??= new WorkingHoursRepository(_dataContext);

        public IFloorRepository FloorRepository => _floorRepository ??= new FloorRepository(_dataContext);
        public IBuildingRepository BuildingRepository => _buildingRepository ??= new BuildingRepository(_dataContext);

        public IMapBuildingRepository MapBuildingRepository =>
            _mapBuildingRepository ??= new MapBuildingRepository(_dataContext);
        public IMapFloorRepository MapFloorRepository =>
            _mapFloorRepository ??= new MapFloorRepository(_dataContext);
        public IMapRoomRepository MapRoomRepository =>
            _mapRoomRepository ??= new MapRoomRepository(_dataContext);

        public IAllergenRepository AllergenRepository => _allergenRepository ??= new AllergenRepository(_dataContext);
        public IMedicineRepository MedicineRepository => _medicineRepository ??= new MedicineRepository(_dataContext);
        public IMedicalRecordRepository MedicalRecordRepository => _medicalRecordRepository ??= new MedicalRecordRepository(_dataContext);
        public IHospitalizationRepository HospitalizationRepository => _hospitalizationRepository ??= new HospitalizationRepository(_dataContext);
        public ITherapyRepository TherapyRepository => _therapyRepository ??= new TherapyRepository(_dataContext);
        public IBloodStorageRepository BloodStorageRepository => _bloodStorageRepository ??= new BloodStorageRepository(_dataContext);
        public IRoomEquipmentRepository RoomEquipmentRepository => _roomEquipmentRepository ??= new RoomEquipmentRepository(_dataContext);
        public IBedRepository BedRepository => _bedRepository ??= new BedRepository(_dataContext);
        public IAnnualLeaveRepository AnnualLeaveRepository =>
            _annualLeaveRepository ??= new AnnualLeaveRepository(_dataContext);
        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_dataContext);
    }
}