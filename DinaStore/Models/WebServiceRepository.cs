using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore02.Models
{
    public class WebServiceRepository: IWebServiceRepository
    {
        private DataContext context;

        public WebServiceRepository(DataContext ctx) => context = ctx;

        public object GetProduct(long id)
        {
            return context.Products.FirstOrDefault(p => p.Id == id);
        }

    }
}
