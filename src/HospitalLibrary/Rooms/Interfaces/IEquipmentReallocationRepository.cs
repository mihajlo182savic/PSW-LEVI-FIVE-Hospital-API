﻿using HospitalLibrary.Appointments;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Rooms.Interfaces
{
    public interface IEquipmentReallocationRepository: IBaseRepository<EquipmentReallocation>
    {
        Task<List<TimeInterval>> GetAllRoomTakenInrevalsForDate(int roomId, DateTime date);
        List<TimeInterval> GetAllRoomTakenInrevalsForDateList(int roomId, DateTime date);
        Task<EquipmentReallocation> GetById(int appointmentId);
        Task<List<EquipmentReallocation>> GetAllPending();
        Task<List<EquipmentReallocation>> GetAllPendingForToday();
    }
}
