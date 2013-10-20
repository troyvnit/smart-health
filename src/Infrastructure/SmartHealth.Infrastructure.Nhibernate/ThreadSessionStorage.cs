using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

using NHibernate;

namespace SmartHealth.Infrastructure.Nhibernate
{
    public class ThreadSessionStorage : ISessionStorage
    {
        private readonly ConcurrentDictionary<string, SimpleSessionStorage> perThreadSessionStorage =
            new ConcurrentDictionary<string, SimpleSessionStorage>();

        public IEnumerable<ISession> GetAllSessions()
        {
            return GetSimpleSessionStorageForThread().GetAllSessions();
        }

        public ISession GetSessionForKey(string factoryKey)
        {
            return GetSimpleSessionStorageForThread().GetSessionForKey(factoryKey);
        }

        public void SetSessionForKey(string factoryKey, ISession session)
        {
            GetSimpleSessionStorageForThread().SetSessionForKey(factoryKey, session);
        }

        public void RemoveAllSessions()
        {
            SimpleSessionStorage curent;
            perThreadSessionStorage.TryRemove(GetCurrentThreadName(), out curent);
        }

        private SimpleSessionStorage GetSimpleSessionStorageForThread()
        {
            string currentThreadName = GetCurrentThreadName();
            SimpleSessionStorage sessionStorage;
            if (!perThreadSessionStorage.TryGetValue(currentThreadName, out sessionStorage))
            {
                sessionStorage = new SimpleSessionStorage();
                perThreadSessionStorage.TryAdd(currentThreadName, sessionStorage);
            }

            return sessionStorage;
        }

        private string GetCurrentThreadName()
        {
            if (Thread.CurrentThread.Name == null)
            {
                Thread.CurrentThread.Name = Guid.NewGuid().ToString();
            }
            return Thread.CurrentThread.Name;
        }
    }
}
