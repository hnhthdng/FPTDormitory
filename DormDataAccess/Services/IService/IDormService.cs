using DormModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormDataAccess.Services.IService
{
    public interface IDormService
    {
        Task<List<Dorm>> GetAllAsync();
        Task<Dorm> GetByNameAsync(string name);
        Task<Dorm> GetByIdAsync(int id);
        Task AddAsync(Dorm dorm);
        Task UpdateAsync(Dorm dorm);
        Task DeleteAsync(int id);
    }
}
