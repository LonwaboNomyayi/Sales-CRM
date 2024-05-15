using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DTO
{
    public class SalesOrderWithOrderlineDTO
    {
        public SalesOrderDTO OrderHeaderDetails { get; set; }
        public List<OrderlineDTO> Orderlines { get; set; } = new List<OrderlineDTO>();
    }
}
