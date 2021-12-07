using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace OLT.Core
{

    public static class OltMemoryCacheServiceCollectionExtensions
    {

        /// <summary>
        /// Adds Memory Cache
        /// </summary>
        /// <remarks>
        /// Registers <see cref="IOltMemoryCache"/> as a singleton
        /// </remarks>
        /// <param name="services"><seealso cref="IServiceCollection"/></param>
        /// <param name="expirationMinutes"></param>
        /// <returns><seealso cref="IServiceCollection"/></returns>
        public static IServiceCollection AddOltAddMemoryCache(this IServiceCollection services, TimeSpan defaultSlidingExpiration, TimeSpan defaultAbsoluteExpiration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return services
                .AddOltAddMemoryCache(o => 
                    new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(defaultSlidingExpiration)
                        .SetAbsoluteExpiration(DateTimeOffset.Now.Add(defaultAbsoluteExpiration)));
        }

        /// <summary>
        /// Adds Memory Cache
        /// </summary>
        /// <remarks>
        /// Registers <see cref="IOltMemoryCache"/> as a singleton
        /// </remarks>
        /// <param name="services"><seealso cref="IServiceCollection"/></param>
        /// <param name="expirationMinutes"></param>
        /// <returns><seealso cref="IServiceCollection"/></returns>
        public static IServiceCollection AddOltAddMemoryCache(this IServiceCollection services, Action<MemoryCacheOptions> setupAction) 
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return services
                .AddSingleton<IOltMemoryCache, OltMemoryCache>()
                .AddMemoryCache(setupAction);
        }
    }
}
