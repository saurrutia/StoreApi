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
            if (result == null)
                return NotFound($"Product with Name: {name} not found");
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

            product = new ProductEntity().Create(item.Name, item.Stock, item.Price);
            if(product == null)
                return Error($"Invalid data.");

            await _productRepository.CreateProduct(product);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("stock")]
        public async Task<IActionResult> Stock([FromQuery]int id, int stock)
        {
            var product = await _productRepository.GetById(id);

            if (product == null)
                return ProductNotFound(id);

            if (!product.ChangeStock(stock))
                return Error("Invalid data.");
            
            await _productRepository.UpdateProduct(product);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("price")]
        public async Task<IActionResult> Price([FromQuery]int id, double price)
        {
            var product = await _productRepository.GetById(id);

            if (product == null)
                return ProductNotFound(id);

            var result = product.ChangePrice(price);
            if (!result)
                return Error("Invalid price.");
            
            await _productRepository.UpdateProduct(product);
            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.GetById(id);
            if (product == null)
                return ProductNotFound(id);

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
                return ProductNotFound(id);

            var userNameClaim = _tokenFactory.GetUser();
            if (userNameClaim == null)
                return Unauthorized();

            var account = await _accountRepository.GetByUserName(userNameClaim);
            if(account == null)
                return Error("Account not found.");

            product.ToggleLike(account);
            await _productRepository.UpdateProduct(product);

            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Buyer")]
        [Route("{id}/buy")]
        public async Task<IActionResult> Buy(int id, [FromQuery]int quantity)
        {
            var product = await _productRepository.GetById(id);

            if (product == null)
                return ProductNotFound(id);

            var result = product.Buy(quantity);
            if (!result)
                return Error("The quantity exceeds product's stock.");

            await _productRepository.UpdateProduct(product);
            return Ok();
        }

        private IActionResult ProductNotFound(int id)
        {
            return NotFound($"Product with Id: {id} not found.");
        }
    }
}
