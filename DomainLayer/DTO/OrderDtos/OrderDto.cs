namespace DomainLayer.DTO.OrderDtos
{
    public class OrderDto
    {
        public long OrderId { get; set; }
        public string Description { get; set; }
        public DateTime DateOfOrder { get; set; }
        public long BookId { get; set; }
    }
}
