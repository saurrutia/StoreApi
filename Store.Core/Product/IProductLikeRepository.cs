using System.Collections.Generic;
using Store.Core.Interfaces;

namespace Store.Core.Product
{
    public interface IProductLikeRepository : IRepository<ProductLikesEntity>
    {
        void RemoveRange(IEnumerable<ProductLikesEntity> rangeToRemove);
        void AddRange(IEnumerable<ProductLikesEntity> rangeToAdd);
    }
}
