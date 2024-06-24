using AutoMapper;
using DormDataAccess.Services;
using DormDataAccess.Services.IService;
using DormModel.DTO.Account;
using DormModel.DTO.Dorm;
using DormModel.DTO.Floor;
using DormModel.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DormAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class FloorController : ControllerBase
    {
        private readonly IFloorService _floorService;
        private readonly IMapper _mapper;

        public FloorController(IFloorService floorService, IMapper mapper)
        {
            _floorService = floorService;
            _mapper = mapper;
        }

        [HttpGet("get-all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {

            var floor = await _floorService.GetAllAsync();
            var floorDTO = _mapper.Map<List<FloorResponseDTO>>(floor);
            return Ok(floorDTO);
        }

        [HttpGet("get-by-name")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByNameAsync(string name)
        {

            var floor = await _floorService.GetByNameAsync(name);
            var floorDTO = _mapper.Map<FloorResponseDTO>(floor);
            return Ok(floorDTO);
        }

        [HttpGet("get-by-id")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {

            var floor = await _floorService.GetByIdAsync(id);
            var floorDTO = _mapper.Map<FloorResponseDTO>(floor);
            return Ok(floorDTO);
        }

        [HttpGet("get-all-by-dormId")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllFloorByDormID(int id)
        {

            var floor = await _floorService.GetByDormId(id);
            var floorDTO = _mapper.Map<List<FloorResponseDTO>>(floor);
            return Ok(floorDTO);
        }


        [HttpPost("add-floor")]
        public async Task<IActionResult> AddFloor(string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isFloorExist = await _floorService.GetByNameAsync(name);
            if (isFloorExist == null)
            {
                var floor = new Floor()
                {
                    Name = name,
                };
                await _floorService.AddAsync(floor);
                return StatusCode(200, "Add success");
            }
            return StatusCode(500, "Add failed");
        }

        [HttpPost("update-floor")]
        public async Task<IActionResult> UpdateFloor(int id, string nameUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var isExistFloor = await _floorService.GetByNameAsync(nameUpdate);
            if (isExistFloor == null)
            {
                var dormById = await _floorService.GetByIdAsync(id);
                if (dormById != null)
                {
                    dormById.Name = nameUpdate;
                    await _floorService.UpdateAsync(dormById);
                    return StatusCode(200, "Update success");
                }
                return StatusCode(500, "Can not find this floor !");

            }

            return StatusCode(500, "this floor is existed !");
        }
        [HttpGet("delete-floor")]
        public async Task<IActionResult> DeleteFloor(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var floorById = await _floorService.GetByIdAsync(id);
            if (floorById != null)
            {
                await _floorService.DeleteAsync(id);
                return StatusCode(200, "Delete success");
            }
            return StatusCode(500, "Can not find this floor !");
        }
    }
}
