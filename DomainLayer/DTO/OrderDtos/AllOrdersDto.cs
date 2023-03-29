namespace BookLibrary.Domain.Core.DTO.OrderDTOs
{
    public class AllOrdersDto
    {
        public int OrderAmount { get; set; }
        public List<OrderView> Orders { get; set; }
    }

    public class OrderView
    {
        public long OrderId { get; set; }
        public string Description { get; set; }
        public DateTime DateOfOrder { get; set; }
        public long BookId { get; set; }
    }
}
