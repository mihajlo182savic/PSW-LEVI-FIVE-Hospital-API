using System;

namespace HospitalLibrary.Renovations.Model
{
  public class RenovationEventDTO
  {
    public int Id { get; set; }
    public int MainRoomId { get; set; }
    public string SecondaryRoomIds { get; set; }
    public RenovationType Type { get; set; }
    public RenovationState State { get; set; }
    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }
    public String? roomName { get; set; }
    public String Uuid { get; set; }
    public RenovationEventDTO(){}
    public RenovationEventDTO(Renovation renovation, string uuid)
    {
      Id=renovation.Id;
      MainRoomId = renovation.MainRoomId;
      StartAt = renovation.StartAt;
      EndAt = renovation.EndAt;
      State = RenovationState.PENDING;
      Type = renovation.Type;
      SecondaryRoomIds = renovation.SecondaryRoomIds;
      roomName = renovation.roomName;
      Uuid=uuid;
    }

    public Renovation MapToModel()
    {
      return new Renovation()
      {
        Id = Id,
        MainRoomId = MainRoomId,
        SecondaryRoomIds = SecondaryRoomIds,
        StartAt = StartAt,
        EndAt = EndAt,
        State = RenovationState.PENDING,
        Type = Type,
        roomName = roomName
      };
    }
  }
}
