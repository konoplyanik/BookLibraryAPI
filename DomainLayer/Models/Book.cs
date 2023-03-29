namespace BookLibrary.Domain.Core.Models
{
    public class Book
    {
        public long BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
    }
}
