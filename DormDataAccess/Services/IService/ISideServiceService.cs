using DormModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormDataAccess.Services.IService
{
    public interface ISideServiceService
    {
        Task<List<SideService>> GetAllAsync();
        Task<SideService> GetByNameAsync(string name);
        Task<SideService> GetByIdAsync(int id);
        Task UpdateAsync(SideService sideService);
        Task DeleteAsync(int id);
    }
}
