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
    public class RoomOrderService : IRoomOrderService
    {
        private readonly RoomOrderDAO _roomOrderDAO;

        public RoomOrderService(RoomOrderDAO roomOrderDAO)
        {
            _roomOrderDAO = roomOrderDAO;
        }
        public async Task CreateRoomOrder(RoomOrder roomOrder) =>_roomOrderDAO.AddAsync(roomOrder);
    }
}
