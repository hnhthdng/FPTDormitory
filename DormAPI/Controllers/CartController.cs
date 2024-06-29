using AutoMapper;
using DormDataAccess.Service.IServices;
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
using Microsoft.AspNetCore.Identity;
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
        private readonly IRoomOrderService _roomOrderService;
        private readonly IOrderSideServiceService _orderSideServiceService;
        private readonly UserManager<AppUser> _userManager;

        const string CART_KEY = "CART_KEY";

        public CartController(IRoomService roomService, IMapper mapper, ISideServiceService sideServiceService,
            IVNPayService vNPayService, IOrderService orderService, IUserService userService, IRoomOrderService roomOrderService,
            IOrderSideServiceService orderSideServiceService, UserManager<AppUser> userManager)
        {
            _roomService = roomService;
            _mapper = mapper;
            _sideServiceService = sideServiceService;
            _vNPayService = vNPayService;
            _orderService = orderService;
            _userService = userService;
            _roomOrderService = roomOrderService;
            _orderSideServiceService = orderSideServiceService;
            _userManager = userManager;
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
            
            
            //add item to cart
            if (roomId.HasValue || sideServiceId.HasValue)
            {
                var carts = Carts;

                if (roomId.HasValue)
                {
                    //check if room or sideService exist
                    var existingRoomInDB = await _roomService.GetByIdAsync(roomId.Value);
                    if (existingRoomInDB == null)
                    {
                        return BadRequest("Room not found");
                    }

                    //check if room is already in cart
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
                    //check if sideService exist
                    var existingSideServiceInDB = await _sideServiceService.GetByIdAsync(sideServiceId.Value);

                    if (existingSideServiceInDB == null)
                    {
                        return BadRequest("Side Service not found");
                    }

                    //check if sideService is already in cart
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
            var user = _userService.GetUserInCookieAsync(User).Result;

            if (Carts.roomResponseDTOs.Count == 0 && Carts.sideServiceResponseDTOs.Count == 0)
            {
                return Ok("Please add items to cart");
            }

            if (Carts.roomResponseDTOs.Any(r => r.IsMaximum))
            {
                return BadRequest("Maximum room capacity reached");
            }
            if(user.RoomId.HasValue)
            {
                return BadRequest("You has a room in use, please wait for the next semester !");
            }

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

            // Lưu đơn hàng vô database 
            Order order = new Order()
            {
                Id = response.OrderId,
                UserId = user.Id,
                CreatedDate = DateTime.Now,
                Status = "Success",
                Description = response.OrderInfo,
                TotalPrice = response.Amount/100
            };
            await _orderService.AddAsync(order);

            // update room capacity after payment
            List<Room> rooms = new List<Room>();

            foreach (var item in Carts.roomResponseDTOs)
            {
                var room = await _roomService.GetByIdAsync(item.Id);
                rooms.Add(room);
            }
            foreach (var item in rooms)
            {
                item.CurrentNumberOfPeople += 1;
                if (item.CurrentNumberOfPeople == item.MaximumNumberOfPeople) item.IsMaximum = true;
                await _roomService.UpdateAsync(item);
            }

            //update roomId for user (1 user has 1 room in use )
            user.RoomId = rooms[0].Id;
            await _userManager.UpdateAsync(user);

            // tạo orderSideService, OrderRoom Relationship
            foreach (var item in Carts.roomResponseDTOs)
            {
                RoomOrder orderRoom = new RoomOrder
                {
                    OrderId = order.Id,
                    RoomId = item.Id
                };
                await _roomOrderService.CreateRoomOrder(orderRoom);
            }
            foreach (var item in Carts.sideServiceResponseDTOs)
            {
                OrderSideService orderSideService = new OrderSideService
                {
                    OrderId = order.Id,
                    SideServiceId = item.Id
                };
                await _orderSideServiceService.AddOrderSideService(orderSideService);
            }

            //remoce Cart Session 
            HttpContext.Session.Remove(CART_KEY);
            return Ok(order);
        }

       
    }
}