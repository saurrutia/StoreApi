using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Store.Core.Product;

namespace StoreApi.Controllers
{
    [Route("products")]
    public class ProductController : BaseController
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_productRepository.List());
        }
    }
}
