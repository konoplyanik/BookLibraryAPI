using BookLibrary.Domain.Core.DTO.BookDTOs;
using Microsoft.AspNetCore.Mvc;
using BookLibrary.Services.Interfaces.Books;

namespace BookLibrary.Controllers
{
#pragma warning disable CA2254
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BookController> _logger;

        public BookController(IBookService bookService, ILogger<BookController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllBooksAsync()
        {
            var response = await _bookService.GetAllBooksAsync();

            if (response.Result.Succeeded)
            {
                _logger.LogDebug("Selected all books");

                return Ok(response);
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetBookAsync([FromQuery] int id)
        {
            var response = await _bookService.GetBookAsync(id);

            if (response.Result.Succeeded)
            {
                _logger.LogDebug($"The selection of books was successful. Selected book with Id: {id}");

                return Ok(response);
            }

            return BadRequest();
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddBookAsync([FromBody] AddBookDto book)
        {
            var response = await _bookService.AddBookAsync(book);

            if (response.Result.Succeeded)
            {
                _logger.LogDebug($"Book: {book.Title} added successfully.");

                return Ok($"Book: {book.Title} added successfully.");
            }

            return BadRequest();
        }

        [HttpPut("Edit")]
        public async Task<IActionResult> UpdateBookAsync([FromBody] EditBookDto book)
        {
            var response = await _bookService.UpdateBookAsync(book);

            if (response.Result.Succeeded)
            {
                _logger.LogDebug($"Book info: {book.Title} changed successfully.");

                return Ok($"Book info: {book.Title} changed successfully.");
            }

            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBookAsync([FromQuery] long id)
        {
            var response = await _bookService.RemoveBookAsync(id);

            if (response.Result.Succeeded)
            {
                _logger.LogDebug($"Book with Id: {id} was successfully removed from the library.");

                return Ok($"Book with id: {id} was successfully removed from the library.");
            }

            return BadRequest();
        }
    }
}
