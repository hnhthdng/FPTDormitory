using AutoMapper;
using DormDataAccess.Services.IService;
using DormModel.DTO.Account;
using DormModel.DTO.Dorm;
using DormModel.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DormAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class DormController : ControllerBase
    {
        private readonly IDormService _dormService;
        private readonly IMapper _mapper;

        public DormController(IDormService dormService, IMapper mapper)
        {
            _dormService = dormService;
            _mapper = mapper;
        }


        [HttpGet("get-all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllASync() 
        {
            var dorms = await _dormService.GetAllAsync();
            return Ok(dorms);
        }

        [HttpGet("get-by-name")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByNameAsync(string name)
        {
            var dorm = await _dormService.GetByNameAsync(name);
            return Ok(dorm);
        }

        [HttpGet("get-by-id")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var dorm = await _dormService.GetByIdAsync(id);
            return Ok(dorm);
        }

        [HttpPost("add-dorm")]
        public async Task<IActionResult> AddDorm(DormRequestDTO dormModel)
        {
            if(!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var isDormExist = await _dormService.GetByNameAsync(dormModel.Name);
            if (isDormExist == null)
            {
                var dorm = _mapper.Map<Dorm>(dormModel);
                await _dormService.AddAsync(dorm);
                return StatusCode(200, "Add success");
            }
            return StatusCode(500, "Add failed");
        }

        [HttpPost("update-dorm")]
        public async Task<IActionResult> UpdateDorm(int id, string nameUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var isExistDorm = await _dormService.GetByNameAsync(nameUpdate);
            if(isExistDorm == null)
            {
                var dormById = await _dormService.GetByIdAsync(id);
                if (dormById != null)
                {
                    dormById.Name = nameUpdate;
                    await _dormService.UpdateAsync(dormById);
                    return StatusCode(200, "Update success");
                }
                return StatusCode(500, "Can not find this dorm !");

            }

            return StatusCode(500, "this dorm is existed !");
        }


        [HttpGet("delete-dorm")]
        public async Task<IActionResult> DeleteDorm(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dormById = await _dormService.GetByIdAsync(id);
            if (dormById != null)
            {
                await _dormService.DeleteAsync(id);
                return StatusCode(200, "Delete success");
            }
            return StatusCode(500, "Can not find this dorm !");
        }
    }
}
