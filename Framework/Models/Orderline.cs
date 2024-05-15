using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Models
{
    public class Orderline
    {
        public int OrderlineKey { get; set; }
        public int LineNumber { get; set; }
        public string ProductCode { get; set; }
        public int ProductTypeId { get; set; }
        public double ProductCostPrice { get; set; }
        public double ProductSalesPrice { get; set; }
        public int Quantity { get; set; }
        public int SalesOrderKey { get; set; }
    }
}
