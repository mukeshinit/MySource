namespace EventfulWebApi.Cache
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Caching;   

    public class ApiCache
    {
        private static readonly ObjectCache Cache = MemoryCache.Default;

        public static T Get<T>(string key) where T : class
        {
            if (Cache.Contains(key))
            {
                return (T)Cache[key];
            }
            else
            {
                return null;
            }
        }

        public static object Get(string key)
        {
            if (Cache.Contains(key))
            {
                return Cache[key];
            }
            else
            {
                return null;
            }
        }

        public static void Add<T>(T objectToCache, string key, CacheItemPolicy policy) where T : class
        {
            //policy = new CacheItemPolicy();
            //policy.Priority = CacheItemPriority.Default;
            //policy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(60.00);

            Cache.Add(key, objectToCache, policy);
        }

        public static void Add(object objectToCache, string key, CacheItemPolicy policy)
        {
            Cache.Add(key, objectToCache, policy);
        }

        public static void Clear(string key)
        {
            Cache.Remove(key);
        }

        public static bool Exists(string key)
        {
            return Cache.Get(key) != null;
        }

        public static List<string> GetAll()
        {
            return Cache.Select(keyValuePair => keyValuePair.Key).ToList();
        }

        public static CacheItemPolicy GetCachePolicy(double expirationMinutes)
        {
            var cachePolicy = new CacheItemPolicy
            {
                Priority = CacheItemPriority.Default,
                SlidingExpiration = TimeSpan.FromMinutes(expirationMinutes)
            };
            return cachePolicy;
        }
    }
    public enum MyCachePriority
    {
        Default,
        NotRemovable
    }
}
