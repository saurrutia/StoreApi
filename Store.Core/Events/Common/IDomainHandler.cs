namespace Store.Core.Events.Common
{
    public interface IDomainHandler<T> where T : IDomainEvent
    {
        void Handle(T @event);
    }
}
