using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.DTO.OrderDtos
{
    public class AllOrdersDto
    {
        public int OrderAmount { get; set; }
        public List<OrderView> Orders { get; set; }
    }

    public class OrderView
    {
        public long OrderId { get; set; }
        public string Description { get; set; }
        public DateTime DateOfOrder { get; set; }
        public long BookId { get; set; }
    }
}
