using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalLibrary.Renovations.Model
{
  public class SplitDTO
  {
    [Required]
    public int MainRoomId { get; set; }
    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }
    [Required]
    public String RoomName { get; set; }

    public SplitDTO(int mainRoomId, DateTime startDate, DateTime endTime, string roomName)
    {
      MainRoomId = mainRoomId;
      StartDate = startDate;
      EndDate = endTime;
      RoomName = roomName;
    }
    public Renovation MapToModel()
    {
      return new Renovation()
      {
        MainRoomId = MainRoomId,
        SecondaryRoomIds = null,
        StartAt = StartDate,
        EndAt = EndDate,
        State = RenovationState.PENDING,
        Type = RenovationType.SPLIT,
        roomName = RoomName
      };
    }
  }
}
