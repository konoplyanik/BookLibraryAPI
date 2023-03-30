using AutoMapper;
using BookLibrary.Domain.Core.DTO.UserDTOs;
using BookLibrary.Domain.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Controllers
{
#pragma warning disable CA2254
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(IMapper mapper, ILogger<UserController> logger, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            var request = new AllUsersDto
            {
                UserAmount = users.Count,
                Users = _mapper.Map<List<ApplicationUser>, List<UserView>>(users)
            };

            _logger.LogDebug("All users have been sampled.");

            return Ok(request);
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetUserAsync([FromQuery] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                _logger.LogError("Error: User with given id was not found. Check your input!");
                return BadRequest("Error: User with given id was not found. Check your input!");
            }

            var request = _mapper.Map<ApplicationUser, GetUserDto>(user);

            return Ok(request);
        }

        [HttpPatch("Edit")]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserDto model)
        {
            ApplicationUser applicationUser = await _userManager.FindByEmailAsync(model.Email);
            if (applicationUser == null)
            {
                _logger.LogError($"Error: User: {model.Email} is not registered. Please register first!");
                return BadRequest($"Error: User: {model.Email} is not registered. Please register first!");
            }

            applicationUser.FirstName = model.FirstName;
            applicationUser.LastName = model.LastName;
            applicationUser.Email = model.Email;
            applicationUser.UserName = model.Email;

            await _userManager.UpdateAsync(applicationUser);

            return Ok($"User info: {model.FirstName} {model.LastName}, updated!");
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteUserAsync([FromQuery] string email)
        {
            ApplicationUser applicationUser = await _userManager.FindByEmailAsync(email);
            if (applicationUser == null)
            {
                _logger.LogError($"Error: User {applicationUser.FirstName} {applicationUser.LastName} does not exist!");
                return Ok($"Error: User {applicationUser.FirstName} {applicationUser.LastName} does not exist!");
            }

            await _userManager.DeleteAsync(applicationUser);

            return Ok($"User: {applicationUser.FirstName} {applicationUser.LastName} deleted successfully!");
        }
    }
}
