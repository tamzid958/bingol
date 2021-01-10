using Bingol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bingol.Data
{
    public class SQLProductRepository : IProductRepository
    {
        private readonly BingolContext context;
        public SQLProductRepository(BingolContext context)
        {
            this.context = context;
        }
        public ProductModel Add(ProductModel product)
        {
            context.Products.Add(product);
            context.SaveChanges();
            return product;
        }
        public ProductModel Delete(int Id)
        {
            ProductModel product = context.Products.Find(Id);
            if(product != null)
            {
                context.Products.Remove(product);
                context.SaveChanges();
            }
            return product;
        }
        public ProductModel GetProduct(int Id)
        {
            return context.Products.Find(Id);
        }
        public IEnumerable<ProductModel> GetProducts()
        {
            return context.Products;
        }
        public ProductModel Update(ProductModel productChanges)
        {
            var product = context.Products.Attach(productChanges);
            product.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return productChanges;
        }
    }
}
