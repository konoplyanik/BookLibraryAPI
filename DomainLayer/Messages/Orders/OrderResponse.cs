using BookLibrary.Domain.Core.Models;

namespace BookLibrary.Domain.Core.Messages.Orders
{
    public class OrderResponse : BaseResponse
    {
        public Order Order { get; set; }
    }
}
