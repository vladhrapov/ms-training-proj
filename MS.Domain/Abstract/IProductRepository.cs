using MS.Domain.Entities;
using System.Collections.Generic;

namespace MS.Domain.Abstract
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts(int page, int pageSize);
    }
}
