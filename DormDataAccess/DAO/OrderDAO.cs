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
     public class OrderDAO
    {
        private readonly DormitoryDBContext _context;

        public OrderDAO(DormitoryDBContext context)
        {
            _context = context;
        }
        public async Task DeleteAsync(string id)
        {
            var order = await GetByIdAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Error for delete");
            }
        }

        public async Task AddAsync(Order order) {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _context.Orders
          .ToListAsync();
        }

        public async Task<Order> GetByIdAsync(string id)
        {
            return await _context.Orders.Where(d => d.Id.Equals(id)).SingleOrDefaultAsync();
        }

        public async Task<List<Order>> GetByUserId(string userId)
        {
            return await _context.Orders
                .Where(d => d.UserId.Equals(userId))
                .ToListAsync();
        }


        public async Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }
    }
}
