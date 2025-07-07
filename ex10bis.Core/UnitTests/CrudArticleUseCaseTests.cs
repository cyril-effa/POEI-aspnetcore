using ex10bis.Core.Article.Dtos;
using ex10bis.Core.Article.Interfaces;
using ex10bis.Core.Article.UseCases;
using ex10bis.Core.Entities;
using Moq;

namespace UnitTests
{
    [TestClass]
    public class CrudArticleUseCaseTests
    {
        [TestMethod]
        public void Create_ShouldReturnSuccess_WhenArticleIsCreated()
        {
            // Arrange
            var fakeRepository = new Mock<IArticleRepository>();
            var request = new CreateArticleRequest("Article A", "Description A", 100.0m, 10);

            // Act
            var result = new CrudArticleUseCase(fakeRepository.Object).Create(request).Result;

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual("Article A", result.Article.Name);
            Assert.AreEqual("Description A", result.Article.Description);
            Assert.AreEqual(100.0m, result.Article.Price);
            Assert.AreEqual(10, result.Article.StockQuantity);
            fakeRepository.Verify(r => r.AddAsync(It.IsAny<Article>()), Times.Once);
        }

        [TestMethod]
        public void Create_ShouldReturnError_WhenArticleNameIsEmpty()
        {
            // Arrange
            var fakeRepository = new Mock<IArticleRepository>();
            var request = new CreateArticleRequest("", "Description A", 100.0m, 10);

            // Act
            var result = new CrudArticleUseCase(fakeRepository.Object).Create(request).Result;

            // Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual("Invalid input data", result.Response);
            Assert.IsNull(result.Article);
            fakeRepository.Verify(r => r.AddAsync(It.IsAny<Article>()), Times.Never);
        }

        [TestMethod]
        public void Read_ShouldReturnArticle_WhenIdExists()
        {
            // Arrange
            var fakeRepository = new Mock<IArticleRepository>();
            var article = new Article { Id = 1, Name = "Article A", Description = "Description A", Price = 100.0m, StockQuantity = 10 };
            fakeRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(article);
            var request = new ReadArticleRequest(1);

            // Act
            var result = new CrudArticleUseCase(fakeRepository.Object).Read(request).Result;

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(article, result.Article);
        }

        [TestMethod]
        public void Read_ShouldReturnError_WhenIdDoesNotExist()
        {
            // Arrange
            var fakeRepository = new Mock<IArticleRepository>();
            fakeRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Article)null);
            var request = new ReadArticleRequest(1);

            // Act
            var result = new CrudArticleUseCase(fakeRepository.Object).Read(request).Result;

            // Assert
            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Article);
        }

        [TestMethod]
        public void Edit_ShouldReturnSuccess_WhenArticleIsUpdated()
        {
            // Arrange
            var fakeRepository = new Mock<IArticleRepository>();
            var request = new EditArticleRequest(1, "Updated Article", "Updated Description", 150.0m, 10);
            var article = new Article { Id = 1, Name = "Article A", Description = "Description A", Price = 100.0m, StockQuantity = 200 };
            fakeRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(article);

            // Act
            var result = new CrudArticleUseCase(fakeRepository.Object).Edit(request).Result;

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual("Updated Article", result.Article.Name);
            Assert.AreEqual("Updated Description", result.Article.Description);
            Assert.AreEqual(150.0m, result.Article.Price);
            fakeRepository.Verify(r => r.UpdateAsync(It.IsAny<Article>()), Times.Once);
        }

        [TestMethod]
        public void Edit_ShouldReturnError_WhenArticleDoesNotExist()
        {
            // Arrange
            var fakeRepository = new Mock<IArticleRepository>();
            fakeRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Article)null);
            var request = new EditArticleRequest(1, "Updated Article", "Updated Description", 150.0m, 10);

            // Act
            var result = new CrudArticleUseCase(fakeRepository.Object).Edit(request).Result;

            // Assert
            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Article);
        }

        [TestMethod]
        public void Delete_ShouldReturnSuccess_WhenArticleIsDeleted()
        {
            // Arrange
            var fakeRepository = new Mock<IArticleRepository>();
            var article = new Article { Id = 1, Name = "Article A", Description = "Description A", Price = 100.0m, StockQuantity = 10 };
            fakeRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(article);

            // Act
            var result = new CrudArticleUseCase(fakeRepository.Object).Delete(new DeleteArticleRequest(1)).Result;

            // Assert
            Assert.IsTrue(result.Success);
            fakeRepository.Verify(r => r.DeleteAsync(article), Times.Once);
        }

        [TestMethod]
        public void Delete_ShouldReturnError_WhenArticleDoesNotExist()
        {
            // Arrange
            var fakeRepository = new Mock<IArticleRepository>();
            fakeRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Article)null);

            // Act
            var result = new CrudArticleUseCase(fakeRepository.Object).Delete(new DeleteArticleRequest(1)).Result;

            // Assert
            Assert.IsFalse(result.Success);
            fakeRepository.Verify(r => r.DeleteAsync(It.IsAny<Article>()), Times.Never);
        }
    }
}
