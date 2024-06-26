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
    public class SideServiceService : ISideServiceService
    {
        private readonly SideServiceDAO _sideServiceDAO;

        public SideServiceService(SideServiceDAO sideServiceDAO)
        {
            _sideServiceDAO = sideServiceDAO;
        }
        public Task DeleteAsync(int id) => _sideServiceDAO.DeleteAsync(id);

        public Task<List<SideService>> GetAllAsync() => _sideServiceDAO.GetAllAsync();

        public Task<SideService> GetByIdAsync(int id) => _sideServiceDAO.GetByIdAsync(id);

        public Task<SideService> GetByNameAsync(string name) => _sideServiceDAO.GetByNameAsync(name);

        public Task UpdateAsync(SideService sideService) => _sideServiceDAO.UpdateAsync(sideService);
    }
}
