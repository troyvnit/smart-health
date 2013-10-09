using System;
using System.Configuration;
using System.Configuration.Provider;

namespace VinaSale.Infrastructure.Caching
{
    public static class CacheManager
    {
        private static CacheProvider defaultCacheProvider;

        private static bool isInitialized;

        public static CacheProvider Provider
        {
            get
            {
                Initialize();
                return defaultCacheProvider;
            }
        }

        public static object Get(string key)
        {
            return Provider.Get(key);
        }

        public static void Remove(string key)
        {
            Provider.Remove(key);
        }

        public static void Set(string key, object value, CacheDuration duration)
        {
            Provider.Set(key, value, GetTimeSpanFromDuration(duration));
        }

        public static T Get<T>(string key, Func<T> funcIfNull, CacheDuration duration)
        {
            T item = (T)Provider.Get(key);
            if (item == null)
            {
                item = funcIfNull();
                if (item != null)
                {
                    Set(key, item, duration);
                }
            }
            return item;
        }

        private static TimeSpan GetTimeSpanFromDuration(CacheDuration duration)
        {
            switch (duration)
            {
                case CacheDuration.Short:
                    return Provider.ShortCacheDuration;
                case CacheDuration.Medium:
                    return Provider.MediumCacheDuration;
                default:
                    return Provider.LongCacheDuration;
            }
        }

        private static void Initialize()
        {
            if (isInitialized)
            {
                return;
            }
            var cacheProviderConfigSection = ConfigurationManager.GetSection("cacheProviderConfiguration") as CacheProviderConfiguration;
            if (cacheProviderConfigSection == null)
            {
                throw new ConfigurationErrorsException("Missing Cache Provider Configuration section");
            }

            var setting = cacheProviderConfigSection.Providers[cacheProviderConfigSection.DefaultProvider];
            defaultCacheProvider = ProvidersHelper.InstantiateProvider(setting, typeof(CacheProvider)) as CacheProvider;

            if (defaultCacheProvider == null)
            {
                throw new ProviderException("Cannot load default cache provider.");
            }
            isInitialized = true;
        }
    }
}