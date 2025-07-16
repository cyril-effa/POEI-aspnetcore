using ex10bis.Core.Entities;
using ex10bis.Core.Customer.Dtos;
using ex10bis.Core.Customer.Interfaces;
using ex10bis.Core.Customer.UseCases;
using Moq;

namespace UnitTests
{
    [TestClass]
    public class CustomerUseCaseTests
    {
        [TestMethod]
        public void Create_ShouldReturnSuccess_WhenCustomerIsCreated()
        {
            // Arrange
            var fakeRepository = new Mock<ICustomerRepository>();
            var request = new CreateCustomerRequest("John Doe", "John", "john@mail.com", "10 rue de john", "Paris", new List<Order>());
            fakeRepository.Setup(r => r.AddAsync(It.IsAny<Customer>())).Returns(Task.CompletedTask);
            var useCase = new CustomerUseCase(fakeRepository.Object);

            // Act
            var result = useCase.Execute(request).Result;

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual("Customer created successfully", result.Response);
            Assert.IsNotNull(result.Customer);
            Assert.AreEqual("John", result.Customer.Name);
        }

        [TestMethod]
        public void Create_ShouldReturnError_WhenRequestIsNull()
        {
            // Arrange
            var fakeRepository = new Mock<ICustomerRepository>();
            var useCase = new CustomerUseCase(fakeRepository.Object);

            // Act
            var result = useCase.Execute(null).Result;

            // Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual("Invalid request", result.Response);
            Assert.IsNull(result.Customer);
        }
    }
}
