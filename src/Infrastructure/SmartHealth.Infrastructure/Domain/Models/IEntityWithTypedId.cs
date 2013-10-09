namespace VinaSale.Infrastructure.Domain.Models
{
    public interface IEntityWithTypedId<TId>
    {
        TId Id { get; }

        bool IsTransient();
    }
}
