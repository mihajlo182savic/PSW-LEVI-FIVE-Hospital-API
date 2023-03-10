using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ceTe.DynamicPDF.PageElements;
using HospitalLibrary.Appointments;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Renovations.Interface
{
  public interface IRenovationRepository : IBaseRepository<Model.Renovation>
  {
    Task<List<Model.Renovation>> GetAllPending();
    Task<List<Model.Renovation>> GetAllPendingForRoom(int roomId);
    Task<List<Model.Renovation>> GetAllPendingForRange(TimeInterval interval);
    Task<List<Model.Renovation>> GetAllPendingForRoomInRange(TimeInterval interval, int roomId);
    Task<TimeInterval> GetActiveRenovationForDay(DateTime date, int roomId);
    int MaxId();

  }
}
