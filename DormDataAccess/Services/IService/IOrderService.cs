using DormModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormDataAccess.Services.IService
{
    public interface IOrderService
    {
        Task AddAsync(Order order);
        Task<List<Order>> GetAllAsync();
        Task<Order> GetByIdAsync(string id);
        Task<List<Order>> GetByUserIdAsync(string userId);
        Task UpdateAsync(Order order);
        Task DeleteAsync(string id);
    }
}
