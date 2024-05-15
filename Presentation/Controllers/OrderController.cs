using Framework.Contracts;
using Framework.DTO;
using Framework.Helpers;
using Framework.Models;
using Framework.Repository;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        // GET: Order

        private readonly ISalesOrder _orderRepo = new SalesOrderRepository();

        #region Views 
        public ActionResult Index()
        {
            if (User.IsInRole(ApplicationConstants.CanManageOrders))
            {
                return View();
            }
            else
            {
                return View("OrderDetailsReader");
            }
        }

        [Authorize(Roles = ApplicationConstants.CanManageOrders)]
        public ActionResult AddOrEditOrder(int Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [Authorize(Roles = ApplicationConstants.CanManageOrders)]
        public async Task<ActionResult> Orderlines(int Id)
        {
            ViewBag.OrderId = Id;

            var order = await _orderRepo.GetSalesOrderByIdAsync(Id);
            ViewBag.OrderNumber = order.OrderNumber;
            ViewBag.CustomerName = order.CustomerName;
            ViewBag.CreateDate = DateTime.Parse(order.CreateDate).ToShortDateString();
            return View();
        }

        public ActionResult OrderDetailsReader()
        {
            return View();
        }

        public async Task<ActionResult> OrderlineReader(int Id)
        {
            ViewBag.OrderId = Id;

            var order = await _orderRepo.GetSalesOrderByIdAsync(Id);
            ViewBag.OrderNumber = order.OrderNumber;
            ViewBag.CustomerName = order.CustomerName;
            ViewBag.CreateDate = DateTime.Parse(order.CreateDate).ToShortDateString();
            return View();
        }


        #endregion


        #region Data Routines 

        #region Get Requests 
        [HttpGet]
        public async Task<JsonResult> GetAllOrdersAsync(string OrderNumber, string OrderType, string FromDate, string ToDate)
        {
            // 
            var filter = new OrderFilter { OrderNumber = OrderNumber == null ? String.Empty: OrderNumber, OrderType = int.Parse(OrderType == null ? "-1": OrderType), FromDate = FromDate == null ? String.Empty: FromDate, ToDate = ToDate == null ? String.Empty: ToDate };
            var orders = await _orderRepo.GetAllSalesOrdersAsync(filter);
            return Json(new { data = orders }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetAllOrderStatusesAsync()
        {
            var statuses = await _orderRepo.GetOrderStatusesAsync();
            return Json(new { data = statuses }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<JsonResult> GetAllOrderTypesAsync()
        {
            var orderTypes = await _orderRepo.GetAllOrderTypesAsync();
            return Json(new { data = orderTypes }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<JsonResult> GetSalesOrderByIdAsync(int Id)
        {
            var order = await _orderRepo.GetSalesOrderByIdAsync(Id);
            return Json(new { data = order }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetAllOrderlinesAsync(int Id)
        {
            var orderlines = await _orderRepo.GetAllOrderlinesAsync(Id);
            return Json(new { data = orderlines }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetOrderlineByIdAsync(int Id)
        {
            var orderlines = await _orderRepo.GetOrderlineByIdAsync(Id);
            return Json(new { data = orderlines }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetAllProductTypesAsync()
        {
            var productTypes = await _orderRepo.GetAllProductTypesAsync();
            return Json(new { data = productTypes }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Post Requests
        [HttpPost]
        [Authorize(Roles = ApplicationConstants.CanManageOrders)]
        public async Task<JsonResult> AddOrEditOrderHeaderAsync(SalesOrder order)
        {
            var response = await _orderRepo.AddOrEditOrdrHeaderAsync(order);
            return Json(new { success = response }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize(Roles = ApplicationConstants.CanManageOrders)]
        public async Task<JsonResult> AddOrEditOrderlineAsync(Orderline orderline)
        {
            var response = await _orderRepo.AddOrEditOrderlineAsync(orderline);
            return Json(new { success = response }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        [HttpPost]
        [Authorize(Roles = ApplicationConstants.CanManageOrders)]
        public async Task<JsonResult> DeleteOrderlineAsync(OrderlineDeleteDTO info)
        {
            var response = await _orderRepo.DeleteOrderlineAsync(info);
            return Json(new { success = response }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}