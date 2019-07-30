using System.Threading.Tasks;

namespace Store.Core.Events.Common
{
    public interface IEventDispatcher
    {
        Task Dispatch<TEvent>(TEvent eventToDispatch) where TEvent : IDomainEvent;
    }
}
