using System.Collections.Generic;

using NHibernate;

namespace SmartHealth.Infrastructure.Nhibernate
{
    public interface ISessionStorage
    {
        IEnumerable<ISession> GetAllSessions();

        ISession GetSessionForKey(string factoryKey);

        void SetSessionForKey(string factoryKey, ISession session);
    }
}