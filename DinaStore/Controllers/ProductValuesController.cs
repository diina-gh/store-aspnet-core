using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore02.Models;

namespace SportsStore02.Controllers
{
    [Route("api/products")]
    public class ProductValuesController: Controller
    {
        private IWebServiceRepository repository;

        public ProductValuesController(IWebServiceRepository repo)
            => repository = repo;

        [HttpGet("{id}")]
        public object GetProduct(long id)
        {
            return repository.GetProduct(id) ?? NotFound();
        }
    }
}
