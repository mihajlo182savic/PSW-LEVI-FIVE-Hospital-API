﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Rooms.Model
{
    public class EquipmentReallocation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }

        [ForeignKey("StartingRoom")]
        public int StartingRoomId { get; set; }

        public Room StartingRoom { get; set; }
        [ForeignKey("DestinationRoom")]
        public int DestinationRoomId { get; set; }

        public Room DestinationRoom { get; set; }

        [ForeignKey("Equipment")]
        public int EquipmentId { get; set; }
        public RoomEquipment Equipment { get; set; }

        public int Amount { get; set; }
        public ReallocationState State { get; set; }
        
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }

        public EquipmentReallocation() 
        {
        }
    }
}

public enum ReallocationState 
{
    FINISHED,
    PENDING
}
