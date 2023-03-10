using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Rooms.Dtos;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Rooms.Interfaces
{
    public interface IRoomRepository: IBaseRepository<Room>
    {
        Task<IEnumerable<Room>> FindAllByFloor(int floor);
        Task<IEnumerable<Room>> GetHospitalExaminationRooms();
        Task<IEnumerable<Room>> GetHospitalConsiliumRooms();
        Task<IEnumerable<RoomEquipment>> GetAllEquipmentbyRoom(int id);
        Task<IEnumerable<Room>> SearchByTypeAndName(RoomSearchDTO roomsSearchDTO, int floorId);
        int GetMaxId();

        Task<IEnumerable<Room>> GetBySplitRoomId(int roomId);
    }
}
