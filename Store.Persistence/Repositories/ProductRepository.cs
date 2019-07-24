using System;
using System.Collections.Generic;
using System.Text;
using Store.Core.Product;

namespace Store.Persistence.Repositories
{
    public class ProductRepository : Repository<ProductEntity>, IProductRepository
    {
        public ProductRepository(StoreDbContext context) : base(context)
        {
        }
       
    }
}
