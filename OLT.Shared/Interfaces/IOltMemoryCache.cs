using System;

namespace OLT.Core
{
    public interface IOltMemoryCache : IOltInjectableSingleton
    {
        int DefaultExpirationInMinutes { get; }

        /// <summary>
        /// A generic method for getting and setting objects to the memory cache using auto-created key of "cached_{typeof(<typeparam name="TEntry"/>).FullName}". Uses DefaultExpirationInMinutes setting
        /// </summary>
        /// <typeparam name="TEntry">The type of the object to be returned.</typeparam>
        /// <param name="factory">A parameterless function to call if the object isn't in the cache and you need to set it.</param>
        /// <returns>An object of the type you asked for</returns>
        TEntry Get<TEntry>(Func<TEntry> factory);


        /// <summary>
        /// A generic method for getting and setting objects to the memory cache. (Uses DefaultExpirationInMinutes Setting) 
        /// </summary>
        /// <typeparam name="TEntry">The type of the object to be returned.</typeparam>
        /// <param name="key">The name to be used when storing this object in the cache.</param>
        /// <param name="factory">A parameterless function to call if the object isn't in the cache and you need to set it.</param>
        /// <returns>An object of the type you asked for</returns>
        TEntry Get<TEntry>(string key, Func<TEntry> factory);

        /// <summary>
        /// A generic method for getting and setting objects to the memory cache.
        /// </summary>
        /// <typeparam name="TEntry">The type of the object to be returned.</typeparam>
        /// <param name="key">The name to be used when storing this object in the cache.</param>
        /// <param name="cacheTimeInMinutes">How long (in minutes) to cache this object for.</param>
        /// <param name="factory">A parameterless function to call if the object isn't in the cache and you need to set it.</param>
        /// <returns>An object of the type you asked for</returns>
        TEntry Get<TEntry>(string key, int cacheTimeInMinutes, Func<TEntry> factory);

        /// <summary>
        /// A generic method for getting and setting objects to the memory cache.
        /// </summary>
        /// <typeparam name="TEntry">The type of the object to be returned.</typeparam>
        /// <param name="key">The name to be used when storing this object in the cache.</param>
        /// <param name="absoluteExpiration">When to expire the object to cache this object for.</param>
        /// <param name="factory">A parameterless function to call if the object isn't in the cache and you need to set it.</param>
        /// <returns>An object of the type you asked for</returns>
        TEntry Get<TEntry>(string key, DateTimeOffset absoluteExpiration, Func<TEntry> factory);


        /// <summary>
        /// A generic method for getting and setting objects to the memory cache.
        /// </summary>
        /// <param name="key">The name to be used for this object in the cache.</param>
        void Remove(string key);
    }

}