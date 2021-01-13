using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bingol.Models
{
    public interface IProductRepository
    {
        ProductModel GetProduct(int Id);
        IEnumerable<ProductModel> GetProducts();
        ProductModel Add(ProductModel product);
        ProductModel Update(ProductModel product);
        ProductModel Delete(int Id);
    }
}
