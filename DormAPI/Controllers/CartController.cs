using AutoMapper;
using DormDataAccess.Service.IServices;
using DormDataAccess.Services;
using DormDataAccess.Services.IService;
using DormModel.DTO.Account;
using DormModel.DTO.Floor;
using DormModel.DTO.Room;
using DormModel.DTO.SideService;
using DormModel.Model;
using DormUtility.Session;
using DormUtility.VNPay;
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
        private readonly IRoomService _roomService;
        private readonly ISideServiceService _sideServiceService;
        private readonly IMapper _mapper;
        private readonly IVNPayService _vNPayService;
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;

        const string CART_KEY = "CART_KEY";

        public CartController(IRoomService roomService, IMapper mapper, ISideServiceService sideServiceService,
            IVNPayService vNPayService, IOrderService orderService, IUserService userService)
        {
            _roomService = roomService;
            _mapper = mapper;
            _sideServiceService = sideServiceService;
            _vNPayService = vNPayService;
            _orderService = orderService;
            _userService = userService;
        }

        public Cart Carts => HttpContext.Session.Get<Cart>(CART_KEY) ?? new Cart()
        {
            roomResponseDTOs = new List<RoomResponseDTO>(),
            sideServiceResponseDTOs = new List<SideServiceResponseDTO>(),
        };

        [HttpGet("get-all-item")]
        public async Task<IActionResult> GetAllItemInCart()
        {
            var cartItems = Carts;
            return Ok(cartItems);
        }

        [HttpGet("add-item")]
        public async Task<IActionResult> AddItemInCart(int? roomId, int? sideServiceId)
        {
            if(roomId.HasValue || sideServiceId.HasValue)
            {
                var carts = Carts;

                if (roomId.HasValue)
                {
                    var existingRoom = carts.roomResponseDTOs.SingleOrDefault(p => p.Id == roomId);
                    if (existingRoom == null)
                    {
                        var room = await _roomService.GetByIdAsync(roomId.Value);
                        var roomDTO = _mapper.Map<RoomResponseDTO>(room);
                        carts.roomResponseDTOs.Add(roomDTO);
                        HttpContext.Session.Set(CART_KEY, carts);
                    }
                }

                if (sideServiceId.HasValue)
                {
                    var existingSideService = carts.sideServiceResponseDTOs.SingleOrDefault(p => p.Id == sideServiceId);
                    if (existingSideService == null)
                    {
                        var sideService = await _sideServiceService.GetByIdAsync(sideServiceId.Value);
                        var sideServiceDTO = _mapper.Map<SideServiceResponseDTO>(sideService);
                        carts.sideServiceResponseDTOs.Add(sideServiceDTO);
                        HttpContext.Session.Set(CART_KEY, carts);
                    }
                }
                return Ok(Carts);
            }

            return BadRequest("No valid item ID provided");
        }

        [HttpGet("remove-item")]
        public async Task<IActionResult> RemoveItemInCart(int? roomId, int? sideServiceId)
        {
            if (roomId.HasValue || sideServiceId.HasValue)
            {
                var cart = Carts;

                if(roomId.HasValue)
                {
                    var roomToRemove = cart.roomResponseDTOs.SingleOrDefault(p => p.Id == roomId);
                    if (roomToRemove != null)
                    {
                        cart.roomResponseDTOs.Remove(roomToRemove);
                        HttpContext.Session.Set(CART_KEY, cart);
                    }
                }

                if (sideServiceId.HasValue)
                {
                    var sideServiceToRemove = cart.sideServiceResponseDTOs.SingleOrDefault(p => p.Id == sideServiceId);
                    if (sideServiceToRemove != null)
                    {
                        cart.sideServiceResponseDTOs.Remove(sideServiceToRemove);
                        HttpContext.Session.Set(CART_KEY, cart);
                    }
                }
                return Ok(Carts);
            }

            return BadRequest("No valid item ID provided");
        }

        [HttpGet("clear-all")]
        public IActionResult ClearCart()
        {
            HttpContext.Session.Remove(CART_KEY);
            return Ok();
        }

        [HttpPost("checkout")]
        public IActionResult Checkout()
        {
            if (Carts.roomResponseDTOs.Count == 0 && Carts.sideServiceResponseDTOs.Count == 0)
            {
                return Ok("Please add items to cart");
            }

            var user = _userService.GetUserInCookieAsync(User).Result;
            var userDTO = _mapper.Map<ProfileResposeDTO>(user);
            if (ModelState.IsValid)
            {

                var vnPayModel = new VNPaymentRequestModel
                {
                    Amount = Carts.roomResponseDTOs.Sum(p => p.Price) + Carts.sideServiceResponseDTOs.Sum(p => p.Price),
                    CreatedDate = DateTime.Now,
                    Description = $"{userDTO.FirstName} {userDTO.LastName} {userDTO.PhoneNumber}",
                    FullName = $"{userDTO.FirstName} {userDTO.LastName}",
                    OrderId = new Random().Next(1000, 10000)
                };
                return Ok(_vNPayService.CreatePaymentURL(HttpContext, vnPayModel));

            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("payment-callback")]
        public async Task<IActionResult> PaymentCallBack()
        {
            var user = _userService.GetUserInCookieAsync(User).Result;
            var response = _vNPayService.PaymentExecute(Request.Query);

            if (response == null || response.VnPayResponseCode != "00")
            {
                return BadRequest($"Lỗi thanh toán VN Pay: {response.VnPayResponseCode}");
            }

            // Lưu đơn hàng vô database (xu li sau)
            Order order = new Order()
            {
                Id = response.OrderId,
                UserId = user.Id,
                CreatedDate = DateTime.Now,

                Status = "Success",
                Description = response.OrderInfo,
                TotalPrice = response.Amount/100
            };

            // tạo orderSideService, OrderRoom Relationship
            // update room capacity
            await _orderService.AddAsync(order);
            return Ok("PaymentSuccess");
        }
    }
}