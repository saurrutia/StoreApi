using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Store.Core.Product;

namespace Store.Persistence.Repositories
{
    public class ProductLikeRepository : Repository<ProductLikesEntity>, IProductLikeRepository
    {
        public ProductLikeRepository(StoreDbContext context) : base(context)
        {
        }

        public void RemoveRange(IEnumerable<ProductLikesEntity> rangeToRemove)
        {
            _dbSet.RemoveRange(rangeToRemove);
        }

        public void AddRange(IEnumerable<ProductLikesEntity> rangeToAdd)
        {
            _dbSet.AddRange(rangeToAdd);
        }
    }
}
