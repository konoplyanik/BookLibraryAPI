using BookLibrary.Domain.Core.Models;

namespace BookLibrary.Domain.Core.Messages.Books
{
    public class BooksResponse : BaseResponse
    {
        public IEnumerable<Book> Books { get; set; }
    }
}
