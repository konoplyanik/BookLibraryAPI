using BookLibrary.Domain.Core.Models;

namespace BookLibrary.Domain.Core.Messages.Orders
{
    public class OrdersResponse : BaseResponse
    {
        public IEnumerable<Order> Orders { get; set; }
    }
}
