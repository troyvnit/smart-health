using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using SmartHealth.Infrastructure.Domain.Models;

namespace SmartHealth.Infrastructure.Bussiness
{
    public interface IService<T> : IServiceWithTypedId<T, int> where T : EntityWithTypedId<int> { }

    public interface IServiceWithTypedId<T, IdT> where T : EntityWithTypedId<IdT>
    {
        T Get(IdT id);
        U Get<U>(IdT id) where U : EntityWithTypedId<IdT>;
        T Load(IdT id);
        U Load<U>(IdT id) where U : EntityWithTypedId<IdT>;
        IQueryable<T> GetQuery();
        IQueryable<U> GetQuery<U>() where U : EntityWithTypedId<IdT>;
        IList<T> GetAll();
        IList<U> GetAll<U>() where U : EntityWithTypedId<IdT>;
        IList<T> GetAllOrderBy(Expression<Func<T, object>> orderBy);
        IList<T> GetAllOrderByDescending(Expression<Func<T, object>> orderBy);
        IList<U> GetAllOrderBy<U>(Expression<Func<U, object>> orderBy) where U : EntityWithTypedId<IdT>;
        IList<U> GetAllOrderByDescending<U>(Expression<Func<U, object>> orderBy) where U : EntityWithTypedId<IdT>;
        IList<T> FindAll(Expression<Func<T, bool>> criteria);
        IList<T> FindAllOrderBy(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> orderBy);
        IList<T> FindAllOrderByDescending(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> orderBy);
        IList<U> FindAll<U>(Expression<Func<U, bool>> criteria) where U : EntityWithTypedId<IdT>;
        IList<U> FindAllOrderBy<U>(Expression<Func<U, bool>> criteria, Expression<Func<U, object>> orderBy) where U : EntityWithTypedId<IdT>;
        IList<U> FindAllOrderByDescending<U>(Expression<Func<U, bool>> criteria, Expression<Func<U, object>> orderBy) where U : EntityWithTypedId<IdT>;
        void Save(T entity, bool commitTransaction = false);
        void Save<U>(U entity, bool commitTransaction = false) where U : EntityWithTypedId<IdT>;
        void SaveOrUpdate(T entity, bool commitTransaction = false);
        void SaveOrUpdate<U>(U entity, bool commitTransaction = false) where U : EntityWithTypedId<IdT>;
        void Delete<U>(U entity, bool commitTransaction = false) where U : EntityWithTypedId<IdT>;
        void Delete(T entity, bool commitTransaction = false);
        void Evict(T entity);
        void Evict<U>(U entity) where U : EntityWithTypedId<IdT>;
        void ClearSession();
    }
}
