using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

using NHibernate;
using NHibernate.Cfg;

namespace VinaSale.Infrastructure.Nhibernate
{
    public class NHibernateSession
    {
        /// <summary>
        ///     The default factory key used if only one database is being communicated with.
        /// </summary>
        public static readonly string DefaultFactoryKey = "nhibernate.current_session";

        /// <summary>
        ///     Maintains a dictionary of NHibernate session factories, one per database.  The key is 
        ///     the "factory key" used to look up the associated database, and used to decorate respective
        ///     repositories.  If only one database is being used, this dictionary contains a single
        ///     factory with a key of <see cref="DefaultFactoryKey" />.
        /// </summary>
        private static readonly Dictionary<string, ISessionFactory> sessionFactories = new Dictionary<string, ISessionFactory>();

        private static IInterceptor registeredInterceptor;

        /// <summary>
        ///     Used to get the current NHibernate session if you're communicating with a single database.
        ///     When communicating with multiple databases, invoke <see cref="CurrentFor()" /> instead.
        /// </summary>
        public static ISession Current
        {
            get
            {
                if (IsConfiguredForMultipleDatabases())
                {
                    throw new ApplicationException("The NHibernateSession.Current property may " +
                                                   "only be invoked if you only have one NHibernate session factory; i.e., you're " +
                                                   "only communicating with one database.  Since you're configured communications " +
                                                   "with multiple databases, you should instead call CurrentFor(string factoryKey)");
                }

                return CurrentFor(DefaultFactoryKey);
            }
        }

        /// <summary>
        ///     An application-specific implementation of ISessionStorage must be setup either thru
        ///     <see cref="InitStorage" /> or one of the <see cref="Init" /> overloads.
        /// </summary>
        public static ISessionStorage Storage { get; set; }

        public static Configuration AddConfiguration(string factoryKey, string[] mappingAssemblies, AutoPersistenceModel autoPersistenceModel, string cfgFile, IDictionary<string, string> cfgProperties, IPersistenceConfigurer persistenceConfigurer)
        {
            Configuration config = ConfigureNHibernate(cfgFile, cfgProperties);
            ISessionFactory sessionFactory = CreateSessionFactoryFor(mappingAssemblies, autoPersistenceModel, config, persistenceConfigurer);
            if (sessionFactories.ContainsKey(factoryKey))
            {
                throw new ApplicationException("A session factory has already been configured with the key of " + factoryKey);
            }

            sessionFactories.Add(factoryKey, sessionFactory);

            return config;
        }

        /// <summary>
        ///     This method is used by application-specific session storage implementations
        ///     and unit tests. Its job is to walk thru existing cached sessions and Close() each one.
        /// </summary>
        public static void CloseAllSessions()
        {
            if (Storage != null)
            {
                foreach (var session in Storage.GetAllSessions())
                {
                    if (session.IsOpen)
                    {
                        session.Close();
                    }
                }
            }
        }

        /// <summary>
        ///     Used to get the current NHibernate session associated with a factory key; i.e., the key 
        ///     associated with an NHibernate session factory for a specific database.
        /// 
        ///     If you're only communicating with one database, you should call <see cref="Current" /> instead,
        ///     although you're certainly welcome to call this if you have the factory key available.
        /// </summary>
        public static ISession CurrentFor(string factoryKey)
        {
            if (string.IsNullOrEmpty(factoryKey))
            {
                throw new ApplicationException("FactoryKey may not be null or empty");
            }

            if (Storage == null)
            {
                throw new ApplicationException("An ISessionStorage has not been configured");
            }

            if (!sessionFactories.ContainsKey(factoryKey))
            {
                throw new ApplicationException("AnISessionFactory does not exist with a factory key of " + factoryKey);
            }

            ISession session = Storage.GetSessionForKey(factoryKey);

            if (session == null)
            {
                if (registeredInterceptor != null)
                {
                    session = sessionFactories[factoryKey].OpenSession(registeredInterceptor);
                }
                else
                {
                    session = sessionFactories[factoryKey].OpenSession();
                }

                Storage.SetSessionForKey(factoryKey, session);
            }

            return session;
        }

        /// <summary>
        ///     Returns the default ISessionFactory using the DefaultFactoryKey.
        /// </summary>
        public static ISessionFactory GetDefaultSessionFactory()
        {
            return GetSessionFactoryFor(DefaultFactoryKey);
        }

