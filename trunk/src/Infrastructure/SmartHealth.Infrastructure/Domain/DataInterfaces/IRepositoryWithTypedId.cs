using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SmartHealth.Infrastructure.Domain.Models;

namespace SmartHealth.Infrastructure.Domain.DataInterfaces
{
    public interface IRepositoryWithTypedId<T, TId> where T : IEntityWithTypedId<TId>
    {
        T Get(TId id);
        U Get<U>(TId id) where U : EntityWithTypedId<TId>;
        T Load(TId id);
        U Load<U>(TId id) where U : EntityWithTypedId<TId>;
        IQueryable<T> GetQuery();
        IQueryable<U> GetQuery<U>() where U : EntityWithTypedId<TId>;
        IList<T> GetAll();
        IList<U> GetAll<U>() where U : EntityWithTypedId<TId>;
        IList<T> GetAllOrderBy(Expression<Func<T, object>> orderBy);
        IList<U> GetAllOrderBy<U>(Expression<Func<U, object>> orderBy) where U : EntityWithTypedId<TId>;
        IList<T> GetAllOrderByDescending(Expression<Func<T, object>> orderBy);
        IList<U> GetAllOrderByDescending<U>(Expression<Func<U, object>> orderBy) where U : EntityWithTypedId<TId>;
        IList<T> FindAll(Expression<Func<T, bool>> criteria);
        IList<T> FindAllOrderBy(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> orderBy);
        IList<T> FindAllOrderByDescending(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> orderBy);
        IList<U> FindAll<U>(Expression<Func<U, bool>> criteria);
        IList<U> FindAllOrderBy<U>(Expression<Func<U, bool>> criteria, Expression<Func<U, object>> orderBy);
        IList<U> FindAllOrderByDescending<U>(Expression<Func<U, bool>> criteria, Expression<Func<U, object>> orderBy);
        T Save(T entity);
        U Save<U>(U entity) where U : EntityWithTypedId<TId>;
        T Update(T entity);
        U Update<U>(U entity) where U : EntityWithTypedId<TId>;
        void Delete(T entity);
        void Delete<U>(U entity) where U : EntityWithTypedId<TId>;
        T SaveOrUpdate(T entity);
        U SaveOrUpdate<U>(U entity) where U : EntityWithTypedId<TId>;
        T UnProxy(T entity);
        void Evict(T entity);
        void Evict<U>(U entity);
        void ClearSession();
        IDbContext DbContext { get; }
    }
}
