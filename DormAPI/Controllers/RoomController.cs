using AutoMapper;
using DormDataAccess.Services;
using DormDataAccess.Services.IService;
using DormModel.DTO.Room;
using DormModel.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DormAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly IMapper _mapper;

        public RoomController(IRoomService roomService, IMapper mapper)
        {
            _roomService = roomService;
            _mapper = mapper;
        }

        [HttpGet("get-all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {

            var rooms = await _roomService.GetAllAsync();
            var roomsDTO = _mapper.Map<List<RoomResponseDTO>>(rooms);
            return Ok(roomsDTO);
        }

        [HttpGet("get-by-dorm-id")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByDormId(int dormID)
        {

            var rooms = await _roomService.GetByDormId(dormID);
            var roomsDTO = _mapper.Map<List<RoomResponseDTO>>(rooms);
            return Ok(roomsDTO);
        }


        [HttpGet("get-by-dorm-id-and-floor-id")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByDormIdAndFloorId(int dormId, int floorId)
        {

            var rooms = await _roomService.GetByDormIdAndFloorId(dormId,floorId);
            var roomsDTO = _mapper.Map<List<RoomResponseDTO>>(rooms);
            return Ok(roomsDTO);
        }


        [HttpGet("get-by-floor-id")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByFloorId(int floorid)
        {
            var rooms = await _roomService.GetByFloorId(floorid);
            var roomsDTO = _mapper.Map<List<RoomResponseDTO>>(rooms);
            return Ok(roomsDTO);
        }

        [HttpGet("get-by-id")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var rooms = await _roomService.GetByIdAsync(id);
            var roomsDTO = _mapper.Map<RoomResponseDTO>(rooms);
            return Ok(roomsDTO);
        }

        [HttpGet("get-by-name")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByNameAsync(string name)
        {
            var rooms = await _roomService.GetByNameAsync(name);
            var roomsDTO = _mapper.Map<RoomResponseDTO>(rooms);
            return Ok(roomsDTO);
        }

        [HttpPost("add-room-to-floorId")]
        public async Task<IActionResult> AddRoom(int floorId, RoomRequestDTO roomModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isFloorExist = await _roomService.GetByNameAsync(roomModel.Name);
            if (isFloorExist == null)
            {
                var room = new Room()
                {
                    Name = roomModel.Name,
                    MaximumNumberOfPeople = roomModel.MaximumNumberOfPeople,
                    Price = roomModel.Price,
                };
                await _roomService.AddRoomToFloorAsync(floorId, room);
                return StatusCode(200, "Add success");
            }
            return StatusCode(500, "Add failed");
        }

        [HttpPost("update-room")]
        public async Task<IActionResult> UpdateRoom(int roomid, RoomRequestDTO roomModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var isExistFloor = await _roomService.GetByNameAsync(roomModel.Name);
            if (isExistFloor == null)
            {
                var roomById = await _roomService.GetByIdAsync(roomid);
                if (roomById != null)
                {
                    roomById.Name = roomModel.Name;
                    roomById.MaximumNumberOfPeople = roomModel.MaximumNumberOfPeople;
                    roomById.CurrentNumberOfPeople = roomById.CurrentNumberOfPeople;
                    roomById.IsMaximum = roomById.IsMaximum;    
                    await _roomService.UpdateAsync(roomById);
                    return StatusCode(200, "Update success");
                }
                return StatusCode(500, "Can not find this room !");

            }

            return StatusCode(500, "this room is existed !");
        }
        [HttpGet("delete-room")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var floorById = await _roomService.GetByIdAsync(id);
            if (floorById != null)
            {
                await _roomService.DeleteAsync(id);
                return StatusCode(200, "Delete success");
            }
            return StatusCode(500, "Can not find this room !");
        }
    }
}
