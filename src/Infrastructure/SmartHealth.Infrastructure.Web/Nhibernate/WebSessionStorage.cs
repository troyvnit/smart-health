using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;

using NHibernate;
using NHibernate.Tool.hbm2ddl;

using SmartHealth.Infrastructure.Nhibernate;

namespace SmartHealth.Infrastructure.Web.Nhibernate
{
    public class WebSessionStorage : ISessionStorage
    {
        private const string HttpContextSessionStorageKey = "HttpContextSessionStorageKey";

        public WebSessionStorage(HttpApplication app)
        {
            app.EndRequest += Application_EndRequest;
            app.BeginRequest += Application_BeginRequest;
        }

        public IEnumerable<ISession> GetAllSessions()
        {
            SimpleSessionStorage storage = GetSimpleSessionStorage();
            return storage.GetAllSessions();
        }

        public ISession GetSessionForKey(string factoryKey)
        {
            SimpleSessionStorage storage = GetSimpleSessionStorage();
            return storage.GetSessionForKey(factoryKey);
        }

        public void SetSessionForKey(string factoryKey, ISession session)
        {
            SimpleSessionStorage storage = GetSimpleSessionStorage();
            storage.SetSessionForKey(factoryKey, session);
        }

        private void Application_BeginRequest(object sender, EventArgs e)
        {
            NHibernateInitializer.Instance().InitializeNHibernateOnce(InitializeNHibernateSession);
        }

        private void Application_EndRequest(object sender, EventArgs e)
        {
            NHibernateSession.CloseAllSessions();
            HttpContext context = HttpContext.Current;
            context.Items.Remove(HttpContextSessionStorageKey);
        }

        private SimpleSessionStorage GetSimpleSessionStorage()
        {
            HttpContext context = HttpContext.Current;
            var storage = context.Items[HttpContextSessionStorageKey] as SimpleSessionStorage;
            if (storage == null)
            {
                storage = new SimpleSessionStorage();
                context.Items[HttpContextSessionStorageKey] = storage;
            }

            return storage;
        }

        private void InitializeNHibernateSession()
        {
            NHibernate.Cfg.Configuration cofig = NHibernateSession.Init(
               this,
               new []
                   {
                       HttpContext.Current.Server.MapPath("~/bin/SmartHealth.Core.dll"),
                       HttpContext.Current.Server.MapPath("~/bin/SmartHealth.Box.dll"),
                       HttpContext.Current.Server.MapPath("~/bin/SmartHealth.SampleModule.dll"),
                   },
               new AutoPersistenceModelGenerator().Generate(),
               HttpContext.Current.Server.MapPath("~/NHibernate.config"));

            if (ConfigurationManager.AppSettings["AutoUpdateSchema"] == "true")
            {
                SchemaUpdate update = new SchemaUpdate(cofig);
                update.Execute(false, true);
            }
        }
    }
}
