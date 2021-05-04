using System;
using Microsoft.Extensions.Caching.Memory;

namespace OLT.Core
{

    public class OltMemoryCache : OltMemoryCacheBase
    {
        private readonly IMemoryCache _memoryCache;

        public OltMemoryCache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }


        public override TEntry Get<TEntry>(string key, DateTimeOffset absoluteExpiration, Func<TEntry> factory)
        {
            return _memoryCache.GetOrCreate(key, entry => factory.Invoke());
        }

        public override void Remove(string key)
        {
            _memoryCache.Remove(key);
        }
    }
}