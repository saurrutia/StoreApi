using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Common.Dtos;
using Store.Core.Account;
using Store.Core.Product;
using StoreApi.Dtos;

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

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromQuery]GetProductsDto item = null)
        {
            item = item ?? new GetProductsDto();
            return Ok(await _productRepository.GetAllProductsChunk(new PaginationDto
            {
                PageNumber = item.PageNumber,
                PageSize = item.PageSize,
                SortBy = item.SortBy.ToString()
            }));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody]AddProductDto item)
        {
            var product = await _productRepository.GetByName(item.Name);

            if (product != null)
                return Error($"Name is already in use: {item.Name}");

            _productRepository.Create(new ProductEntity()
            {
                Name = item.Name,
                Price = item.Price,
                Stock = item.Stock
            });
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.GetById(id);
            if (product == null)
                return Error($"Product not found for id: {id}");

            _productRepository.Delete(product);
            return Ok();
        }

        [HttpPut]
        [Route("{id}/like/toggle")]
        public async Task<IActionResult> ToggleLike(int id)
        {
            var product = await _productRepository.GetById(id);
            if (product == null)
                return Error($"Product not found for id: {id}");
            var userName = string.Empty;
            product.ToggleLike(new AccountEntity
            {
                UserName = "username1"
            });
            await _productRepository.UpdateProduct(product);
            return Ok();
        }
    }
}
