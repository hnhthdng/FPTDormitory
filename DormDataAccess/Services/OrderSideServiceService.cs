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
    public class OrderSideServiceService : IOrderSideServiceService
    {
        private readonly OrderSideServiceDAO _orderSideServiceDAO;
        public OrderSideServiceService(OrderSideServiceDAO orderSideServiceDAO)
        {
            _orderSideServiceDAO = orderSideServiceDAO;
        }
        public Task AddOrderSideService(OrderSideService orderSideService) => _orderSideServiceDAO.AddAsync(orderSideService);
    }
}
