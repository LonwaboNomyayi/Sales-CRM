using Framework.Contracts;
using Framework.Models;
using Framework.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace SalesOrder_CRM_Testing
{
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            //Arrange
            SalesOrder order = new SalesOrder()
            {
                Id = 0,
                OrderNumber = "SO101010101",
                OrderStatusId = 1,
                OrderTypeId = 1,
                CustomerName = "Lubabalo",
                CreateDate = DateTime.Now.ToString()
            };

            //Act
            ISalesOrder repo = new SalesOrderRepository();
            var result = await repo.AddOrEditOrdrHeaderAsync(order);

            //Assert
            Assert.IsTrue(result);
        }
    }
}
