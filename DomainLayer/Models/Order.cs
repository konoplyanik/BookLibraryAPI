namespace BookLibrary.Domain.Core.Models
{
    public class Order
    {
        public long OrderId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime DateOfOrder { get; set; }
        public long BookId { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
