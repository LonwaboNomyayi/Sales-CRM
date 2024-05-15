using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DTO
{
    public class OrderlineDTO
    {
        public int OrderlineKey { get; set; }
        public int LineNumber { get; set; }
        public string ProductCode { get; set; }
        public string ProductType { get; set; }
        public double ProductCostPrice { get; set; }
        public double ProductSalesPrice { get; set; }
        public int Quantity { get; set; }

    }
}
