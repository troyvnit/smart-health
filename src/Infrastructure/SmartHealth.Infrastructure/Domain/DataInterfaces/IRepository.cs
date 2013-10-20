using SmartHealth.Infrastructure.Domain.Models;

namespace SmartHealth.Infrastructure.Domain.DataInterfaces
{
    public interface IRepository<T> : IRepositoryWithTypedId<T, int> where T : EntityWithTypedId<int>
    {
    }
}
