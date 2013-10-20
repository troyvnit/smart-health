using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using SmartHealth.Infrastructure.Domain.DataInterfaces;
using SmartHealth.Infrastructure.Domain.Models;
using SmartHealth.Infrastructure.Domain.Validation;

namespace SmartHealth.Infrastructure.Bussiness
{
    public class Service<T> : ServiceWithTypedId<T, int>, IService<T> where T : EntityWithTypedId<int>
    {
        public Service(IRepository<T> repository)
            : base(repository)
        {
        }
    }

    public class ServiceWithTypedId<T, IdT> : IServiceWithTypedId<T, IdT> where T : EntityWithTypedId<IdT>
    {
        private IRepositoryWithTypedId<T, IdT> repository;

        public ServiceWithTypedId(IRepositoryWithTypedId<T, IdT> repository)
        {
            this.repository = repository;
        }

        public virtual T Get(IdT id)
        {
            return repository.Get(id);
        }

        public virtual U Get<U>(IdT id) where U : EntityWithTypedId<IdT>
        {
            return repository.Get<U>(id);
        }

        public virtual T Load(IdT id)
        {
            return repository.Load(id);
        }

        public virtual U Load<U>(IdT id) where U : EntityWithTypedId<IdT>
        {
            return repository.Load<U>(id);
        }

        public virtual IQueryable<T> GetQuery()
        {
            return repository.GetQuery();
        }

        public virtual IQueryable<U> GetQuery<U>() where U : EntityWithTypedId<IdT>
        {
            return repository.GetQuery<U>();
        }

        public virtual IList<T> GetAll()
        {
            return GetAll<T>();
        }

        public virtual IList<U> GetAll<U>() where U : EntityWithTypedId<IdT>
        {
            return repository.GetAll<U>();
        }

        public IList<T> GetAllOrderBy(Expression<Func<T, object>> orderBy)
        {
            return GetAllOrderBy<T>(orderBy);
        }

        public IList<T> GetAllOrderByDescending(Expression<Func<T, object>> orderBy)
        {
            return GetAllOrderByDescending<T>(orderBy);
        }

        public IList<U> GetAllOrderBy<U>(Expression<Func<U, object>> orderBy) where U : EntityWithTypedId<IdT>
        {
            return repository.GetAllOrderBy<U>(orderBy);
        }

        public IList<U> GetAllOrderByDescending<U>(Expression<Func<U, object>> orderBy) where U : EntityWithTypedId<IdT>
        {
            return repository.GetAllOrderByDescending<U>(orderBy);
        }

        public IList<T> FindAll(Expression<Func<T, bool>> criteria)
        {
            return FindAll<T>(criteria);
        }

        public virtual IList<T> FindAllOrderBy(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> orderBy)
        {
            return FindAllOrderBy<T>(criteria, orderBy);
        }

        public virtual IList<T> FindAllOrderByDescending(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> orderBy)
        {
            return FindAllOrderByDescending<T>(criteria, orderBy);
        }

        public IList<U> FindAll<U>(Expression<Func<U, bool>> criteria) where U : EntityWithTypedId<IdT>
        {
            return repository.FindAll<U>(criteria);
        }

        public virtual IList<U> FindAllOrderBy<U>(Expression<Func<U, bool>> criteria, Expression<Func<U, object>> orderBy) where U : EntityWithTypedId<IdT>
        {
            return repository.FindAllOrderBy<U>(criteria, orderBy);
        }

        public virtual IList<U> FindAllOrderByDescending<U>(Expression<Func<U, bool>> criteria, Expression<Func<U, object>> orderBy) where U : EntityWithTypedId<IdT>
        {
            return repository.FindAllOrderByDescending<U>(criteria, orderBy);
        }

        public virtual void Save(T entity, bool commitTransaction = false)
        {
            Save<T>(entity, commitTransaction);
        }

        public virtual void Save<U>(U entity, bool commitTransaction = false) where U : EntityWithTypedId<IdT>
        {
            if (!entity.IsValid())
                throw new ValidationException(entity.ValidationResults());

            if (!commitTransaction)
            {
                repository.Save<U>(entity);
                return;
            }

            repository.DbContext.BeginTransaction();
            try
            {
                repository.Save<U>(entity);
                repository.DbContext.CommitTransaction();
            }
            catch
            {
                repository.DbContext.RollbackTransaction();
                throw;
            }
        }

        public virtual void SaveOrUpdate(T entity, bool commitTransaction = false)
        {
            SaveOrUpdate<T>(entity, commitTransaction);
        }

        public virtual void SaveOrUpdate<U>(U entity, bool commitTransaction = false) where U : EntityWithTypedId<IdT>
        {
            if (!entity.IsValid())
                throw new ValidationException(entity.ValidationResults());

            if (!commitTransaction)
            {
                repository.SaveOrUpdate<U>(entity);
                return;
            }

            repository.DbContext.BeginTransaction();
            try
            {
                repository.SaveOrUpdate<U>(entity);
                repository.DbContext.CommitTransaction();
            }
            catch
            {
                repository.DbContext.RollbackTransaction();
                throw;
            }
        }

        public virtual void Delete<U>(U entity, bool commitTransaction = false) where U : EntityWithTypedId<IdT>
        {
            if (!commitTransaction)
            {
                repository.Delete<U>(entity);
                return;
            }

            repository.DbContext.BeginTransaction();
            try
            {
                repository.Delete<U>(entity);
                repository.DbContext.CommitTransaction();
            }
            catch
            {
                repository.DbContext.RollbackTransaction();
                throw;
            }
        }

        public virtual void Delete(T entity, bool commitTransaction = false)
        {
            Delete<T>(entity, commitTransaction);
        }

        public virtual void Evict(T entity)
        {
            Evict<T>(entity);
        }

        public virtual void Evict<U>(U entity) where U : EntityWithTypedId<IdT>
        {
            repository.Evict<U>(entity);
        }

        public virtual void ClearSession()
        {
            repository.ClearSession();
        }
    }
}
