using AutoMapper;
using DormDataAccess.Services;
using DormDataAccess.Services.IService;
using DormModel.DTO.Order;
using DormModel.DTO.Room;
using DormModel.DTO.SideService;
using DormModel.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace DormAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin, User")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderService.GetAllAsync();
            var ordersDTO = _mapper.Map<List<OrderResponseDTO>>(orders);
            return Ok(ordersDTO);
        }

        [HttpGet("get-items-by-order-id")]
        public async Task<IActionResult> GetItemsByOrderId(string orderid)
        {
            var orders = await _orderService.GetByIdAsync(orderid);
            Cart cart = new Cart()
            {
                roomResponseDTOs = new List<RoomResponseDTO>(),
                sideServiceResponseDTOs = new List<SideServiceResponseDTO>()
            };
            foreach (var room in orders.Rooms.ToList())
            {
                var roomDTO = _mapper.Map<RoomResponseDTO>(room);
                cart.roomResponseDTOs.Add(roomDTO);
            }
            foreach (var sideService in orders.SideServices.ToList())
            {
                var sideServiceDTO = _mapper.Map<SideServiceResponseDTO>(sideService);
                cart.sideServiceResponseDTOs.Add(sideServiceDTO);
            }
            return Ok(cart);
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById(string id)
        {
            var order = await _orderService.GetByIdAsync(id);
            var orderDTO = _mapper.Map<OrderResponseDTO>(order);
            return Ok(orderDTO);
        }

        [HttpGet("get-by-user-id")]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            var orders = await _orderService.GetByUserIdAsync(userId);
            var ordersDTO = _mapper.Map<List<OrderResponseDTO>>(orders);
            return Ok(ordersDTO);
        }

        [HttpPost("update-order")]
        public async Task<IActionResult> UpdateOrder(OrderRequestDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var orderExist = await _orderService.GetByIdAsync(model.Id);
            if (orderExist != null)
            {
                orderExist.Status = model.Status;
                orderExist.Description = model.Description;
                orderExist.TotalPrice = model.TotalPrice;
                orderExist.UpdatedDate = DateTime.Now;
                await _orderService.UpdateAsync(orderExist);
                return Ok("Update success");
            }
            return StatusCode(500, "Can not find this order !");
        }
    }
}
