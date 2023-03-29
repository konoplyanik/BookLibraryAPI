namespace BookLibrary.Domain.Core.DTO.BookDTOs
{
    public class AllBooksDto
    {
        public int BookAmount { get; set; }
        public List<BookView> Books { get; set; }
    }

    public class BookView
    {
        public long BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
    }
}
