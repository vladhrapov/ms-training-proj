using MS.Domain.Abstract;
using MS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Domain.Concrete
{
    public class ProductRepository : IProductRepository
    {
        private EFDbContext context;

        public ProductRepository(EFDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<string> Categories
        {
            get
            {
                return context.Products
                    .Select(product => product.Category)
                    .Distinct()
                    .OrderBy(name => name);
            }
        }

        public int GetProductsCount() => context.Products.Count();

        public IEnumerable<Product> GetProducts(int page, int pageSize, string category)
        {
            return context.Products
                .Where(p => category == null || p.Category == category)
                .OrderBy(product => product.ProductID)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}
