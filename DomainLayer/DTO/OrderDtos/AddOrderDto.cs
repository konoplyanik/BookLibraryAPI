using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.DTO.OrderDtos
{
    public class AddOrderDto
    {
        public long BookId { get; set; }
        public string UserEmail { get; set; }

    }
}
