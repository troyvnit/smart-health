namespace VinaSale.Infrastructure.Domain.Events
{
    public interface IHandles<in T> where T : IDomainEvent
    {
        void Handle(T args);
    }
}
