using System;
using System.Configuration.Provider;

namespace VinaSale.Infrastructure.Caching
{
    public abstract class CacheProvider : ProviderBase
    {
        public abstract TimeSpan ShortCacheDuration { get; }

        public abstract TimeSpan MediumCacheDuration { get; }

        public abstract TimeSpan LongCacheDuration { get; }

        public abstract void Set(string key, object value, TimeSpan timeout);

        public abstract object Get(string key);

        public abstract void Remove(string key);
    }
}
