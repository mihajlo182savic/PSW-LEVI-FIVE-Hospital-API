using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Shared.Repository;
using HospitalLibrary.Appointments.Interfaces;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Appointments
{
    public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(HospitalDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TimeInterval>> GetAllRoomTakenIntervalsForDate(int roomId, DateTime date)
        {
            return await _dataContext.Appointments
                .Where(a => a.RoomId == roomId)
                .Where(a => a.StartAt.Date.Equals(date.Date))
                .Where(a => a.State.Equals(AppointmentState.PENDING))
                .Select(a => new TimeInterval(a.StartAt, a.EndAt))
                .ToListAsync();
        }

        public async Task<IEnumerable<TimeInterval>> GetAllDoctorTakenIntervalsForDate(int doctorId, DateTime date)
        {
            return await _dataContext.Appointments
                .Where(a => a.DoctorId == doctorId)
                .Where(a => a.StartAt.Date.Equals(date.Date))
                .Where(a => a.State.Equals(AppointmentState.PENDING))
                .Select(a => new TimeInterval(a.StartAt, a.EndAt))
                .ToListAsync();
        }
        public async Task<IEnumerable<Appointment>> GetAllDoctorUpcomingAppointments(int doctorId)
        {
            return await _dataContext.Appointments
                .Where(a => a.DoctorId == doctorId)
                .Where(a => a.State.Equals(AppointmentState.PENDING))
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAllDoctorAppointmentsForRange(int doctorId, TimeInterval interval)
        {
            return await _dataContext.Appointments
                .Where(a => a.DoctorId == doctorId)
                .Where(a =>
                    interval.Start.Date.CompareTo(a.StartAt.Date) <= 0
                    && interval.End.Date.CompareTo(a.StartAt.Date) >= 0)
                .Include(a => a.Patient)
                .OrderBy(a => a.StartAt)
                .ToListAsync();
        }
    }
}