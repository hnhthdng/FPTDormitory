using DormDataAccess.DAO;
using DormDataAccess.Services.IService;
using DormModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormDataAccess.Services
{
    public class RoomService : IRoomService
    {
        private readonly RoomDAO  _roomDAO;

        public RoomService(RoomDAO roomDAO)
        {
            _roomDAO = roomDAO;
        }
        public Task AddRoomToFloorAsync(int floorId, Room room) => _roomDAO.AddRoomToFloorAsync(floorId, room);

        public Task DeleteAsync(int id) => _roomDAO.DeleteAsync(id);

        public Task<List<Room>> GetAllAsync() => _roomDAO.GetAllAsync();

        public Task<List<Room>> GetByDormId(int dormId) => _roomDAO.GetByDormId(dormId);

        public Task<List<Room>> GetByDormIdAndFloorId(int dormId, int floorid) => _roomDAO.GetByDormIdAndFloorId(dormId, floorid);

        public Task<List<Room>> GetByFloorId(int floorid) => _roomDAO.GetByFloorId(floorid);

        public Task<Room> GetByIdAsync(int id) => _roomDAO.GetByIdAsync(id);

        public Task<Room> GetByNameAsync(string name) => _roomDAO.GetByNameAsync(name);

        public Task UpdateAsync(Room room)=>_roomDAO.UpdateAsync(room);
    }
}
