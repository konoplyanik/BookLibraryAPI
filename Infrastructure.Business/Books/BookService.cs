using AutoMapper;
using BookLibrary.Domain.Core;
using BookLibrary.Domain.Core.DTO.BookDTOs;
using BookLibrary.Domain.Core.Infrastructure;
using BookLibrary.Domain.Core.Messages.Books;
using BookLibrary.Domain.Core.Models;
using BookLibrary.Domain.Interfaces.Books;
using BookLibrary.Services.Interfaces.Books;
using Microsoft.Extensions.Logging;
using System.Net;

namespace BookLibrary.Infrastructure.Business.Books
{
#pragma warning disable CA2254
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<BookService> _logger;

        public BookService(IBookRepository bookRepository, IMapper mapper, ILogger<BookService> logger)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<BooksResponse> GetAllBooksAsync()
        {
            var response = await _bookRepository.GetAllBooksAsync();

            _logger.LogDebug("Selected all books");

            if (response == null)
            {
                return BaseResponse.Failure<BooksResponse>(HttpStatusCode.NotFound,
                    Constants.Validation.Books.BooksNotFound());
            }

            return new BooksResponse { Books = response };
        }

        public async Task<BookResponse> GetBookAsync(long id)
        {
            var book = await _bookRepository.GetBookAsync(id);

            if (book == null)
            {
                _logger.LogError($"Error: Book with id: {id} not found. Check the correctness of the input!");
                return BaseResponse.Failure<BookResponse>(HttpStatusCode.NotFound,
                    Constants.Validation.Books.BookNotFound(id));
            }

            _logger.LogDebug("The selection of books was successful. Book selected: " + book.Title);

            return new BookResponse { Book = book };
        }

        public async Task<BaseResponse> AddBookAsync(AddBookDto book)
        {
            var newBook = _mapper.Map<AddBookDto, Book>(book);

            await _bookRepository.AddBookAsync(newBook);

            _logger.LogDebug($"Book: {book.Title} added successfully.");

            return BaseResponse.Success;
        }

        public async Task<BaseResponse> UpdateBookAsync(EditBookDto book)
        {
            var editBook = _mapper.Map<EditBookDto, Book>(book);

            await _bookRepository.UpdateBookAsync(editBook);

            _logger.LogDebug($"Book info: {book.Title} changed successfully.");

            return BaseResponse.Success;
        }

        public async Task<BaseResponse> RemoveBookAsync(long id)
        {
            var book = await _bookRepository.GetBookAsync(id);

            if (book == null)
            {
                _logger.LogError($"Error: Book with id: {id} not found. Check the correctness of the input!");
                return BaseResponse.Failure(HttpStatusCode.NotFound, "");

            }

            await _bookRepository.RemoveBookAsync(book);

            _logger.LogDebug($"Book: {book.Title} successfully removed from library.");

            return BaseResponse.Success;
        }
    }
}
