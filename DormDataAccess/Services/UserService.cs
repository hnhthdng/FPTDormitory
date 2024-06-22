using AutoMapper;
using DormDataAccess.Service.IServices;
using DormModel.Model;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;


namespace DormDataAccess.Services
{
    public class UserService : IUserService
    {
        private readonly SignInManager<AppUser> _signInManager;
        public UserService(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }
        public async Task<AppUser> GetUserInCookieAsync(ClaimsPrincipal user)
        {
            var email = user?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (email != null)
            {
                return await _signInManager.UserManager.FindByEmailAsync(email);
            }

            return null;
        }
    }
}
