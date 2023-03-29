using BookLibrary.Domain.Core;
using BookLibrary.Domain.Core.DTO.OrderDTOs;
using BookLibrary.Domain.Core.Messages.Orders;

namespace BookLibrary.Services.Interfaces.Orders
{
    public interface IOrderService
    {
        Task<OrdersResponse> GetAllOrdersAsync();
        Task<OrderResponse> GetOrderAsync(long id);
        Task<BaseResponse> AddOrderAsync(AddOrderDto order);
    }
}
