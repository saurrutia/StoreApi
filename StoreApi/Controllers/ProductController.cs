using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Common.Dtos;
using Store.Core.Account;
using Store.Core.Product;
using StoreApi.Dtos;
using StoreApi.Utils;

namespace StoreApi.Controllers
{
    [Route("products")]
    public class ProductController : BaseController
    {
        private readonly IProductRepository _productRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ITokenFactory _tokenFactory;

        public ProductController(IProductRepository productRepository, IAccountRepository accountRepository, ITokenFactory tokenFactory)
        {
            _productRepository = productRepository;
            _accountRepository = accountRepository;
            _tokenFactory = tokenFactory;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromQuery]GetProductsDto item = null)
        {
            item = item ?? new GetProductsDto();
            var result = await _productRepository.GetAllProductsChunk(new PaginationDto
            {
                PageNumber = item.PageNumber,
                PageSize = item.PageSize,
                SortBy = item.SortBy.ToString(),
                Order = item.Order.ToString()
            });
            return Ok(result.Select(a => new GetProductsResponseDto
            {
                Id = a.Id,
                Name = a.Name,
                Stock = a.Stock,
                Likes = a.Likes,
                Price = a.Price
            }));
        }

        [HttpGet]
        [Route("product")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByName([FromQuery]string name)
        {
            var result = await _productRepository.GetByName(name);
            return Ok(new GetProductsResponseDto
            {
                Id = result.Id,
                Name = result.Name,
                Stock = result.Stock,
                Likes = result.Likes,
                Price = result.Price
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody]AddProductDto item)
        {
            var product = await _productRepository.GetByName(item.Name);

            if (product != null)
                return Error($"Name is already in use: {item.Name}");

            await _productRepository.CreateProduct(new ProductEntity()
            {
                Name = item.Name,
                Price = item.Price,
                Stock = item.Stock
            });
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put([FromQuery]int id, [FromBody]UpdateProductDto item)
        {
            var product = await _productRepository.GetById(id);

            if (product != null)
                return Error($"Name is already in use: {id}");

            product.Price = item.Price;
            product.Stock = item.Stock;
            await _productRepository.UpdateProduct(product);
            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.GetById(id);
            if (product == null)
                return Error($"Product not found for id: {id}");

            await _productRepository.DeleteProduct(product);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Buyer")]
        [Route("{id}/like/toggle")]
        public async Task<IActionResult> ToggleLike(int id)
        {
            var product = await _productRepository.GetById(id);
            if (product == null)
                return Error($"Product not found for id: {id}");

            var userNameClaim = HttpContext.User.Claims.SingleOrDefault(x => x.Type == _tokenFactory.UserIdClaim);
            if (userNameClaim == null)
                return Unauthorized();

            var account = await _accountRepository.GetByUserName(userNameClaim.Value);
            if(account == null)
                return Error("Account not found.");

            product.ToggleLike(account);
            await _productRepository.UpdateProduct(product);

            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Buyer")]
        public async Task<IActionResult> Buy([FromQuery]int id, [FromQuery]int quantity)
        {
            var product = await _productRepository.GetById(id);

            if (product != null)
                return Error($"Name is already in use: {id}");

            product.Stock -= quantity;
            await _productRepository.UpdateProduct(product);
            return Ok();
        }
    }
}
