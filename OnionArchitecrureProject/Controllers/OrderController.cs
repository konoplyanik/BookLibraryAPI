using BookLibrary.Domain.Core.DTO.OrderDTOs;
using BookLibrary.Domain.Core.Models;
using BookLibrary.Services.Interfaces.Orders;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Controllers
{
#pragma warning disable CA2254
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<BookController> _logger;

        public OrderController(IOrderService orderService, ILogger<BookController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllOrdersAsync()
        {
            var response = await _orderService.GetAllOrdersAsync();

            if (response.Result.Succeeded)
            {
                _logger.LogDebug("Selected all orders.");

                return Ok(response);
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetOrderAsync(int id)
        {
            var response = await _orderService.GetOrderAsync(id);

            if (response.Result.Succeeded)
            {
                _logger.LogDebug($"Order fetching was successful. Selected order with Id: {id}.");

                return Ok(response);
            }

            return BadRequest();
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddOrderAsync(AddOrderDto order)
        {
            var response = await _orderService.AddOrderAsync(order);

            if (response.Result.Succeeded)
            {
                _logger.LogDebug("Order added successfully.");

                return Ok("Order added successfully.");
            }

            return BadRequest();
        }
    }
}
