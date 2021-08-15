using System;

// ReSharper disable once CheckNamespace
namespace OLT.Core
{
    public abstract class OltMemoryCacheBase : OltDisposable, IOltMemoryCache
    {

        public virtual int DefaultExpirationInMinutes { get; set; } = 30;

        /// <summary>
        /// A generic method for getting and setting objects to the memory cache. Uses DefaultExpirationInMinutes setting
        /// </summary>
        /// <typeparam name="TEntry">The type of the object to be returned.</typeparam>
        /// <param name="key">The name to be used when storing this object in the cache.</param>
        /// <param name="factory">A parameterless function to call if the object isn't in the cache and you need to set it.</param>
        /// <returns>An object of the type you asked for</returns>
        public virtual TEntry Get<TEntry>(string key, Func<TEntry> factory)
        {
            return Get(key, DateTimeOffset.Now.AddMinutes(DefaultExpirationInMinutes), factory);
        }

        /// <summary>
        /// A generic method for getting and setting objects to the memory cache.
        /// </summary>
        /// <typeparam name="TEntry">The type of the object to be returned.</typeparam>
        /// <param name="key">The name to be used when storing this object in the cache.</param>
        /// <param name="cacheTimeInMinutes">How long (in minutes) to cache this object for.</param>
        /// <param name="factory">A parameterless function to call if the object isn't in the cache and you need to set it.</param>
        /// <returns>An object of the type you asked for</returns>
        public virtual TEntry Get<TEntry>(string key, int cacheTimeInMinutes, Func<TEntry> factory)
        {
            return Get(key, DateTimeOffset.Now.AddMinutes(cacheTimeInMinutes), factory);
        }

        /// <summary>
        /// A generic method for getting and setting objects to the memory cache using auto-created key of "cached_{typeof(TEntry).FullName}". Uses DefaultExpirationInMinutes setting
        /// </summary>
        /// <typeparam name="TEntry">The type of the object to be returned.</typeparam>
        /// <param name="factory">A parameterless function to call if the object isn't in the cache and you need to set it.</param>
        /// <returns>An object of the type you asked for</returns>
        public TEntry Get<TEntry>(Func<TEntry> factory)
        {
            return Get($"cached_{typeof(TEntry).FullName}", factory);
        }

        public abstract TEntry Get<TEntry>(string key, DateTimeOffset absoluteExpiration, Func<TEntry> factory);

        public abstract void Remove(string key);

    }

}