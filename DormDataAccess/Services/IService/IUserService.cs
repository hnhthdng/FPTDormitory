using DormModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DormDataAccess.Service.IServices
{
    public interface IUserService
    {
        Task<AppUser> GetUserInCookieAsync(ClaimsPrincipal user);
    }
}
