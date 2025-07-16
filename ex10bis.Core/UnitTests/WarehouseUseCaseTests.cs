using ex10bis.Core.Entities;
using ex10bis.Core.Warehouse.Dtos;
using ex10bis.Core.Warehouse.Interfaces;
using ex10bis.Core.Warehouse.UseCases;
using Moq;

namespace UnitTests
{
    [TestClass]
    public sealed class WarehouseUseCaseTests
    {
        [TestMethod]
        public void Create_ShouldReturnSuccess_WhenWarehouseIsCreated()
        {
            // Arrange
            var fakeRepository = new Mock<IWarehouseRepository>();
            var request = new CreateWarehouseRequest("Entrepôt A", "Adresse A", 10000, new List<Order>());

            // Act
            var result = new WarehouseUseCase(fakeRepository.Object).Create(request).Result;

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual("Entrepôt A", result.Warehouse.Name);
            Assert.AreEqual("Adresse A", result.Warehouse.Address);
            Assert.AreEqual(10000, result.Warehouse.PostalCode);
            fakeRepository.Verify(r => r.AddAsync(It.IsAny<Warehouse>()), Times.Once);
        }

        [TestMethod]
        public void Create_ShouldReturnError_WhenWarehouseNameIsEmpty()
        {
            // Arrange
            var fakeRepository = new Mock<IWarehouseRepository>();
            var request = new CreateWarehouseRequest("", "Adresse A", 10000, new List<Order>());

            // Act
            var result = new WarehouseUseCase(fakeRepository.Object).Create(request).Result;

            // Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual("Invalid input data", result.Response);
            Assert.IsNull(result.Warehouse);
            fakeRepository.Verify(r => r.AddAsync(It.IsAny<Warehouse>()), Times.Never);
        }

        [TestMethod]
        public void Read_ShouldReturnWarehouse_WhenIdExists()
        {
            // Arrange
            var fakeRepository = new Mock<IWarehouseRepository>();
            var warehouse = new Warehouse { Id = 1, Name = "Entrepôt A", Address = "Adresse A" };
            fakeRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(warehouse);

            var request = new ReadWarehouseRequest(1);

            // Act
            var result = new WarehouseUseCase(fakeRepository.Object).Read(request).Result;

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(1, result.Warehouse.Id);
            Assert.AreEqual("Entrepôt A", result.Warehouse.Name);
            Assert.AreEqual("Adresse A", result.Warehouse.Address);
            fakeRepository.Verify(r => r.GetByIdAsync(1), Times.Once);
        }

        [TestMethod]
        public void Read_ShouldReturnError_WhenIdDoesNotExist()
        {
            // Arrange
            var fakeRepository = new Mock<IWarehouseRepository>();
            fakeRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Warehouse)null);
            var request = new ReadWarehouseRequest(1);

            // Act
            var result = new WarehouseUseCase(fakeRepository.Object).Read(request).Result;

            // Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual("Warehouse not found", result.Response);
            Assert.IsNull(result.Warehouse);
            fakeRepository.Verify(r => r.GetByIdAsync(1), Times.Once);
        }

        [TestMethod]
        public void Edit_ShouldReturnSuccess_WhenWarehouseIsUpdated()
        {
            // Arrange
            var fakeRepository = new Mock<IWarehouseRepository>();
            var warehouse = new Warehouse { Id = 1, Name = "Entrepôt A", Address = "Adresse A" };
            fakeRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(warehouse);
            fakeRepository.Setup(r => r.UpdateAsync(It.IsAny<Warehouse>())).Returns(Task.CompletedTask);
            var request = new EditWarehouseRequest(1, "Entrepôt B", "Adresse B", 20000, new List<Order>());

            // Act
            var result = new WarehouseUseCase(fakeRepository.Object).Edit(request).Result;

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual("Entrepôt B", result.Warehouse.Name);
            Assert.AreEqual("Adresse B", result.Warehouse.Address);
            Assert.AreEqual(20000, result.Warehouse.PostalCode);
            fakeRepository.Verify(r => r.UpdateAsync(It.IsAny<Warehouse>()), Times.Once);
        }

        [TestMethod]
        public void Edit_ShouldReturnError_WhenWarehouseDoesNotExist()
        {
            // Arrange
            var fakeRepository = new Mock<IWarehouseRepository>();
            fakeRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Warehouse)null);
            var request = new EditWarehouseRequest(1, "Entrepôt B", "Adresse B", 20000, new List<Order>());

            // Act
            var result = new WarehouseUseCase(fakeRepository.Object).Edit(request).Result;

            // Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual("Warehouse not found", result.Response);
            Assert.IsNull(result.Warehouse);
            fakeRepository.Verify(r => r.UpdateAsync(It.IsAny<Warehouse>()), Times.Never);
        }

        [TestMethod]
        public void Delete_ShouldReturnSuccess_WhenWarehouseIsDeleted()
        {
            // Arrange
            var fakeRepository = new Mock<IWarehouseRepository>();
            var warehouse = new Warehouse { Id = 1, Name = "Entrepôt A", Address = "Adresse A" };
            fakeRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(warehouse);
            fakeRepository.Setup(r => r.DeleteAsync(warehouse)).Returns(Task.CompletedTask);

            var request = new DeleteWarehouseRequest(1);

            // Act
            var result = new WarehouseUseCase(fakeRepository.Object).Delete(request).Result;

            // Assert
            Assert.IsTrue(result.Success);
            fakeRepository.Verify(r => r.DeleteAsync(warehouse), Times.Once);
        }

        [TestMethod]
        public void Delete_ShouldReturnError_WhenWarehouseDoesNotExist()
        {
            // Arrange
            var fakeRepository = new Mock<IWarehouseRepository>();
            fakeRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Warehouse)null);
            var request = new DeleteWarehouseRequest(1);

            // Act
            var result = new WarehouseUseCase(fakeRepository.Object).Delete(request).Result;

            // Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual("Warehouse not found", result.Response);
            fakeRepository.Verify(r => r.DeleteAsync(It.IsAny<Warehouse>()), Times.Never);
        }
    }
}
