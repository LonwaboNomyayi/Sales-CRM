using Framework.DTO;
using Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Contracts
{
    public interface ISalesOrder
    {
        Task<IReadOnlyList<SalesOrderDTO>> GetAllSalesOrdersAsync(OrderFilter filter);
        Task<IReadOnlyList<OrderStatus>> GetOrderStatusesAsync();
        Task<IReadOnlyList<OrderType>> GetAllOrderTypesAsync();
        Task<SalesOrder> GetSalesOrderByIdAsync(int id);
        Task<bool> AddOrEditOrdrHeaderAsync(SalesOrder order);
        Task<IReadOnlyList<OrderlineDTO>> GetAllOrderlinesAsync(int OrderId);
        Task<IReadOnlyList<ProductType>> GetAllProductTypesAsync();
        Task<bool> AddOrEditOrderlineAsync(Orderline lineItem);
        Task<Orderline> GetOrderlineByIdAsync(int OrderlineId);
        Task<bool> DeleteOrderlineAsync(OrderlineDeleteDTO info);
        Task<SalesOrderWithOrderlineDTO> GetOrderDetailsWithLines(string orderNumber);
    }
}
