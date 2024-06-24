using DormDataAccess.DAO;
using DormDataAccess.Services.IService;
using DormModel.DTO.Floor;
using DormModel.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormDataAccess.Services
{
    public class FloorService : IFloorService
    {
        private readonly FloorDAO _floorDAO;

        public FloorService(FloorDAO floorDAO)
        {
            _floorDAO = floorDAO;
        }
        public Task AddAsync(Floor floor) => _floorDAO.AddAsync(floor);

        public Task DeleteAsync(int id) => _floorDAO.DeleteAsync(id);

        public Task<List<Floor>> GetAllAsync() => _floorDAO.GetAllAsync();

        public Task<List<Floor>> GetByDormId(int dormId) => _floorDAO.GetByDormId(dormId);

        public Task<Floor> GetByIdAsync(int id) => _floorDAO.GetByIdAsync(id);

        public Task<Floor> GetByNameAsync(string name) => _floorDAO.GetByNameAsync(name);

        public Task UpdateAsync(Floor floor) => _floorDAO.UpdateAsync(floor);

    }
}
