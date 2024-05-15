using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DTO
{
    public class OrderlineDeleteDTO
    {
        public int OrderId { get; set; }
        public int OrderlineId { get; set; }
        public int OrderlineIndex { get; set; }
    }
}
