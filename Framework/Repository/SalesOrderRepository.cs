using Framework.Contracts;
using Framework.DTO;
using Framework.Helpers;
using Framework.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Repository
{
    public class SalesOrderRepository : ISalesOrder
    {

        public async Task<IReadOnlyList<SalesOrderDTO>> GetAllSalesOrdersAsync(OrderFilter filter)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@OrderNumber", filter.OrderNumber),
                    new SqlParameter("@OrderType", filter.OrderType),
                    new SqlParameter("@FromDate", filter.FromDate),
                    new SqlParameter("@ToDate", filter.ToDate)
                };



                DataTable dt = await Task.Run(() => DBContext.GetParamatizedQuery("SP_Get_AllOrders", parameters));

                var orders = new List<SalesOrderDTO>();

                foreach (DataRow row in dt.Rows)
                {

                    var order = new SalesOrderDTO
                    {
                        Id = (int)row["Id"],
                        OrderNumber = row["OrderNumber"].ToString().Trim(),
                        OrderType = row["OrderType"].ToString().Trim(),
                        OrderStatus = row["OrderStatus"].ToString().Trim(),
                        CustomerName = row["CustomerName"].ToString().Trim(),
                        CreateDate = row["CreateDate"].ToString().Trim()
                    };
                    orders.Add(order);
                }
                return orders;
            }
            catch (Exception ex)
            {
                ex.Logger();
            }
            return null;
        }

        public async Task<SalesOrder> GetSalesOrderByIdAsync(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@OrderId", id) };
                DataTable dt = await Task.Run(() => DBContext.GetParamatizedQuery("SP_Get_OrderById", parameters));

                if (dt.Rows.Count > 0)
                {
                    return new SalesOrder
                    {
                        Id = (int)dt.Rows[0]["Id"],
                        OrderNumber = dt.Rows[0]["OrderNumber"].ToString().Trim(),
                        OrderTypeId = (int)dt.Rows[0]["OrderTypeId"],
                        OrderStatusId = (int)dt.Rows[0]["OrderStatusId"],
                        CustomerName = dt.Rows[0]["CustomerName"].ToString().Trim(),
                        CreateDate = dt.Rows[0]["CreateDate"].ToString().Trim()
                    };

                }
            }
            catch (Exception ex)
            {
                ex.Logger();
            }
            return null;
        }


        public async Task<IReadOnlyList<OrderStatus>> GetOrderStatusesAsync()
        {
            try
            {
                DataTable dt = await Task.Run(() => DBContext.GetSelectQuery("SP_Get_AllStatuses"));

                var statuses = new List<OrderStatus>();

                foreach (DataRow row in dt.Rows)
                {

                    var status = new OrderStatus
                    {
                        OrderStatusId = (int)row["Id"],
                        OrderStatusDesc = row["OrderStatusDesc"].ToString().Trim()
                    };

                    statuses.Add(status);
                }
                return statuses;
            }
            catch (Exception ex)
            {
                ex.Logger();
            }
            return null;
        }

        public async Task<IReadOnlyList<OrderType>> GetAllOrderTypesAsync()
        {
            try
            {
                DataTable dt = await Task.Run(() => DBContext.GetSelectQuery("SP_Get_AllOrderTypes"));

                var orderTypes = new List<OrderType>();

                foreach (DataRow row in dt.Rows)
                {

                    var typeOfOrder = new OrderType
                    {
                        Id = (int)row["Id"],
                        OrderTypeDesc = row["OrderTypeDesc"].ToString().Trim()
                    };

                    orderTypes.Add(typeOfOrder);
                }
                return orderTypes;
            }
            catch (Exception ex)
            {
                ex.Logger();
            }
            return null;
        }

        public async Task<bool> AddOrEditOrdrHeaderAsync(SalesOrder order)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@OrderId", order.Id),
                    new SqlParameter("@OrderNumber", order.OrderNumber),
                    new SqlParameter("@OrderStatusId", order.OrderStatusId),
                    new SqlParameter("@OrderTypeId", order.OrderTypeId),
                    new SqlParameter("@CustomerName", order.CustomerName),
                    new SqlParameter("@CreateDate", order.CreateDate.Replace("T"," "))
                };
                return await Task.Run(() => DBContext.ExecuteNonQuery("SP_iNSERT_UPDATE_Order", parameters));
            }
            catch (Exception ex)
            {
                ex.Logger();
            }
            return false;
        }

        public async Task<IReadOnlyList<OrderlineDTO>> GetAllOrderlinesAsync(int OrderId)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@OrderId", OrderId) };
                DataTable dt = await Task.Run(() => DBContext.GetParamatizedQuery("SP_Get_OrderlinesByOrderId", parameters));


                var orderlines = new List<OrderlineDTO>();

                foreach (DataRow row in dt.Rows)
                {
                    var orderline = new OrderlineDTO
                    {
                        OrderlineKey = (int)row["OrderlineKey"],
                        LineNumber = (int)row["LineNumber"],
                        ProductCode = row["ProductCode"].ToString().Trim(),
                        ProductType = row["ProductType"].ToString().Trim(),
                        ProductCostPrice = double.Parse(row["ProductCostPrice"].ToString().Trim()),
                        ProductSalesPrice = double.Parse(row["ProductSalesPrice"].ToString().Trim()),
                        Quantity = (int)row["Quantity"]
                    };
                    orderlines.Add(orderline);
                }
                return orderlines;
            }
            catch (Exception ex)
            {
                ex.Logger();
            }
            return null;
        }

        public async Task<IReadOnlyList<ProductType>> GetAllProductTypesAsync()
        {
            try
            {
                DataTable dt = await Task.Run(() => DBContext.GetSelectQuery("SP_Get_ProductTypes"));


                var productTypes = new List<ProductType>();

                foreach (DataRow row in dt.Rows)
                {
                    var type = new ProductType
                    {
                        Id = (int)row["Id"],
                        ProductTypeDesc = row["ProductTypeDesc"].ToString().Trim()
                    };
                    productTypes.Add(type);
                }
                return productTypes;
            }
            catch (Exception ex)
            {
                ex.Logger();
            }
            return null;
        }

        public async Task<bool> AddOrEditOrderlineAsync(Orderline lineItem)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@OrderlineId", lineItem.OrderlineKey),
                    new SqlParameter("@SalesOrderKey", lineItem.SalesOrderKey),
                    new SqlParameter("@LineNumber", lineItem.LineNumber),
                    new SqlParameter("@ProductCode", lineItem.ProductCode),
                    new SqlParameter("@ProductTypeId", lineItem.ProductTypeId),
                    new SqlParameter("@ProductCostPrice", lineItem.ProductCostPrice),
                    new SqlParameter("@ProductSalesPrice", lineItem.ProductSalesPrice),
                    new SqlParameter("@Quantity", lineItem.Quantity)
                };
                return await Task.Run(() => DBContext.ExecuteNonQuery("SP_iNSERT_UPDATE_Orderline", parameters));
            }
            catch (Exception ex)
            {
                ex.Logger();
            }
            return false;
        }

        public async Task<Orderline> GetOrderlineByIdAsync(int OrderlineId)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@OrderlineId", OrderlineId) };
                DataTable dt = await Task.Run(() => DBContext.GetParamatizedQuery("SP_Get_OrderlineById", parameters));

                if (dt.Rows.Count > 0)
                {
                    return new Orderline
                    {
                        OrderlineKey = OrderlineId,
                        LineNumber = (int)dt.Rows[0]["LineNumber"],
                        ProductCode = dt.Rows[0]["ProductCode"].ToString().Trim(),
                        ProductTypeId = (int)dt.Rows[0]["ProductTypeId"],
                        ProductSalesPrice = double.Parse(dt.Rows[0]["SalesPrice"].ToString().Trim()),
                        ProductCostPrice = double.Parse(dt.Rows[0]["ProductCostPrice"].ToString().Trim()),
                        Quantity = (int)dt.Rows[0]["Quantity"]
                    };

                }
            }
            catch (Exception ex)
            {
                ex.Logger();
            }
            return null;
        }

        public async Task<bool> DeleteOrderlineAsync(OrderlineDeleteDTO info)
        {
            try
            {
                //step 1: we need to delete the orderline with the Id we have in memory 
                var response = await DeleteOrderlineInternalAsync(info.OrderlineId);
                if (response)
                {
                    //we deleted the record, now lets call the list in memory 

                    //step 2: we need to arrange the line number to offset the deleted value 
                    //step 2.1: we are going to start at the value of i, where i was the position that used to leave our deleted member 
                    var orderlines = await GetOrderlinesByOrderIdInternalAsync(info.OrderId);

                    for (var i = info.OrderlineIndex; i < orderlines.Count; i++)
                    {
                        //worst case would be in the event were we have deleted the item that was line number 1 - thereby making this O(n)
                        //ofcourse best scenario would be deleting the very last element which wont affect anything 
                        //At least we will hit a O(1) for deleting  (i - 2) item, where i is the lenth of the list

                        var order = orderlines[i];
                        order.LineNumber--;

                        //this will update the line to the correct numbering 
                        await AddOrEditOrderlineAsync(order);
                    }
                    return true;
                }

            }
            catch (Exception ex)
            {
                ex.Logger();
            }
            return false;
        }


        public async Task<SalesOrderWithOrderlineDTO> GetOrderDetailsWithLines(string orderNumber)
        {
            try
            {
                //instead of writing a new filter - why not use the exisitng one ?

                var orders = await GetAllSalesOrdersAsync(new OrderFilter { OrderNumber = orderNumber,FromDate = "",ToDate = "",OrderType = -1 });
                var orderWithOrderlines = new SalesOrderWithOrderlineDTO();
                //we have to be careful here - there is a possibility that the above call returns more than 1 record. This is because the sub query involving the where clauses uses like 

                if(orders.Count > 0)
                {
                    //the query uses like so let be more specific here 
                    var orderFilter = orders.Where(o => o.OrderNumber == orderNumber);
                    if(orderFilter == null)
                    {
                        return orderWithOrderlines;
                    }
                    else
                    {
                        orderWithOrderlines.OrderHeaderDetails = orderFilter.FirstOrDefault();
                        var orderlines = await GetAllOrderlinesAsync(orders[0].Id);
                        orderWithOrderlines.Orderlines.AddRange(orderlines);
                    }
                    
                }
                //else if(orders.Count == 1)
                //{
                //    orderWithOrderlines.OrderHeaderDetails = orders[0];

                //    //awesome - order has an id that we can use to call a local resource that can get use the orderlines 
                //    var orderlines = await GetAllOrderlinesAsync(orders[0].Id);

                //    orderWithOrderlines.Orderlines.AddRange(orderlines);
                //    //foreach (var line in orderlines)
                //    //{
                //    //    orderWithOrderlines.Orderlines.Add(line);
                //    //}
                //    return orderWithOrderlines;

                //}
                return orderWithOrderlines;
            }
            catch(Exception ex)
            {
                ex.Logger();
            }
            return null;
        }

        #region Private Routines 
        private async Task<bool> DeleteOrderlineInternalAsync(int orderlineId)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@OrderlineId", orderlineId)
                };
                return await Task.Run(() => DBContext.ExecuteNonQuery("SP_DELETE_Orderline", parameters));
            }
            catch (Exception ex)
            {
                ex.Logger();
            }
            return false;
        }

        private async Task<List<Orderline>> GetOrderlinesByOrderIdInternalAsync(int orderId)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@OrderId", orderId) };
                DataTable dt = await Task.Run(() => DBContext.GetParamatizedQuery("SP_Get_OrderlinesByOrderIdInternal", parameters));


                var orderlines = new List<Orderline>();

                foreach (DataRow row in dt.Rows)
                {
                    var orderline = new Orderline
                    {
                        OrderlineKey = (int)row["OrderlineKey"],
                        LineNumber = (int)row["LineNumber"],
                        ProductCode = row["ProductCode"].ToString().Trim(),
                        ProductTypeId = (int)row["ProductTypeId"],
                        ProductCostPrice = double.Parse(row["ProductCostPrice"].ToString().Trim()),
                        ProductSalesPrice = double.Parse(row["SalesPrice"].ToString().Trim()),
                        Quantity = (int)row["Quantity"],
                        SalesOrderKey = (int)row["SalesOrderId"]
                    };
                    orderlines.Add(orderline);
                }
                return orderlines;
            }
            catch (Exception ex)
            {
                ex.Logger();
            }
            return null;
        }


        #endregion

    }
}
