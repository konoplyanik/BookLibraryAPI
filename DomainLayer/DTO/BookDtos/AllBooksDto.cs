using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.DTO.BookDtos
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
