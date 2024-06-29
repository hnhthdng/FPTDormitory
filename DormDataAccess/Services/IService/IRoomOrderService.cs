using DormModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormDataAccess.Services.IService
{
    public interface IRoomOrderService
    {
        Task CreateRoomOrder(RoomOrder roomOrder);
    }
}
