using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.DTO.BookDtos
{
    public class AddBookDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
    }
}
