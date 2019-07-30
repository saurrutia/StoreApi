using Microsoft.Extensions.DependencyInjection;
using Store.Core.Events;
using Store.Core.Events.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreApi.Handlers
{
    public class NetCoreEventContainer : IEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        public NetCoreEventContainer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Dispatch<TEvent>(TEvent eventToDispatch) where TEvent : IDomainEvent
        {
            if (eventToDispatch.GetType() == typeof(PriceUpdated))
            {
                var domainHandler = _serviceProvider.GetServices<IDomainHandler<PriceUpdated>>();
                foreach (var handler in domainHandler)
                {
                    await handler.Handle(eventToDispatch as PriceUpdated);
                }
            }
            if (eventToDispatch.GetType() == typeof(ProductBuyed))
            {
                var domainHandler = _serviceProvider.GetServices<IDomainHandler<ProductBuyed>>();
                foreach (var handler in domainHandler)
                {
                    await handler.Handle(eventToDispatch as ProductBuyed);
                }
            }
            
        }
    }
}
