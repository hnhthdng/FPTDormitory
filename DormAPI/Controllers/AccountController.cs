
﻿using AutoMapper;
using DormDataAccess.Service.IServices;
using DormModel.DTO.Account;
using DormModel.Model;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DormAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper, IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _userService = userService;
        }


        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userService.GetUserInCookieAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }
            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                return Ok(new { Message = "Password changed successfully" });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest(ModelState);
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var user = await _userService.GetUserInCookieAsync(User);
            // Sử dụng AutoMapper để map từ User sang ProfileRequestDTO
            var userDto = _mapper.Map<ProfileResposeDTO>(user);
            if (userDto == null)
            {
                return Unauthorized();
            }
            return Ok(userDto);

        }

        [HttpPost("profile")]
        public async Task<IActionResult> UpdateProfile(ProfileRequestDTO requestModel)
        {
            var user = await _userService.GetUserInCookieAsync(User);
            _mapper.Map(requestModel, user);
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok("Update Success");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return BadRequest(ModelState);
        }
    }
}
