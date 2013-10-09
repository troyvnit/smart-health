using VinaSale.Infrastructure.Domain.Models;

namespace VinaSale.Infrastructure.Domain.DataInterfaces
{
    public interface IRepository<T> : IRepositoryWithTypedId<T, int> where T : EntityWithTypedId<int>
    {
    }
}
