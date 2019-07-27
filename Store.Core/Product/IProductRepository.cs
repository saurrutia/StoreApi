using System.Collections.Generic;
using System.Threading.Tasks;
using Store.Common.Dtos;
using Store.Core.Interfaces;

namespace Store.Core.Product
{
    public interface IProductRepository : IRepository<ProductEntity>
    {
        Task<IEnumerable<ProductEntity>> GetAllProducts();
        Task<IEnumerable<ProductEntity>> GetAllProductsChunk(PaginationDto pagination);
        Task<ProductEntity> GetById(int id);
        Task<ProductEntity> GetByName(string name);
        Task UpdateProduct(ProductEntity product);
        Task CreateProduct(ProductEntity product);
        Task DeleteProduct(ProductEntity product);
    }
}
