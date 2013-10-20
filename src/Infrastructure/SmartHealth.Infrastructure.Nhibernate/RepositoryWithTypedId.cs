using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Linq;

using SmartHealth.Infrastructure.Domain.DataInterfaces;
using SmartHealth.Infrastructure.Domain.Models;

namespace SmartHealth.Infrastructure.Nhibernate
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

        public virtual IDbContext DbContext
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

        public virtual U Get<U>(TId id) where U : EntityWithTypedId<TId>
        {
            return Session.Get<U>(id);
        }

        public virtual T Load(TId id)
        {
            return Session.Load<T>(id);
        }

        public virtual U Load<U>(TId id) where U : EntityWithTypedId<TId>
        {
            return Session.Load<U>(id);
        }

        public virtual IQueryable<T> GetQuery()
        {
            return Session.Query<T>();
        }

        public virtual IQueryable<U> GetQuery<U>() where U : EntityWithTypedId<TId>
        {
            return Session.Query<U>();
        }

        public virtual IList<T> GetAll()
        {
            return Session.Query<T>().ToList();
        }

        public virtual IList<U> GetAll<U>() where U : EntityWithTypedId<TId>
        {
            return Session.Query<U>().ToList();
        }

        public virtual IList<T> GetAllOrderBy(Expression<Func<T, object>> orderBy)
        {
            return Session.Query<T>().OrderBy(orderBy).ToList();
        }

        public virtual IList<U> GetAllOrderBy<U>(Expression<Func<U, object>> orderBy) where U : EntityWithTypedId<TId>
        {
            return Session.Query<U>().OrderBy(orderBy).ToList();
        }

        public virtual IList<T> GetAllOrderByDescending(Expression<Func<T, object>> orderBy)
        {
            return Session.Query<T>().OrderByDescending(orderBy).ToList();
        }

        public virtual IList<U> GetAllOrderByDescending<U>(Expression<Func<U, object>> orderBy) where U : EntityWithTypedId<TId>
        {
            return Session.Query<U>().OrderByDescending(orderBy).ToList();
        }

        public virtual IList<T> FindAll(Expression<Func<T, bool>> criteria)
        {
            return Session.Query<T>().Where(criteria).ToList();
        }

        public virtual IList<T> FindAllOrderBy(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> orderBy)
        {
            return Session.Query<T>().Where(criteria).OrderBy(orderBy).ToList();
        }

        public virtual IList<T> FindAllOrderByDescending(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> orderBy)
        {
            return Session.Query<T>().Where(criteria).OrderByDescending(orderBy).ToList();
        }

        public virtual IList<U> FindAll<U>(Expression<Func<U, bool>> criteria)
        {
            return Session.Query<U>().Where(criteria).ToList();
        }

        public virtual IList<U> FindAllOrderBy<U>(Expression<Func<U, bool>> criteria, Expression<Func<U, object>> orderBy)
        {
            return Session.Query<U>().Where(criteria).OrderBy(orderBy).ToList();
        }

        public virtual IList<U> FindAllOrderByDescending<U>(Expression<Func<U, bool>> criteria, Expression<Func<U, object>> orderBy)
        {
            return Session.Query<U>().Where(criteria).OrderByDescending(orderBy).ToList();
        }

        public virtual T Save(T entity)
        {
            Session.Save(entity);
            return entity;
        }

        public virtual U Save<U>(U entity) where U : EntityWithTypedId<TId>
        {
            Session.Save(entity);
            return entity;
        }

        public virtual T Update(T entity)
        {
            Session.Update(entity);
            return entity;
        }

        public virtual U Update<U>(U entity) where U : EntityWithTypedId<TId>
        {
            Session.Update(entity);
            return entity;
        }

        public virtual void Delete(T entity)
        {
            Session.Delete(entity);
        }

        public virtual void Delete<U>(U entity) where U : EntityWithTypedId<TId>
        {
            Session.Delete(entity);
        }

        /// <summary>
        /// Although SaveOrUpdate _can_ be invoked to update an object with an assigned Id, you are 
        /// hereby forced instead to use Save/Update for better clarity.
        /// </summary>
        public virtual T SaveOrUpdate(T entity)
        {
            Session.SaveOrUpdate(entity);
            return entity;
        }

        public virtual U SaveOrUpdate<U>(U entity) where U : EntityWithTypedId<TId>
        {
            Session.SaveOrUpdate(entity);
            return entity;
        }

        public virtual void Evict(T entity)
        {
            Session.Evict(entity);
        }

        public virtual void Evict<U>(U entity)
        {
            Session.Evict(entity);
        }

        public virtual void ClearSession()
        {
            Session.Clear();
        }

        public virtual T UnProxy(T entity)
        {
            return (T)Session.GetSessionImplementation().PersistenceContext.Unproxy(entity);
        }
    }
}
