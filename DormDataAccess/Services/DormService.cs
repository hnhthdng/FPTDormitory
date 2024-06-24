using DormDataAccess.DAO;
using DormDataAccess.DBContext;
using DormDataAccess.Services.IService;
using DormModel.DTO.Dorm;
using DormModel.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormDataAccess.Services
{
    public class DormService : IDormService
    {
        private readonly DormDAO _dormDAO;

        public DormService(DormDAO roomDAO)
        {
            _dormDAO = roomDAO;
        }
        public Task AddAsync(Dorm dorm) => _dormDAO.AddAsync(dorm);

        public Task DeleteAsync(int id) => _dormDAO.DeleteAsync(id);
        public Task<List<Dorm>> GetAllAsync() => _dormDAO.GetAllAsync();

        public Task<Dorm> GetByIdAsync(int id) => _dormDAO.GetByIdAsync(id);

        public Task<Dorm> GetByNameAsync(string name) => _dormDAO.GetByNameAsync(name);

        public Task UpdateAsync(Dorm dorm) => _dormDAO.UpdateAsync(dorm);
    }
}
