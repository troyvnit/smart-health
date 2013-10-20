using System;

namespace SmartHealth.Infrastructure.Domain.DataInterfaces
{
    public interface IDbContext
    {
        IDisposable BeginTransaction();

        void Evict(object obj);

        void CommitChanges();

        void CommitTransaction();

        void RollbackTransaction();
    }
}