        /// <summary>
        ///     Return an ISessionFactory based on the specified factoryKey.
        /// </summary>
        public static ISessionFactory GetSessionFactoryFor(string factoryKey)
        {
            if (!sessionFactories.ContainsKey(factoryKey))
            {
                return null;
            }

            return sessionFactories[factoryKey];
        }

        public static Configuration Init(ISessionStorage storage, string[] mappingAssemblies, AutoPersistenceModel autoPersistenceModel, string cfgFile)
        {
            return Init(storage, mappingAssemblies, autoPersistenceModel, cfgFile, null, null);
        }

        public static Configuration Init(ISessionStorage storage, string[] mappingAssemblies, AutoPersistenceModel autoPersistenceModel, string cfgFile, IDictionary<string, string> cfgProperties, IPersistenceConfigurer persistenceConfigurer)
        {
            InitStorage(storage);
            try
            {
                return AddConfiguration(DefaultFactoryKey, mappingAssemblies, autoPersistenceModel, cfgFile, cfgProperties, persistenceConfigurer);
            }
            catch
            {
                // If this NHibernate config throws an exception, null the Storage reference so 
                // the config can be corrected without having to restart the web application.
                Storage = null;
                throw;
            }
        }

        public static void InitStorage(ISessionStorage storage)
        {
            if (storage == null)
            {
                throw new ApplicationException("storage mechanism was null but must be provided.");
            }

            if (Storage != null)
            {
                throw new ApplicationException("A storage mechanism has already been configured for this application");
            }

            Storage = storage;
        }

        public static bool IsConfiguredForMultipleDatabases()
        {
            return sessionFactories.Count > 1;
        }

        public static void RegisterInterceptor(IInterceptor interceptor)
        {
            if (interceptor == null)
            {
                throw new ApplicationException("Interceptor may not be null.");
            }

            registeredInterceptor = interceptor;
        }

        public static void RemoveSessionFactoryFor(string factoryKey)
        {
            if (GetSessionFactoryFor(factoryKey) != null)
            {
                sessionFactories.Remove(factoryKey);
            }
        }

        /// <summary>
        ///     To facilitate unit testing, this method will reset this object back to its
        ///     original state before it was configured.
        /// </summary>
        public static void Reset()
        {
            if (Storage != null)
            {
                foreach (var session in Storage.GetAllSessions())
                {
                    session.Dispose();
                }
            }

            sessionFactories.Clear();

            Storage = null;
            registeredInterceptor = null;
        }

        private static Configuration ConfigureNHibernate(string cfgFile, IDictionary<string, string> cfgProperties)
        {
            var cfg = new Configuration();

            if (cfgProperties != null)
            {
                cfg.AddProperties(cfgProperties);
            }

            if (string.IsNullOrEmpty(cfgFile) == false)
            {
                return cfg.Configure(cfgFile);
            }

            if (File.Exists("Hibernate.cfg.xml"))
            {
                return cfg.Configure();
            }

            return cfg;
        }

        private static ISessionFactory CreateSessionFactoryFor(string[] mappingAssemblies, AutoPersistenceModel autoPersistenceModel, Configuration cfg, IPersistenceConfigurer persistenceConfigurer)
        {
            FluentConfiguration fluentConfiguration = Fluently.Configure(cfg);

            if (persistenceConfigurer != null)
            {
                fluentConfiguration.Database(persistenceConfigurer);
            }

            fluentConfiguration.Mappings(m =>
                {
                    foreach (var mappingAssembly in mappingAssemblies)
                    {
                        var assembly = Assembly.LoadFrom(MakeLoadReadyAssemblyName(mappingAssembly));

                        m.HbmMappings.AddFromAssembly(assembly);
                        m.FluentMappings.AddFromAssembly(assembly).Conventions.AddAssembly(assembly);
                    }

                    if (autoPersistenceModel != null)
                    {
                        m.AutoMappings.Add(autoPersistenceModel);
                    }

                    // m.AutoMappings.ExportTo(@"E:\Projects\Fantastic\mapping");
                    // m.FluentMappings.ExportTo(@"E:\Projects\Fantastic\mapping");
                });

            return fluentConfiguration.BuildSessionFactory();
        }

        private static string MakeLoadReadyAssemblyName(string assemblyName)
        {
            return (assemblyName.IndexOf(".dll") == -1) ? assemblyName.Trim() + ".dll" : assemblyName.Trim();
        }
    }
}
