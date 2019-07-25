using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Common.Dtos;
using Store.Core.Product;

namespace Store.Persistence.Repositories
{
    public class ProductRepository : Repository<ProductEntity>, IProductRepository
    {
        public ProductRepository(StoreDbContext context) : base(context)
        {
        }

        public async Task AddLike(int id)
        {
            
        }

        public async Task<IEnumerable<ProductEntity>> GetAllProducts()
        {
            return await FindAll().OrderBy(a => a.Name).ToListAsync();
        }

        public async Task<IEnumerable<ProductEntity>> GetAllProductsChunk(PaginationDto pagination)
        {

            var property = TypeDescriptor.GetProperties(typeof(ProductEntity)).Find(pagination.SortBy, true);
            return await FindAll().OrderBy(a => property.GetValue(a))
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync();
        }

        public async Task<ProductEntity> GetById(int id)
        {
            return await FindByCondition(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ProductEntity> GetByName(string name)
        {
            return await FindByCondition(p => p.Name == name).FirstOrDefaultAsync();
        }

        public async Task UpdateProduct(ProductEntity product)
        {
            Update(product);
            await SaveAsync();
        }

        public async Task RemoveLike(int id)
        {
           
        }
    }
}
