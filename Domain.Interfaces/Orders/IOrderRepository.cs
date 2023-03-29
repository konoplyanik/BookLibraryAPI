using BookLibrary.Domain.Core.Models;

namespace BookLibrary.Domain.Interfaces.Orders
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderAsync(long id);
        Task AddOrderAsync(Order book);
    }
}
