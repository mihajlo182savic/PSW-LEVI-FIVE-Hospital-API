﻿using HospitalLibrary.Rooms.Dtos;
using HospitalLibrary.Rooms.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Rooms.Interfaces
{
    public interface IRoomEquipmentService
    {
        Task<IEnumerable<RoomEquipment>> searchEquipmentInRoom(RoomEquipmentDTO roomEquipmentDTO);
        IEnumerable<Room> searchRoomsByFloorContainingEquipment(RoomEquipmentDTO roomEquipmentDTO);
    }
}
