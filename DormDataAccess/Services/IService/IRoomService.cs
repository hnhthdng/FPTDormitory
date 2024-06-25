using DormModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormDataAccess.Services.IService
{
    public interface IRoomService
    {
        Task<List<Room>> GetAllAsync();
        Task<Room> GetByNameAsync(string name);
        Task<Room> GetByIdAsync(int id);
        Task<List<Room>> GetByDormId(int dormId);
        Task<List<Room>> GetByFloorId(int floorid);
        Task<List<Room>> GetByDormIdAndFloorId(int dormId, int floorid);
        Task AddRoomToFloorAsync(int floorId, Room room);
        Task UpdateAsync(Room room);
        Task DeleteAsync(int id);
    }
}
