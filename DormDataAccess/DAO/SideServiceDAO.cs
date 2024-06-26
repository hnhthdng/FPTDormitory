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
    public class SideServiceDAO
    {
        private readonly DormitoryDBContext _context;

        public SideServiceDAO(DormitoryDBContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(int id)
        {
            var sideService = await GetByIdAsync(id);
            if (sideService != null)
            {
                _context.SideServices.Remove(sideService);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Error for delete");
            }
        }

        public async Task<List<SideService>> GetAllAsync()
        {
            return await _context.SideServices
           .ToListAsync();
        }

        public async Task<SideService> GetByIdAsync(int id)
        {
            var sideService = await _context.SideServices
            .FirstOrDefaultAsync(r => r.Id == id);

            return sideService;
        }

        public async Task<SideService> GetByNameAsync(string name)
        {
            var sideService = await _context.SideServices
            .FirstOrDefaultAsync(r => r.Name == name);

            return sideService;
        }

        public async Task UpdateAsync(SideService sideService)
        {
            _context.SideServices.Update(sideService);
            await _context.SaveChangesAsync();
        }
    }
}
