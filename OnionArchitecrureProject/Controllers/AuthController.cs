using System.IdentityModel.Tokens.Jwt;
using DomainLayer.DTO.AuthDtos;
using DomainLayer.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IIdentityServerInteractionService _interactionService;
        private ILogger<AuthController> _logger;
        private readonly IConfiguration _configuration;

        public AuthController(SignInManager<ApplicationUser> signInManager, 
            UserManager<ApplicationUser> userManager, 
            IIdentityServerInteractionService interactionService, 
            ILogger<AuthController> logger,
            IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _interactionService = interactionService;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                _logger.LogDebug($"Новый пользователь {user.FirstName} {user.LastName} успешно зарегистрирован.");
                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token), expiration = token.ValidTo });

            }
            _logger.LogError("Произошла ошибка авторизации!");
            return Unauthorized();
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
                Created = DateTime.Now
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                _logger.LogError($"Ошибка: Error occurred");
                return StatusCode(400, $"Ошибка: Error occurred");
            }

            await _signInManager.SignInAsync(user, false);

            return StatusCode(200, model);
        }

        //[HttpGet]
        //public async Task<IActionResult> Logout(string logoutId)
        //{
        //    await _signInManager.SignOutAsync();

        //    var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);

        //    return Redirect(logoutRequest.PostLogoutRedirectUri);
        //}
    }
}
