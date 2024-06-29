using DormDataAccess.DBContext;
using DormModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormDataAccess.DAO
{
    public class OrderSideServiceDAO
    {
        private readonly DormitoryDBContext _context;
        public OrderSideServiceDAO(DormitoryDBContext context) { _context = context; }

        public async Task AddAsync(OrderSideService orderSideService)
        {
            await _context.OrderSideServices.AddAsync(orderSideService);
            await _context.SaveChangesAsync();
        }
    }
}
