using AutoMapper;
using BookLibrary.Domain.Core;
using BookLibrary.Domain.Core.DTO.OrderDTOs;
using BookLibrary.Domain.Core.Infrastructure;
using BookLibrary.Domain.Core.Messages.Orders;
using BookLibrary.Domain.Core.Models;
using BookLibrary.Domain.Interfaces.Orders;
using BookLibrary.Services.Interfaces.Orders;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Net;
using BookLibrary.Domain.Interfaces.Books;

namespace BookLibrary.Infrastructure.Business.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<OrderService> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBookRepository _bookRepository;

        public OrderService(IOrderRepository orderRepository, ILogger<OrderService> logger, UserManager<ApplicationUser> userManager, IBookRepository bookRepository)
        {
            _orderRepository = orderRepository;
            _logger = logger;
            _userManager = userManager;
            _bookRepository = bookRepository;
        }

        public async Task<OrdersResponse> GetAllOrdersAsync()
        {
            var response = await _orderRepository.GetAllOrdersAsync();

            if (response == null)
            {
                return BaseResponse.Failure<OrdersResponse>(HttpStatusCode.NotFound,
                    Constants.Validation.Orders.OrdersNotFound());
            }

            return new OrdersResponse { Orders = response };
        }

        public async Task<OrderResponse> GetOrderAsync(long id)
        {
            var order = await _orderRepository.GetOrderAsync(id);

            return new OrderResponse { Order = order };
        }

        public async Task<BaseResponse> AddOrderAsync(AddOrderDto order)
        {
            var user = await _userManager.FindByEmailAsync(order.UserEmail);

            if (user == null)
                return BaseResponse.Failure<OrdersResponse>(HttpStatusCode.NotFound,
                    Constants.Validation.Users.UserNotFound(user.Id));

            var book = await _bookRepository.GetBookAsync(order.BookId);

            var newOrder = new Order()
            {
                Description = $"Order: {book.Author} - {book.Title}",
                Price = book.Price,
                BookId = order.BookId,
                ApplicationUserId = user.Id,
            };

            await _orderRepository.AddOrderAsync(newOrder);

            _logger.LogDebug($"Order added successfully.");

            return BaseResponse.Success;
        }
    }
}
