using System;

using NHibernate;

using SmartHealth.Infrastructure.Domain.DataInterfaces;

namespace SmartHealth.Infrastructure.Nhibernate
{
    public class DbContext : IDbContext
    {
        public DbContext(string factoryKey)
        {
            if (String.IsNullOrEmpty(factoryKey))
                throw new Exception("factoryKey may not be null or empty");

            FactoryKey = factoryKey;
        }

        private ISession Session
        {
            get
            {
                return NHibernateSession.CurrentFor(FactoryKey);
            }
        }

        public void Evict(object obj)
        {
            Session.Evict(obj);
        }

        public IDisposable BeginTransaction()
        {
            return Session.BeginTransaction();
        }

        public void CommitChanges()
        {
            Session.Flush();
        }

        public void CommitTransaction()
        {
            Session.Transaction.Commit();
        }

        public void RollbackTransaction()
        {
            Session.Transaction.Rollback();
        }

        public string FactoryKey { get; set; }
    }
}
