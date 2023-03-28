using AutoMapper;
using DomainLayer.DTO.UserDtos;
using DomainLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI_Layer.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IMapper _mapper;
        private readonly ILogger<UserController> _logger;
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;

        public UserController(IMapper mapper, ILogger<UserController> logger, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult GetAllUsers()
        {
            var users = _userManager.Users.ToList();

            var request = new AllUsersDto
            {
                UserAmount = users.Count,
                Users = _mapper.Map<List<ApplicationUser>, List<UserView>>(users)
            };

            _logger.LogDebug("Произведена выборка всех пользователей");

            return StatusCode(200, request);
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                _logger.LogError($"Ошибка: Пользователь с данным id не найден. Проверьте корректность ввода!");
                return StatusCode(400, $"Ошибка: Пользователь с данным id не найден. Проверьте корректность ввода!");
            }

            var request = _mapper.Map<ApplicationUser, GetUserDto>(user);

            return StatusCode(200, request);
        }

        [HttpPatch("edit")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto model)
        {
            ApplicationUser applicationUser = await _userManager.FindByEmailAsync(model.Email);
            if (applicationUser == null)
            {
                _logger.LogError($"Ошибка: Пользователь: {model.Email} не зарегестрирован. Сначало пройдите регистрацию!");
                return StatusCode(400, $"Ошибка: Пользователь: {model.Email} не зарегестрирован. Сначало пройдите регистрацию!");
            }

            applicationUser.FirstName = model.FirstName;
            applicationUser.LastName = model.LastName;
            applicationUser.Email = model.Email;
            applicationUser.UserName = model.Email;

            await _userManager.UpdateAsync(applicationUser);

            return StatusCode(200, $"Информация о пользователе: {model.FirstName} {model.LastName}, обновлена!");
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            ApplicationUser applicationUser = await _userManager.FindByEmailAsync(email);
            if (applicationUser == null)
            {
                _logger.LogError($"Ошибка: Пользователь {applicationUser.FirstName} {applicationUser.LastName} не существует!");
                return StatusCode(400, $"Ошибка: Пользователь {applicationUser.FirstName} {applicationUser.LastName} не существует!");
            }

            await _userManager.DeleteAsync(applicationUser);

            return StatusCode(200, $"Пользователь: {applicationUser.FirstName} {applicationUser.LastName} удален!");
        }
    }
}
