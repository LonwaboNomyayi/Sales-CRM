using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DTO
{
    public class OrderFilter
    {
        public string OrderNumber { get; set; }
        public int OrderType { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}
