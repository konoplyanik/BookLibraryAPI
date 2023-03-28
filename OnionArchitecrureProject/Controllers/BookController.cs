using AutoMapper;
using DomainLayer.DTO.BookDtos;
using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Service.Implementation;
using ServiceLayer.Service.UoW;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI_Layer.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class BookController : ControllerBase
    {
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        private readonly ILogger<BookController> _logger;

        public BookController(IMapper mapper, IUnitOfWork unitOfWork, ILogger<BookController> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAllBooks()
        {
            var repository = _unitOfWork.GetRepository<Book>() as BookService;

            var books = await Task.Run(() => repository.GetAllBooks());

            var request = new AllBooksDto
            {
                BookAmount = books.Count(),
                Books = _mapper.Map<List<Book>, List<BookView>>(books.ToList())
            };

            _logger.LogDebug("Произведена выборка всех книг");

            return StatusCode(200, request);
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetBook(int id)
        {
            var repository = _unitOfWork.GetRepository<Book>() as BookService;

            var book = await Task.Run(() => repository.GetBookById(id));

            if (book == null)
            {
                _logger.LogError($"Ошибка: Книга с id: {id} не найдена. Проверьте корректность ввода!");
                return StatusCode(400, $"Ошибка: Книга с id: {id} не найдена. Проверьте корректность ввода!");
            }

            var request = _mapper.Map<Book, BookDto>(book);

            _logger.LogDebug("Выборка прошла успешно. Выбрана книга: " + book.Title);

            return StatusCode(200, request);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddBook(AddBookDto book)
        {
            var repository = _unitOfWork.GetRepository<Book>() as BookService;
            var newBook = _mapper.Map<AddBookDto, Book>(book);

            await Task.Run(() => repository.AddBook(newBook));

            _unitOfWork.SaveChanges();

            _logger.LogDebug($"Книга: {book.Title} добавлена успешно.");

            return StatusCode(200, $"Книга: {book.Title} добавлена успешно.");
        }

        [HttpPut("edit")]
        public async Task<IActionResult> UpdateBook(EditBookDto book)
        {
            var repository = _unitOfWork.GetRepository<Book>() as BookService;
            var editBook = _mapper.Map<EditBookDto, Book>(book);

            await Task.Run(() => repository.UpdateBook(editBook));

            _unitOfWork.SaveChanges();

            _logger.LogDebug($"Информация о книге: {book.Title} изменена успешно.");

            return StatusCode(200, $"Информация о книге: {book.Title} изменена успешно.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBook(long id)
        {
            var repository = _unitOfWork.GetRepository<Book>() as BookService;

            var book = await Task.Run(() => repository.GetBookById(id));

            if (book == null)
            {
                _logger.LogError($"Ошибка: Книга с id: {id} не найдена. Проверьте корректность ввода!");
                return StatusCode(400, $"Ошибка: Книга с id: {id} не найдена. Проверьте корректность ввода!");
            }

            await Task.Run(() => repository.RemoveBook(id));

            _unitOfWork.SaveChanges();

            _logger.LogDebug($"Книга: {book.Title} успешно удалена из библиотеки.");

            return StatusCode(200, $"Книга: {book.Title} успешно удалена из библиотеки.");
        }
    }
}
