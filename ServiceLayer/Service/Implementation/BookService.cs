using DomainLayer.Models;
using RepositoryLayer;

namespace ServiceLayer.Service.Implementation
{
    public class BookService : Repository<Book>
    {
        public BookService(AppDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return Set.AsEnumerable();
        }

        public Book GetBookById(long id)
        {
            return Set.AsEnumerable().Where(b => b.BookId == id).FirstOrDefault();
        }

        public string AddBook(Book book)
        {
            try
            {
                Set.Add(book);

                return "Success";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string UpdateBook(Book book)
        {
            try
            {
                var bookValue = Set.Find(book.BookId);

                if (bookValue != null)
                {
                    bookValue.Title = book.Title;
                    bookValue.Author = book.Author;
                    bookValue.Price = book.Price;
                    Set.Update(bookValue);
                    return "Successfully Updated";
                }
                else
                {
                    return "No Record(s) Found";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public string RemoveBook(long id)
        {
            try
            {
                var book = Set.Where(b => b.BookId == id).FirstOrDefault();
                Set.Remove(book);

                return "Successfully Removed";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
