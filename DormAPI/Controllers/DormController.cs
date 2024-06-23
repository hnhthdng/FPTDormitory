using DormDataAccess.Services.IService;
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

        public DormController(IDormService dormService)
        {
            _dormService = dormService;
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
    }
}
