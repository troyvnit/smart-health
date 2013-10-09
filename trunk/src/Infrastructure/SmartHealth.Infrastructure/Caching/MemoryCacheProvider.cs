using System;
using System.Collections.Specialized;
using System.Runtime.Caching;

namespace VinaSale.Infrastructure.Caching
{
    public class MemoryCacheProvider : CacheProvider
    {
        private ObjectCache cache;

        private TimeSpan shortCacheDuration;

        private TimeSpan mediumCacheDuration;

        private TimeSpan longCacheDuration;

        public override void Initialize(string name, NameValueCollection config)
        {
            base.Initialize(name, config);
            cache = MemoryCache.Default;

            shortCacheDuration = TimeSpanHelper.Parse(config["shortCacheDuration"]);
            mediumCacheDuration = TimeSpanHelper.Parse(config["mediumCacheDuration"]);
            longCacheDuration = TimeSpanHelper.Parse(config["longCacheDuration"]);
        }

        public override void Set(string key, object value, TimeSpan timeout)
        {
            var policy = new CacheItemPolicy { SlidingExpiration = timeout };
            cache.Set(key, value, policy);
        }

        public override object Get(string key)
        {
            return cache.Get(key);
        }

        public override void Remove(string key)
        {
            cache.Remove(key);
        }

        public override TimeSpan ShortCacheDuration
        {
            get
            {
                return shortCacheDuration;
            }
        }

        public override TimeSpan MediumCacheDuration
        {
            get
            {
                return mediumCacheDuration;
            }
        }

        public override TimeSpan LongCacheDuration
        {
            get
            {
                return longCacheDuration;
            }
        }
    }
}
