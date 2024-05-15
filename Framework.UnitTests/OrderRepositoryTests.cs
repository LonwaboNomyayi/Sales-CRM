using Framework.Contracts;
using Framework.Models;
using Framework.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Framework.UnitTests
{
    [TestClass]
    public class OrderRepositoryTests
    {
        [TestMethod]
        public async Task AddOrEditOrdrHeaderAsync_AddNewOrder_ReturnsTrue()
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
            ISalesOrder repo = new SalesOrderRepository();

            //Act
            var result = await repo.AddOrEditOrdrHeaderAsync(order);

            //Assert
            Assert.IsTrue(result);
        }
    }
}
