using DormModel.DTO.Floor;
using DormModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormDataAccess.Services.IService
{
    public interface IFloorService
    {
        Task<List<Floor>> GetAllAsync();
        Task<Floor> GetByNameAsync(string name);
        Task<Floor> GetByIdAsync(int id);
        Task<List<Floor>> GetByDormId(int dormId);
        Task AddFloorToDormAsync(int dormId, Floor newFloor);
        Task UpdateAsync(Floor floor);
        Task DeleteAsync(int id);
    }
}
