using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MS.Domain.Concrete;
using MS.Domain.Entities;
using System.Collections.Generic;
using MS.Web.Controllers;
using System.Linq;
using MS.Domain.Abstract;
using System.Web.Mvc;
using MS.Web.Models;
using MS.Web.HtmlHelpers;

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
            productRepositoryMock.Setup(m => m.GetProducts(It.IsAny<int>(), It.IsAny<int>(), null))
                .Returns(() => products);
            productRepositoryMock.Setup(m => m.GetProductsCount())
                .Returns(() => products.Count());

            // Act
            var productCtrl = new ProductController(productRepositoryMock.Object);
            var result = productCtrl.List(null, It.IsAny<int>()).Model as ProductsListViewModel;

            // Assert
            Assert.AreEqual(result.Products, products);
            Assert.IsTrue(result.Products.Count() <= 4);
        }

        [TestMethod]
        public void PageLinks_ShouldGenerateLinksForNavigation()
        {
            // Arrange
            HtmlHelper helper = null;
            var paging = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };
            Func<int, string> pageUrl = i => $"Page{i}";
            var expectedResult = $@"<a class=""btn btn-default"" href=""Page1"">1</a><a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a><a class=""btn btn-default"" href=""Page3"">3</a>";

            // Act
            MvcHtmlString result = helper.PageLinks(paging, pageUrl);

            // Assert
            Assert.AreEqual(expectedResult, result.ToString());
        }

        [TestMethod]
        public void ProductList_ShouldReturnProductListViewModel()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.GetProducts(It.IsAny<int>(), It.IsAny<int>(), null))
                .Returns(products);
            mock.Setup(m => m.GetProductsCount())
                .Returns(() => products.Count());

            var controller = new ProductController(mock.Object);

            // Act
            var result = controller.List(null, 1).Model as ProductsListViewModel;

            // Assert
            Assert.AreEqual(result.PagingInfo.CurrentPage, 1);
            Assert.AreEqual(result.PagingInfo.ItemsPerPage, 4);
            Assert.AreEqual(result.PagingInfo.TotalItems, 4);
            Assert.AreEqual(result.PagingInfo.TotalPages, 1);
        }
    }
}
