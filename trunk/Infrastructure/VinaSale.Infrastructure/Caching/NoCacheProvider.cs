using System;

namespace VinaSale.Infrastructure.Caching
{
    public class NoCacheProvider : CacheProvider
    {
        public override void Set(string key, object value, TimeSpan timeout)
        {
        }

        public override object Get(string key)
        {
            return null;
        }

        public override void Remove(string key)
        {
        }

        public override TimeSpan ShortCacheDuration
        {
            get
            {
                return TimeSpan.Zero; 
            }
        }

        public override TimeSpan MediumCacheDuration
        {
            get
            {
                return TimeSpan.Zero;
            }
        }

        public override TimeSpan LongCacheDuration
        {
            get
            {
                return TimeSpan.Zero;
            }
        }
    }
}
