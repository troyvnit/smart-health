using SmartHealth.Infrastructure.Domain.DataInterfaces;
using SmartHealth.Infrastructure.Domain.Models;

namespace SmartHealth.Infrastructure.Nhibernate
{
    public class Repository<T> : RepositoryWithTypedId<T, int>, IRepository<T> where T : EntityWithTypedId<int>
    {
    }
}
