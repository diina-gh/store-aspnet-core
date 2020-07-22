using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportsStore02.Models.Pages;

namespace SportsStore02.Models
{
    public class DataRepository: IRepository
    {
        //private List<Product> data = new List<Product>();
        private DataContext context;

        public DataRepository(DataContext ctx) => context = ctx;

        public IEnumerable<Product> Products => context.Products
            .Include(p => p.Category).ToArray();

        public PagedList<Product> GetProducts(QueryOptions options, long category = 0)
        {
            IQueryable<Product> query = context.Products.Include(p => p.Category);
            if(category != 0)
            {
                query = query.Where(p => p.CategoryId == category);
            }
            
            return new PagedList<Product>(query, options);
        }

        public Product GetProduct(long key) => context.Products
            .Include(p => p.Category).First(p => p.Id == key);

        public void AddProduct(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            //context.Products.Update(product);

            Product p = context.Products.Find(product.Id);
            p.Name = product.Name;
            p.PurchasePrice = product.PurchasePrice;
            p.RetailPrice = product.RetailPrice;
            p.CategoryId = product.CategoryId;
            context.SaveChanges();
        }

        public void UpdateAll(Product[] products)
        {
            //context.Products.UpdateRange(products);

            Dictionary<long, Product> data = products.ToDictionary(p => p.Id);
            IEnumerable<Product> baseline =
                context.Products.Where(p => data.Keys.Contains(p.Id));

            foreach(Product databaseProduct in baseline)
            {
                Product requestProduct = data[databaseProduct.Id];
                databaseProduct.Name = requestProduct.Name;
                databaseProduct.Category = requestProduct.Category;
                databaseProduct.PurchasePrice = requestProduct.PurchasePrice;
                databaseProduct.RetailPrice = requestProduct.RetailPrice;
            }

            context.SaveChanges();
        }

        public void Delete(Product product)
        {
            context.Products.Remove(product);
            context.SaveChanges();
        }
    }
}
