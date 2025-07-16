using ex10bis.Core.Entities;
using ex10bis.Core.Order.Dtos;
using ex10bis.Core.Order.Interfaces;
using ex10bis.Core.Order.UseCases;
using Moq;

namespace UnitTests
{
    [TestClass]
    public class OrderUseCaseTests
    {
        [TestMethod]
        public async Task CreateOrder_ShouldReturnSuccess_WhenValidRequest()
        {
            // Arrange
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var useCase = new OrderUseCase(orderRepositoryMock.Object);
            var request = new CreateOrderRequest
            (
                CustomerId: 1,
                Customer: new Customer { Id = 1, Name = "Test Customer" },
                WarehouseId: 1,
                Warehouse: new Warehouse { Id = 1, Name = "Test Warehouse" },
                Delivery: null,
                Facture: null,
                OrderDate: DateTime.Now,
                OrderStatus: OrderStatus.Submitted,
                OrderDetails: new List<OrderDetail>(),
                ShippingCost: 10,
                ShippingDuration: 20
            );

            // Act
            var response = await useCase.Create(request);

            // Assert
            Assert.IsTrue(response.Success);
            Assert.AreEqual("Order created successfully", response.Response);
            orderRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Order>()), Times.Once);
        }

        [TestMethod]
        public async Task CreateOrder_ShouldReturnFailure_WhenInvalidRequest()
        {
            // Arrange
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var useCase = new OrderUseCase(orderRepositoryMock.Object);
            CreateOrderRequest request = null; // Invalid request

            // Act
            var response = await useCase.Create(request);

            // Assert
            Assert.IsFalse(response.Success);
            Assert.AreEqual("Invalid request", response.Response);
            orderRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Order>()), Times.Never);
        }

        [TestMethod]
        public async Task DeleteOrder_ShouldReturnSuccess_WhenOrderExists()
        {
            // Arrange
            var orderRepositoryMock = new Mock<IOrderRepository>();
            orderRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Order { Id = 1 });
            var useCase = new OrderUseCase(orderRepositoryMock.Object);
            var request = new DeleteOrderRequest ( Id: 1 );

            // Act
            var response = await useCase.Delete(request);

            // Assert
            Assert.IsTrue(response.Success);
            Assert.AreEqual("Order deleted successfully", response.Response);
            orderRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Order>()), Times.Once);
        }

        [TestMethod]
        public async Task DeleteOrder_ShouldReturnFailure_WhenOrderDoesNotExist()
        {
            // Arrange
            var orderRepositoryMock = new Mock<IOrderRepository>();
            orderRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Order)null);
            var useCase = new OrderUseCase(orderRepositoryMock.Object);
            var request = new DeleteOrderRequest ( Id: 1 );

            // Act
            var response = await useCase.Delete(request);

            // Assert
            Assert.IsFalse(response.Success);
            Assert.AreEqual("Order not found", response.Response);
            orderRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Order>()), Times.Never);
        }

        [TestMethod]
        public async Task EditOrder_ShouldReturnSuccess_WhenOrderExists()
        {
            // Arrange
            var orderRepositoryMock = new Mock<IOrderRepository>();
            orderRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Order { Id = 1 });
            var useCase = new OrderUseCase(orderRepositoryMock.Object);
            var request = new EditOrderRequest
            (
                Id: 1,
                CustomerId: 1,
                Customer: new Customer { Id = 1, Name = "Updated Customer" },
                WarehouseId: 1,
                Warehouse: new Warehouse { Id = 1, Name = "Updated Warehouse" },
                Delivery: null,
                Facture: null,
                OrderDate: DateTime.Now,
                OrderStatus: OrderStatus.Submitted,
                OrderDetails: new List<OrderDetail>(),
                ShippingCost: 15,
                ShippingDuration: 25
            );

            // Act
            var response = await useCase.Edit(request);

            // Assert
            Assert.IsTrue(response.Success);
            Assert.AreEqual("Order updated successfully", response.Response);
            orderRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Order>()), Times.Once);
        }

        [TestMethod]
        public async Task EditOrder_ShouldReturnFailure_WhenOrderDoesNotExist()
        {
            // Arrange
            var orderRepositoryMock = new Mock<IOrderRepository>();
            orderRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Order)null);
            var useCase = new OrderUseCase(orderRepositoryMock.Object);
            var request = new EditOrderRequest
            (
                Id: 1,
                CustomerId: 1,
                Customer: new Customer { Id = 1, Name = "Updated Customer" },
                WarehouseId: 1,
                Warehouse: new Warehouse { Id = 1, Name = "Updated Warehouse" },
                Delivery: null,
                Facture: null,
                OrderDate: DateTime.Now,
                OrderStatus: OrderStatus.Submitted,
                OrderDetails: new List<OrderDetail>(),
                ShippingCost: 15,
                ShippingDuration: 25
            );

            // Act
            var response = await useCase.Edit(request);

            // Assert
            Assert.IsFalse(response.Success);
            Assert.AreEqual("Order not found", response.Response);
            orderRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Order>()), Times.Never);
        }

        [TestMethod]
        public async Task ReadOrder_ShouldReturnOrder_WhenOrderExists()
        {
            // Arrange
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var order = new Order { Id = 1, Customer = new Customer { Id = 1, Name = "Test Customer" } };
            orderRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(order);
            var useCase = new OrderUseCase(orderRepositoryMock.Object);
            var request = new ReadOrderRequest ( Id: 1 );

            // Act
            var response = await useCase.Read(request);

            // Assert
            Assert.IsTrue(response.Success);
            Assert.AreEqual(order, response.Order);
            orderRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public async Task ReadOrder_ShouldReturnFailure_WhenOrderDoesNotExist()
        {
            // Arrange
            var orderRepositoryMock = new Mock<IOrderRepository>();
            orderRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Order)null);
            var useCase = new OrderUseCase(orderRepositoryMock.Object);
            var request = new ReadOrderRequest ( Id: 1 );

            // Act
            var response = await useCase.Read(request);

            // Assert
            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Order);
            orderRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<int>()), Times.Once);
        }
    }
}
