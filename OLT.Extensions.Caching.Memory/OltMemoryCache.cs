using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace OLT.Core
{
    public class OltMemoryCache : OltMemoryCacheBase
    {
        private readonly IMemoryCache _memoryCache;

        public OltMemoryCache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public override void Remove(string key)
        {
            _memoryCache.Remove(ToCacheKey(key));
        }

        public override TEntry Get<TEntry>(string key, Func<TEntry> factory, TimeSpan? slidingExpiration = null, TimeSpan? absoluteExpiration = null)
        {
            var cacheEntry = _memoryCache.GetOrCreate(ToCacheKey(key), entry =>
            {
                if (slidingExpiration.HasValue)
                {
                    entry.SlidingExpiration = slidingExpiration;
                }

                if (absoluteExpiration.HasValue)
                {
                    entry.AbsoluteExpiration = DateTimeOffset.Now.Add(absoluteExpiration.Value);
                }
                return factory();
            });

            return cacheEntry;
        }

        public override async Task<TEntry> GetAsync<TEntry>(string key, Func<Task<TEntry>> factory, TimeSpan? slidingExpiration = null, TimeSpan? absoluteExpiration = null)
        {
            var cacheEntry = await
                  _memoryCache.GetOrCreateAsync(ToCacheKey(key), async entry =>
                  {
                      if (slidingExpiration.HasValue)
                      {
                          entry.SlidingExpiration = slidingExpiration;
                      }
                      
                      if (absoluteExpiration.HasValue)
                      {
                          entry.AbsoluteExpiration = DateTimeOffset.Now.Add(absoluteExpiration.Value);
                      }
                      
                      return await factory();
                  });
            return cacheEntry;
        }



    }
}
