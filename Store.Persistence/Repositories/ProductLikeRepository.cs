using System;
using System.Collections.Generic;
using Store.Core.Events.Common;
using Store.Core.Product;

namespace Store.Persistence.Repositories
{
    public class ProductLikeRepository : Repository<ProductLikesEntity>, IProductLikeRepository
    {
        public ProductLikeRepository(StoreDbContext context, IEventDispatcher domainEventsDispatcher) : base(context, domainEventsDispatcher)
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
