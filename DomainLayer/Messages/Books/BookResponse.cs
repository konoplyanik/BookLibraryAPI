using BookLibrary.Domain.Core.Models;

namespace BookLibrary.Domain.Core.Messages.Books
{
    public class BookResponse : BaseResponse
    {
        public Book Book { get; set; }
    }
}
