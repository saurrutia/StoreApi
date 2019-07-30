using System.Threading.Tasks;
using Store.Core.Events;
using Store.Core.Events.Common;
using Store.Core.Interfaces;
using Store.Core.Product;
using StoreApi.Utils;

namespace StoreApi.Handlers
{
    public class PriceUpdatedHandler : IDomainHandler<PriceUpdated>
    {
        private readonly IRepository<PriceUpdateLogEntity> _priceUpdateLogRepository;
        private readonly ITokenFactory _tokenFactory;

        public PriceUpdatedHandler(IRepository<PriceUpdateLogEntity> priceUpdateLogRepository, ITokenFactory tokenFactory)
        {
            _priceUpdateLogRepository = priceUpdateLogRepository;
            _tokenFactory = tokenFactory;
        }

        public async Task Handle(PriceUpdated @event)
        {
            _priceUpdateLogRepository.Create(new PriceUpdateLogEntity
            {
                ProductId = @event.Product.Id,
                ProductName = @event.Product.Name,
                PriceBefore = @event.LastPrice,
                PriceAfter = @event.Product.Price,
                UserName = _tokenFactory.GetUser()
            });
        }
    }
}
