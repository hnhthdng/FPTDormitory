
using AutoMapper;
using DormDataAccess.Services.IService;
using DormModel.DTO.Floor;
using DormModel.DTO.Room;
using DormModel.DTO.SideService;
using DormModel.Model;
using DormUtility.Session;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace DormAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin, User")]
    public class CartController : ControllerBase
    {
        //Working with session to CRUD room or sideservice in Cart
        private readonly IRoomService _roomService;
        private readonly ISideServiceService _sideServiceService;

        private readonly IMapper _mapper;
        public CartController(IRoomService roomService, IMapper mapper, ISideServiceService sideServiceService) 
        {
            _roomService = roomService;
            _mapper = mapper;
            _sideServiceService = sideServiceService;
        }

        const string CART_KEY = "CART_KEY";
        public Cart Carts => HttpContext.Session.Get<Cart>(CART_KEY) ?? new Cart()
        {
            roomResponseDTOs = new List<RoomResponseDTO>(),
            sideServiceResponseDTOs = new List<SideServiceResponseDTO>(),
        };

        [HttpGet("get-all-item")]
        public async Task<IActionResult> GetAllItemInCart()
        {
            // Retrieve the cart items from the session
            var cartItems = Carts;

            // Return the cart items as an Ok result
            return Ok(cartItems);
        }


        [HttpGet("add-item")]
        public async Task<IActionResult> AddItemInCart(int? roomId, int? sideServiceId)
        {
            if(roomId.HasValue || sideServiceId.HasValue)
            {
                if (roomId.HasValue)
                {
                    var carts = Carts;
                    var isRoomInCart = carts.roomResponseDTOs.SingleOrDefault(p => p.Id == roomId);
                    if (isRoomInCart == null)
                    {
                        var room = await _roomService.GetByIdAsync(roomId.Value);
                        var roomDTO = _mapper.Map<RoomResponseDTO>(room);
                        carts.roomResponseDTOs.Add(roomDTO);
                        HttpContext.Session.Set(CART_KEY, carts);
                    }
                }

                if (sideServiceId.HasValue)
                {
                    var carts = Carts;
                    var isSideServiceInCart = carts.sideServiceResponseDTOs.SingleOrDefault(p => p.Id == sideServiceId);
                    if (isSideServiceInCart == null)
                    {
                        var sideService = await _sideServiceService.GetByIdAsync(sideServiceId.Value);
                        var sideServiceDTO = _mapper.Map<SideServiceResponseDTO>(sideService);
                        carts.sideServiceResponseDTOs.Add(sideServiceDTO);
                        HttpContext.Session.Set(CART_KEY, carts);
                    }
                }
                return Ok(Carts);
            };

            return BadRequest("No valid item ID provided");
        }

        [HttpGet("remove-item")]
        public async Task<IActionResult> RemoveItemInCart(int? roomId, int? sideServiceId)
        {
            if (roomId.HasValue || sideServiceId.HasValue)
            {
                if(roomId.HasValue)
                {
                    var cart = Carts;
                    var isRoomInCart = cart.roomResponseDTOs.SingleOrDefault(p => p.Id == roomId);
                    if (isRoomInCart != null)
                    {
                        cart.roomResponseDTOs.Remove(isRoomInCart);
                        HttpContext.Session.Set(CART_KEY, cart);
                    }
                }

                if (sideServiceId.HasValue)
                {
                    var cart = Carts;
                    var isSideServiceInCart = cart.sideServiceResponseDTOs.SingleOrDefault(p => p.Id == roomId);
                    if (isSideServiceInCart != null)
                    {
                        cart.sideServiceResponseDTOs.Remove(isSideServiceInCart);
                        HttpContext.Session.Set(CART_KEY, cart);
                    }
                }
                return Ok(Carts);
            }

            return BadRequest("No valid item ID provided");
        }
    }
}
