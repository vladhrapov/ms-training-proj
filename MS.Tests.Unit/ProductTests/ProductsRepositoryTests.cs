using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MS.Domain.Abstract;
using MS.Domain.Entities;
using MS.Domain.Concrete;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace MS.Tests.Unit.ProductTests
{
    [TestClass]
    public class ProductsRepositoryTests
    {
        private IEnumerable<Product> products = new Product[] {
             new Product {ProductID = 1, Name = "P1", Category = "Cat1"},
             new Product {ProductID = 2, Name = "P2", Category = "Cat2"},
             new Product {ProductID = 3, Name = "P3", Category = "Cat1"},
             new Product {ProductID = 4, Name = "P4", Category = "Cat2"},
             new Product {ProductID = 5, Name = "P5", Category = "Cat3"}
        };

        private static Mock<DbSet<T>> GetDbSetMock<T>(IEnumerable<T> items = null) where T : class
        {
            if (items == null)
            {
                items = new T[0];
            }

            var dbSetMock = new Mock<DbSet<T>>();
            var q = dbSetMock.As<IQueryable<T>>();

            q.Setup(x => x.GetEnumerator()).Returns(items.GetEnumerator);

            return dbSetMock;
        }

        private static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));

            return dbSet.Object;
        }

        [TestMethod]
        public void ProductRepository_ShouldFilterProductsByCategory()
        {
            ////DbSet<Product> dbset = 
            //var contextMock = new Mock<EFDbContext>();
            ////var dbSetMock = new Mock<DbSet<Product>>();
            ////var q = dbSetMock.As<IQueryable<Product>>();

            ////q.Setup(x => x.GetEnumerator()).Returns(GetDbSetMock(products).Object);

            ////dbSetMock.Object
            ////    .AddRange()
            ////    .AsQueryable();
            ////DbSet.Create<Product[]>()
            ////dbsetMock.Setup(m => m.)
            //contextMock.Setup(m => m.Set<Product>())
            //    .Returns(GetDbSetMock(products).Object);

            //var contextMock = new Mock<EFDbContext>();
            //var dbSetMock = new Mock<DbSet<Product>>();
            //dbSetMock.Setup(d => d.Add(It.IsAny<Product>())).Callback<Product[]>((s) => products);

            var contextMock = new Mock<EFDbContext>();

            contextMock.Setup(x => x.Set<Product>()).Returns(GetDbSetMock(products).Object);

            var productRepository = new ProductRepository(contextMock.Object);
            var category = "Soccer";

            // Act
            var result = productRepository.GetProducts(It.IsAny<int>(), It.IsAny<int>(), category);

            // Assert
            Assert.IsTrue(result.Count() == 33);

        }
    }
}
