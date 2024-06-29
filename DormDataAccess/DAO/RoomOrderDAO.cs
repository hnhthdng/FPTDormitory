using DormDataAccess.DBContext;
using DormModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormDataAccess.DAO
{
    public class RoomOrderDAO
    {
        private readonly DormitoryDBContext _context;

        public RoomOrderDAO(DormitoryDBContext context)
        {
            _context = context;
        }

        public async Task AddAsync(RoomOrder roomOrder)
        {
            await _context.RoomOrders.AddAsync(roomOrder);
            await _context.SaveChangesAsync();
        }
    }
}
