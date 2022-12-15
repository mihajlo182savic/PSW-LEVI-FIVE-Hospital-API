﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Appointments.Interfaces
{
    public interface IAppointmentRepository : IBaseRepository<Appointment>
    {
        Task<List<TimeInterval>> GetAllRoomTakenIntervalsForDate(int roomId, DateTime date);
        Task<IEnumerable<TimeInterval>> GetAllDoctorTakenIntervalsForDate(int doctorId, DateTime date);
        Task<IEnumerable<TimeInterval>> GetAllDoctorTakenIntervalsForTimeInterval(int doctorId, TimeInterval timeInterval);
        Task<IEnumerable<Appointment>> GetAllDoctorUpcomingAppointments(int doctorId);
        Task<IEnumerable<Appointment>> GetAllDoctorAppointmentsForRange(int doctorId, TimeInterval interval);
        int GetNumberOfDoctorAppointmentsForRange(int doctorId, TimeInterval interval);
        Task<Appointment> GetById(int appointmentId);

        Task<IEnumerable<TimeInterval>> GetAllDoctorTakenIntervalsForDateExcept(int roomId, DateTime date, int ignore);
        Task<IEnumerable<TimeInterval>> GetAllRoomTakenIntervalsForDateExcept(int roomId, DateTime date, int ignore);
        Task<TimeInterval> GetLastForDate(DateTime date);

        Task<TimeInterval> GetFirstForDate(DateTime date);

    }
}