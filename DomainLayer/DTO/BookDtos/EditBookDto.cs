namespace BookLibrary.Domain.Core.DTO.BookDTOs
{
    public class EditBookDto
    {
        public long BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
    }
}
