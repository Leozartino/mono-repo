using Kuba.Api.Dtos.User;
using Kuba.Domain.Interfaces;
using Kuba.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Kuba.Api.Controllers
{
    public class AccountController : ApiBaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationSettingsConfig _applicationSettingsConfig;
        private readonly IJwtSettingsHelper _jwtHelper;

        public AccountController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IOptions<ApplicationSettingsConfig> options,
            IJwtSettingsHelper jwtSettingsHelper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _applicationSettingsConfig = options.Value;
            _jwtHelper = jwtSettingsHelper;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApplicationUser>> Register([FromBody] UserRegisterRequest userRegister)
        {

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            ApplicationUser user = new()
            {
                UserName = userRegister.Email,
                Email = userRegister.Email,
                EmailConfirmed = true,

            };

            var result = await _userManager.CreateAsync(user, userRegister.Password);

            if(!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return NoContent();
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserLoginResponse>> Login([FromBody] UserLoginRequest userLogin)
        {
            var result = await _signInManager.PasswordSignInAsync(userLogin.Email, userLogin.Password, false, true);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(userLogin.Email);
                var claims = await AddClaimRoles(user.Email);
                var roles = await _userManager.GetRolesAsync(user!);

                string jwtToken = _jwtHelper.GenerateJWT(_applicationSettingsConfig.JwtSettings, claims);

                UserLoginResponse loginResponse = new()
                {
                    Id = user!.Id,
                    Token = jwtToken,
                    Role = roles.FirstOrDefault() ?? "Employee"
                };

                return Ok(loginResponse);
            }

            return Problem("User and/or password incorrect!");
        }

        private async Task<List<Claim>> AddClaimRoles(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var roles = await _userManager.GetRolesAsync(user!);

            var claims = new List<Claim>
            {
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
    }
}
