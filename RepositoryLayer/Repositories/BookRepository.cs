using BookLibrary.Domain.Core.Models;
using BookLibrary.Domain.Interfaces.Books;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Infrastructure.Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _dbContext;

        public BookRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _dbContext.Books.ToListAsync();
        }

        public async Task<Book> GetBookAsync(long id)
        {
            return await _dbContext.Books.Where(b => b.BookId == id).FirstOrDefaultAsync();
        }

        public async Task AddBookAsync(Book book)
        {
            await _dbContext.Books.AddAsync(book);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateBook(Book book)
        {
            var bookValue = await _dbContext.Books.FindAsync(book.BookId);
            if (bookValue == null)
            {
                // ....
            }
            bookValue.Title = book.Title;
            bookValue.Author = book.Author;
            bookValue.Price = book.Price;
            _dbContext.Books.Update(bookValue);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveBook(long id)
        {
            var book = await _dbContext.Books.Where(b => b.BookId == id).FirstOrDefaultAsync();
            if (book == null)
            {
                //...
            }
            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync();
        }

        public bool CheckForExistedBook(string title)
        {
            if (title == null)
            {
                // ...
            }

            return _dbContext.Books.Any(b => b.Title == title);
        }
    }
}
