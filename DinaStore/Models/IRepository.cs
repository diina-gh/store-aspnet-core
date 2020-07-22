using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportsStore02.Models.Pages;

namespace SportsStore02.Models
{
    public interface IRepository
    {
        IEnumerable<Product> Products { get; }

        PagedList<Product> GetProducts(QueryOptions options, long category = 0);

        Product GetProduct(long key);

        void AddProduct(Product product);

        void UpdateProduct(Product product);

        void UpdateAll(Product[] products);

        void Delete(Product product);
    }
}
