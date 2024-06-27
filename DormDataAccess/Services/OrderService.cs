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
    public class OrderService : IOrderService
    {
        private readonly OrderDAO _orderDAO;

        public OrderService(OrderDAO orderDAO)
        {
            _orderDAO = orderDAO;
        }

        public Task AddAsync(Order order) => _orderDAO.AddAsync(order);

        public Task DeleteAsync(string id) => _orderDAO.DeleteAsync(id);


        public Task<List<Order>> GetAllAsync() => _orderDAO.GetAllAsync();

        public Task<Order> GetByIdAsync(string id) => _orderDAO.GetByIdAsync(id);

        public Task<List<Order>> GetByUserIdAsync(string userId) => _orderDAO.GetByUserId(userId);

        public Task UpdateAsync(Order order) => _orderDAO.UpdateAsync(order);
    }
}
