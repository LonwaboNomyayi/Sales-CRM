using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Models
{
    public class SalesOrder
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public int OrderTypeId { get; set; }
        public int OrderStatusId { get; set; }
        public string CustomerName { get; set; }
        public string CreateDate { get; set; }

    }
}
