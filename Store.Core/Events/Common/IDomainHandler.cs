using System.Threading.Tasks;

namespace Store.Core.Events.Common
{
    public interface IDomainHandler<T> where T : IDomainEvent
    {
        Task Handle(T @event);
    }
}
