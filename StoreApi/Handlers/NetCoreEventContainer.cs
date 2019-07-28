using Microsoft.Extensions.DependencyInjection;
using Store.Core.Events;
using Store.Core.Events.Common;
using System;
using System.Collections.Generic;

namespace StoreApi.Handlers
{
    public class NetCoreEventContainer : IEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        public NetCoreEventContainer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Dispatch<TEvent>(TEvent eventToDispatch) where TEvent : IDomainEvent
        {
            foreach (var handler in _serviceProvider.GetServices<IDomainHandler<TEvent>>())
            {
                handler.Handle(eventToDispatch);
            }
        }
    }
}
