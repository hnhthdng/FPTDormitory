using DormDataAccess.DBContext;
using DormModel.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormDataAccess.DAO
{
    public class DormDAO
    {
        private readonly DormitoryDBContext _context;

        public DormDAO(DormitoryDBContext context)
        {
            _context = context;
        }

        public async Task<List<Dorm>> GetAllAsync()
        {
            return await _context.Dorms.Include(d => d.DormFloors).ToListAsync();
        }

        public async Task<Dorm> GetByNameAsync(string name)
        {
            return await _context.Dorms.Include(d => d.DormFloors).FirstOrDefaultAsync(d => d.Name.ToLower() == name.ToLower());
        }
        public async Task<Dorm> GetByIdAsync(int id)
        {
            return await _context.Dorms.Include(d => d.DormFloors).FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task AddAsync(Dorm dorm)
        {
            var isExistDorm =  await GetByNameAsync(dorm.Name);
            if(isExistDorm == null)
            {
                _context.Dorms.Add(dorm);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Dorm is exist");
            }
        }

        public async Task UpdateAsync(Dorm dorm)
        {
            _context.Dorms.Update(dorm);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var dorm = await GetByIdAsync(id);
            if (dorm != null)
            {
                _context.Dorms.Remove(dorm);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Error for delete");
            }
        }
    }

}
