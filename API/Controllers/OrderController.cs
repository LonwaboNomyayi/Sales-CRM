
using Framework.Contracts;
using Framework.DTO;
using Framework.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace API.Controllers
{
    public class OrderController : ApiController
    {
        private readonly ISalesOrder orderRepo = new SalesOrderRepository();

        
        public async Task<SalesOrderWithOrderlineDTO> Get(string orderNumber)
        {
            return await orderRepo.GetOrderDetailsWithLines(orderNumber);
        }
    }
}