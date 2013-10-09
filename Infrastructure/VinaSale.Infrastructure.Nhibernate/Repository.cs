using VinaSale.Infrastructure.Domain.DataInterfaces;
using VinaSale.Infrastructure.Domain.Models;

namespace VinaSale.Infrastructure.Nhibernate
{
    public class Repository<T> : RepositoryWithTypedId<T, int>, IRepository<T> where T : EntityWithTypedId<int>
    {
    }
}
