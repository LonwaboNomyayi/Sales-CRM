using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DTO
{
    public class SalesOrderDTO
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public string OrderType { get; set; }
        public string OrderStatus { get; set; }
        public string CustomerName { get; set; }
        public string CreateDate { get; set; }
    }
}
