using BookLibrary.Domain.Core;
using BookLibrary.Domain.Core.DTO.BookDTOs;
using BookLibrary.Domain.Core.Messages.Books;

namespace BookLibrary.Services.Interfaces.Books
{
    public interface IBookService
    {
        Task<BooksResponse> GetAllBooksAsync();
        Task<BookResponse> GetBookAsync(long id);
        Task<BaseResponse> AddBookAsync(AddBookDto book);
        Task<BaseResponse> UpdateBookAsync(EditBookDto book);
        Task<BaseResponse> RemoveBookAsync(long id);
    }
}
