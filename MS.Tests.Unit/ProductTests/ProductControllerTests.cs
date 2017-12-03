using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MS.Domain.Concrete;
using MS.Domain.Entities;
using System.Collections.Generic;
using MS.Web.Controllers;
using System.Linq;
using MS.Domain.Abstract;

namespace MS.Tests.Unit.ProductTests
{
    [TestClass]
    public class ProductControllerTests
    {
        private List<Product> products = new List<Product> {
            new Product {
                ProductID = 1,
                Name = "Product 1",
                Category = "Category 1",
                Description = "some product description",
                Price = 56M
            },
            new Product {
                ProductID = 2,
                Name = "Product 2",
                Category = "Best Category 1",
                Description = "Other product 2 description",
                Price = 89.45M
            },
            new Product {
                ProductID = 3,
                Name = "Product 3",
                Category = "Category 45",
                Description = "some product description",
                Price = 23.9M
            },
            new Product {
                ProductID = 4,
                Name = "Product 4",
                Category = "Category 0",
                Description = "Other product description",
                Price = 1673.25M
            }
        };

        [TestMethod]
        public void ProductList_ShouldReturnProductListForPage()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(m => m.GetProducts(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(() => products);

            // Act
            var productCtrl = new ProductController(productRepositoryMock.Object);
            var result = productCtrl.List(It.IsAny<int>()).Model as IEnumerable<Product>;

            // Assert
            Assert.AreEqual(result, products);
            Assert.IsTrue(result.Count() <= 4);
        }
    }
}
