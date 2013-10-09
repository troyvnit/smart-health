using System.Linq;

using NHibernate;
using NHibernate.Linq;

using VinaSale.Infrastructure.Domain.DataInterfaces;
using VinaSale.Infrastructure.Domain.Models;

namespace VinaSale.Infrastructure.Nhibernate
{
    public class RepositoryWithTypedId<T, TId> : IRepositoryWithTypedId<T, TId> where T : IEntityWithTypedId<TId>
    {
        private IDbContext dbContext;

        protected virtual ISession Session
        {
            get
            {
                string factoryKey = SessionFactoryAttribute.GetKeyFrom(this);
                return NHibernateSession.CurrentFor(factoryKey);
            }
        }

        public IDbContext DbContext
        {
            get
            {
                if (dbContext == null)
                {
                    string factoryKey = SessionFactoryAttribute.GetKeyFrom(this);
                    dbContext = new DbContext(factoryKey);
                }

                return dbContext;
            }
        }

        public virtual T Get(TId id)
        {
            return Session.Get<T>(id);
        }

        public virtual T Load(TId id)
        {
            return Session.Load<T>(id);
        }

        public virtual IQueryable<T> Query()
        {
            return Session.Query<T>();
        }

        public virtual void SaveOrUpdate(T entity)
        {
            Session.SaveOrUpdate(entity);
        }

        public virtual void Delete(T entity)
        {
            Session.Delete(entity);
        }
    }
}
