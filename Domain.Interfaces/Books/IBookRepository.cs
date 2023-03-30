using BookLibrary.Domain.Core.Models;

namespace BookLibrary.Domain.Interfaces.Books
{
    public interface IBookRepository
    {
        Task <IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> GetBookAsync(long id);
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task RemoveBookAsync(Book book);
    }
}
