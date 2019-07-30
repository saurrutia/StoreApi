using System.Threading.Tasks;
using Store.Core.Events;
using Store.Core.Events.Common;
using Store.Core.Interfaces;
using Store.Core.Product;
using StoreApi.Utils;

namespace StoreApi.Handlers
{
    public class ProductBuyedHandler : IDomainHandler<ProductBuyed>
    {
        private readonly IRepository<PurchaseLogEntity> _purchaseLogRepository;
        private readonly ITokenFactory _tokenFactory;

        public ProductBuyedHandler(IRepository<PurchaseLogEntity> purchaseLogRepository, ITokenFactory tokenFactory)
        {
            _purchaseLogRepository = purchaseLogRepository;
            _tokenFactory = tokenFactory;            
        }

        public async Task Handle(ProductBuyed @event)
        {
            _purchaseLogRepository.Create(new PurchaseLogEntity
            {
                ProductId = @event.Product.Id,
                ProductName = @event.Product.Name,
                Quantity = @event.Quantity,
                UserName = _tokenFactory.GetUser()
            });
            await _purchaseLogRepository.SaveAsync();
        }
    }
}
