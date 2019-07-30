using System.Threading.Tasks;

namespace Store.Core.Events.Common
{
    public static class DomainEventsDispatcher
    {
        public static IEventDispatcher Dispatcher { get; set; }

        public static async Task Raise<T>(T @event) where T : IDomainEvent
        {
            await Dispatcher.Dispatch(@event);
        }

    }
}

