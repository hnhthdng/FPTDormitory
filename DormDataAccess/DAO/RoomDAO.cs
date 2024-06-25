using DormDataAccess.DBContext;
using DormModel.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormDataAccess.DAO
{
    public class RoomDAO
    {
        private readonly DormitoryDBContext _context;

        public RoomDAO(DormitoryDBContext context)
        {
            _context = context;
        }

        public async Task AddRoomToFloorAsync(int floorId, Room room)
        {

            var isExistRoom = await GetByIdAsync(room.Id);
            if (isExistRoom == null)
            {
                _context.Rooms.Add(room);
                await _context.SaveChangesAsync();

                var floorRoom = new FloorRoom
                {
                    FloorId = floorId,
                    RoomId = room.Id
                };
                _context.FloorRooms.Add(floorRoom);
                await _context.SaveChangesAsync();

            }
            else
            {
                throw new Exception("room is exist");
            }
        }

        public async Task DeleteAsync(int id)
        {
            var room = await GetByIdAsync(id);
            if (room != null)
            {
                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Error for delete");
            }
        }

        public async Task<List<Room>> GetAllAsync()
        {
            return await _context.Rooms
           .ToListAsync();
        }

        public async Task<List<Room>> GetByDormId(int dormId)
        {
            var rooms = await _context.DormFloors
            .Where(df => df.DormId == dormId)
            .SelectMany(df => df.Floor.FloorRooms)
            .Select(fr => fr.Room)
            .Distinct()
            .ToListAsync();

            return rooms;
        }

        public async Task<List<Room>> GetByDormIdAndFloorId(int dormId, int floorId)
        {
            var rooms = await _context.DormFloors
            .Where(df => df.DormId == dormId && df.FloorId == floorId)
            .SelectMany(df => df.Floor.FloorRooms)
            .Select(fr => fr.Room)
            .Distinct()
            .ToListAsync();

            return rooms;
        }

        public async Task<List<Room>> GetByFloorId(int floorid)
        {
            var rooms = await _context.FloorRooms
            .Where(fr => fr.FloorId == floorid)
            .Select(fr => fr.Room)
            .Distinct()
            .ToListAsync();

            return rooms;
        }

        public async Task<Room> GetByIdAsync(int id)
        {
            var room = await _context.Rooms
            .FirstOrDefaultAsync(r => r.Id == id);

            return room;
        }

        public async Task<Room> GetByNameAsync(string name)
        {
            var room = await _context.Rooms
            .FirstOrDefaultAsync(r => r.Name == name);

            return room;
        }

        public async Task UpdateAsync(Room room)
        {
            _context.Rooms.Update(room);
            await _context.SaveChangesAsync();
        }
    }
}
