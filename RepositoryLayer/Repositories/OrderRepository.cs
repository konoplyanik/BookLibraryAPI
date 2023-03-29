using BookLibrary.Domain.Core.Models;
using BookLibrary.Domain.Interfaces.Orders;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Infrastructure.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _dbContext;

        public OrderRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _dbContext.Orders.ToListAsync();
        }

        public async Task<Order> GetOrderAsync(long id)
        {
            return await _dbContext.Orders.Where(b => b.OrderId == id).FirstOrDefaultAsync();
        }

        public async Task AddOrderAsync(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
        }
    }
}
