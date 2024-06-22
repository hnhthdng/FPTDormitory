using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using DormModel.Model;
using Azure;
using DormModel.DTO.Authentication;
using Microsoft.AspNetCore.Identity.UI.Services;
using DormUtility.Email;

namespace DormAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DormUtility.Email.IEmailSender _emailSender;

        public AuthenticationController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager,
                RoleManager<IdentityRole> roleManager, DormUtility.Email.IEmailSender emailSender)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDTO model, string role)
        {
            if (ModelState.IsValid)
            {
                var findUserByMail = await _signInManager.UserManager.FindByEmailAsync(model.Email);
                if (findUserByMail != null)
                {
                    return BadRequest("User has been registered");
                }
                else
                {
                    var user = new AppUser { UserName = model.Username, Email = model.Email };

                    if (await _roleManager.RoleExistsAsync(role))
                    {
                        var result = await _userManager.CreateAsync(user, model.Password);
                        if (!result.Succeeded)
                        {
                            return StatusCode(StatusCodes.Status500InternalServerError);
                        }
                        //Add role to the user
                        await _userManager.AddToRoleAsync(user, role);
                        return StatusCode(StatusCodes.Status200OK);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError);
                    }

                }

            }

            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDTO model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, isPersistent: true, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                //Checking the user
                var user = await _signInManager.UserManager.FindByNameAsync(model.Username);
                if (user != null)
                {
                    var authClaim = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email)
                };

                    var userRoles = await _signInManager.UserManager.GetRolesAsync(user);
                    foreach (var role in userRoles)
                    {
                        authClaim.Add(new Claim(ClaimTypes.Role, role));
                    }

                    var claimsIdentity = new ClaimsIdentity(authClaim, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    return Ok();
                }
            }
            return Unauthorized();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }

        [HttpGet("accessdenied")]
        public IActionResult AccessDenied()
        {
            return Forbid();
        }


        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return BadRequest("User is not registered");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetUrl = Url.Action("ResetPassword", "Authentication", new { token, email }, Request.Scheme);
            var message = new EmailMessage(new[] { user.Email! }, "Reset Password", $"Please reset your password by clicking here: <a href='{resetUrl}'>link</a>");
            _emailSender.SendEmail(message);
            return Ok("Mail has been send");
        }


        [HttpGet("reset-password")]
        public async Task<IActionResult> ResetPassword(string token, string email)
        {
            var model = new ResetPasswordRequestDTO { Token = token, Email = email };
            return Ok(new { model });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return BadRequest("User is not registered");

            var resetPassResult = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
            if (!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            return Ok("Password has been changed");
        }

    }
}
