using System.Linq;

using VinaSale.Infrastructure.Domain.Models;

namespace VinaSale.Infrastructure.Domain.DataInterfaces
{
    public interface IRepositoryWithTypedId<T, TId> where T : IEntityWithTypedId<TId>
    {
        /// <summary>
        /// Provides a handle to application wide DB activities such as committing any pending changes,
        /// beginning a transaction, rolling back a transaction, etc.
        /// </summary>
        IDbContext DbContext { get; }

        /// <summary>
        /// Returns null if a row is not found matching the provided Id.
        /// </summary>
        T Get(TId id);

        T Load(TId id);

        IQueryable<T> Query(); 

        /// <summary>
        /// For entities with automatically generated Ids, such as identity or Hi/Lo, SaveOrUpdate may 
        /// be called when saving or updating an entity.  If you require separate Save and Update
        /// methods, you'll need to extend the base repository interface when using NHibernate.
        /// 
        /// Updating also allows you to commit changes to a detached object.  More info may be found at:
        /// http://www.hibernate.org/hib_docs/nhibernate/html_single/#manipulatingdata-updating-detached
        /// </summary>
        void SaveOrUpdate(T entity);

        /// <summary>
        /// I'll let you guess what this does.
        /// </summary>
        /// <remarks>The SharpLite.NHibernateProvider.Repository commits the deletion immediately; 
        /// see that class for details.</remarks>
        void Delete(T entity);
    }
}
