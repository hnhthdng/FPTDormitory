using DormDataAccess.DBContext;
using DormModel.DTO.Floor;
using DormModel.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormDataAccess.DAO
{
    public class FloorDAO
    {
        private readonly DormitoryDBContext _context;

        public FloorDAO(DormitoryDBContext context)
        {
            _context = context;
        }

        public async Task AddFloorToDormAsync(int dormId, Floor newFloor)
        {
            var isExistFloor = await GetByIdAsync(newFloor.Id);
            if (isExistFloor == null)
            {
                _context.Floors.Add(newFloor);
                await _context.SaveChangesAsync();

                // Create a new DormFloor entry to associate the new floor with the specified dormitory
                var dormFloor = new DormFloor
                {
                    DormId = dormId,
                    FloorId = newFloor.Id
                };

                _context.DormFloors.Add(dormFloor);
                await _context.SaveChangesAsync();

            }
            else
            {
                throw new Exception("floor is exist");
            }
        }

        public async Task DeleteAsync(int id)
        {
            var floor = await GetByIdAsync(id);
            if (floor != null)
            {
                _context.Floors.Remove(floor);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Error for delete");
            }
        }

        public async Task<List<Floor>> GetAllAsync()
        {
            return await _context.Floors
           .ToListAsync();
        }

        public async Task<List<Floor>> GetByDormId(int dormId)
        {
            var floors = await _context.DormFloors
            .Where(df => df.DormId == dormId)
            .Select(df => new Floor
            {
                Id = df.Floor.Id,
                Name = df.Floor.Name,
            }).ToListAsync();

            return floors;
        }

        public async Task<Floor> GetByIdAsync(int id)
        {
            return await _context.Floors.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Floor> GetByNameAsync(string name)
        {
            return await _context.Floors.FirstOrDefaultAsync(d => d.Name.ToLower() == name.ToLower());
        }

        public async Task UpdateAsync(Floor floor)
        {
            _context.Floors.Update(floor);
            await _context.SaveChangesAsync();
        }   
    }
}
