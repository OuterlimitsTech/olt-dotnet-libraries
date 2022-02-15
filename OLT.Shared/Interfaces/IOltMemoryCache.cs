using System;
using System.Threading.Tasks;

namespace OLT.Core
{
    public interface IOltMemoryCache : IDisposable
    {
        /// <summary>
        /// A generic method for getting and setting objects to the memory cache
        /// </summary>
        /// <typeparam name="TEntry">The type of the object to be returned.</typeparam>
        /// <param name="key">The name to be used when storing this object in the cache.</param>
        /// <param name="factory">A parameterless function to call if the object isn't in the cache and you need to set it.</param>
        /// <param name="slidingExpiration">Expire cache after sliding Expiration. (uses default if not supplied)</param>
        /// <param name="absoluteExpiration">Expire cache at. (uses default if not supplied)</param>
        /// <returns>An object of the type you asked for</returns>
        TEntry Get<TEntry>(string key, Func<TEntry> factory, TimeSpan? slidingExpiration = null, TimeSpan? absoluteExpiration = null);


        /// <summary>
        /// A generic method for getting and setting objects to the memory cache.
        /// </summary>
        /// <typeparam name="TEntry">The type of the object to be returned.</typeparam>
        /// <param name="key">The name to be used when storing this object in the cache.</param>
        /// <param name="factory">A parameterless function to call if the object isn't in the cache and you need to set it.</param>
        /// <param name="slidingExpiration">Expire cache after sliding Expiration. (uses default if not supplied)</param>
        /// <param name="absoluteExpiration">Expire cache at. (uses default if not supplied)</param>
        /// <returns>An object of the type you asked for</returns>
        Task<TEntry> GetAsync<TEntry>(string key, Func<Task<TEntry>> factory, TimeSpan? slidingExpiration = null, TimeSpan? absoluteExpiration = null);


        /// <summary>
        /// A generic method for getting and setting objects to the memory cache.
        /// </summary>
        /// <param name="key">The name to be used for this object in the cache.</param>
        void Remove(string key);
    }

}