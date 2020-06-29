using System;
using Microsoft.Extensions.Caching.Memory;

namespace ProductsAPI.Helpers
{
    public interface ICacheHelper
    {
        T GetOrSet<T>(string cacheKey, Func<T> getItemCallback,int duration) where T : class;
        void UpdateCache<T>(string cacheKey, Func<T> getItemCallback, int duration) where T : class;
    }

    public class CacheHelper: ICacheHelper
    {

        private readonly IMemoryCache memoryCache;

        public CacheHelper(IMemoryCache _memoryCache) =>
                    memoryCache = _memoryCache;

        ///// <summary>
        ///// Gets or sets the cached entity
        ///// </summary>
        ///// <typeparam name="T">Entity type</typeparam>
        ///// <param name="cacheKey">Cache Key string</param>
        ///// <param name="getItemCallback">Delegate of the callback function</param>
        ///// <returns></returns>
        public T GetOrSet<T>(string cacheKey, Func<T> getItemCallback, int duration) where T : class
        {
            T item = memoryCache.Get<T>(cacheKey);
            if (item == null)
            {
                int cacheDuration = 3;
                item = getItemCallback();
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(cacheDuration));
                memoryCache.Set("cacheKey", item, cacheEntryOptions);
            }
            return item;
        }

        ///// <summary>
        ///// Forces the cache to update
        ///// </summary>
        ///// <typeparam name="T">Entity type</typeparam>
        ///// <param name="cacheKey">Cache Key string</param>
        ///// <param name="getItemCallback">Delegate of the callback function</param>
        public void UpdateCache<T>(string cacheKey, Func<T> getItemCallback, int duration) where T : class
        {            
            int cacheDuration = 3;
            var item = getItemCallback();
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                   .SetSlidingExpiration(TimeSpan.FromMinutes(cacheDuration));
            memoryCache.Set(cacheKey, item, cacheEntryOptions);
        }
    }
}
