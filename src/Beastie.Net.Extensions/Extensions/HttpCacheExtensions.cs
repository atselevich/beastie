using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Caching;
using Beastie.Net.Extensions.Constants;

namespace Beastie.Net.Extensions.Extensions
{
    public static class HttpCacheExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// All cached items keys
        /// </summary>
        /// <param name="cache">
        /// The cache.
        /// </param>
        /// <returns>
        /// All cache keys
        /// </returns>
        public static IEnumerable<string> AllKeys(this Cache cache)
        {
            var keys = new List<string>();

            var cacheEnum = cache.GetEnumerator();
            while (cacheEnum.MoveNext())
            {
                keys.Add(cacheEnum.Key.ToString());
            }

            return keys.ToArray();
        }

        /// <summary>
        /// The cached method result.
        /// </summary>
        /// <param name="method">
        /// The method.
        /// </param>
        /// <param name="keyPrefix">
        /// The key prefix.
        /// </param>
        /// <param name="compoundKey">
        /// The compound key.
        /// </param>
        /// <param name="cacheTime">
        /// The cache time.
        /// </param>
        /// <param name="memberName">
        /// The member name.
        /// </param>
        /// <param name="filePath">
        /// The file path.
        /// </param>
        /// <param name="lineNumber">
        /// The line number.
        /// </param>
        /// <typeparam name="TObject">
        /// </typeparam>
        /// <returns>
        /// The TObject
        /// </returns>
        public static TObject CachedMethodResult<TObject>(
            Func<TObject> method,
            string keyPrefix,
            bool compoundKey = true,
            CachingTime cacheTime = CachingTime.BelowNormal,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0) where TObject : class
        {
            var result = default(TObject);

            var key = string.Empty;

            if (cacheTime > CachingTime.None && !Debugger.IsAttached)
            {
                key = compoundKey
                    ? string.Concat(
                        keyPrefix,
                        ".CacheMethodResult-",
                        method.GetMethodSignature(),
                        '-',
                        typeof(TObject),
                        '-',
                        memberName,
                        '-',
                        filePath,
                        '-',
                        lineNumber).ToLowerInvariant()
                    : keyPrefix;

                result = HttpRuntime.Cache[key] as TObject;
            }

            if (result == null)
            {
                result = method();

                if (cacheTime > CachingTime.None && !Debugger.IsAttached)
                {
                    result.CacheInsert(key, cacheTime);
                }
            }

            return result;
        }

        /// <summary>
        /// Caches the insert.
        /// </summary>
        /// <param name="cacheObject">
        /// The cache object.
        /// </param>
        /// <param name="key">
        /// The cache key.
        /// </param>
        /// <param name="time">
        /// The cache  time.
        /// </param>
        /// <param name="cacheDependency">
        /// The cache dependency.
        /// </param>
        public static void CacheInsert(
            this object cacheObject,
            string key,
            int time,
            CacheDependency cacheDependency = null)
        {
            Insert(HttpRuntime.Cache, key, cacheObject, time, cacheDependency);
        }

        /// <summary>
        /// Caches the insert.
        /// </summary>
        /// <param name="cacheObject">
        /// The cache object.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="time">
        /// The time.
        /// </param>
        /// <param name="cacheDependency">
        /// The cache dependency.
        /// </param>
        public static void CacheInsert(
            this ICollection<object> cacheObject,
            string key,
            int time,
            CacheDependency cacheDependency = null)
        {
            if (cacheObject != null && cacheObject.Count != 0)
            {
                Insert(HttpRuntime.Cache, key, cacheObject, time, cacheDependency);
            }
        }

        /// <summary>
        /// Caches the insert.
        /// </summary>
        /// <param name="cacheObject">
        /// The cache object.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="time">
        /// The time.
        /// </param>
        /// <param name="cacheDependency">
        /// The cache dependency.
        /// </param>
        public static void CacheInsert(
            this IList<object> cacheObject,
            string key,
            int time,
            CacheDependency cacheDependency = null)
        {
            if (cacheObject != null && cacheObject.Count != 0)
            {
                Insert(HttpRuntime.Cache, key, cacheObject, time, cacheDependency);
            }
        }

        /// <summary>
        /// Caches the insert.
        /// </summary>
        /// <param name="cacheObject">
        /// The cache object.
        /// </param>
        /// <param name="key">
        /// The cache key.
        /// </param>
        /// <param name="time">
        /// The cache  time.
        /// </param>
        /// <param name="cacheDependency">
        /// The cache dependency.
        /// </param>
        public static void CacheInsert(
            this object cacheObject,
            string key,
            CachingTime time = CachingTime.Normal,
            CacheDependency cacheDependency = null)
        {
            Insert(HttpRuntime.Cache, key, cacheObject, time, cacheDependency);
        }

        /// <summary>
        /// Caches the insert.
        /// </summary>
        /// <param name="cacheObject">
        /// The cache object.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="time">
        /// The time.
        /// </param>
        /// <param name="cacheDependency">
        /// The cache dependency.
        /// </param>
        public static void CacheInsert(
            this ICollection<object> cacheObject,
            string key,
            CachingTime time = CachingTime.Normal,
            CacheDependency cacheDependency = null)
        {
            if (cacheObject != null && cacheObject.Count != 0)
            {
                Insert(HttpRuntime.Cache, key, cacheObject, time, cacheDependency);
            }
        }

        /// <summary>
        /// Caches the insert.
        /// </summary>
        /// <param name="cacheObject">
        /// The cache object.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="time">
        /// The time.
        /// </param>
        /// <param name="cacheDependency">
        /// The cache dependency.
        /// </param>
        public static void CacheInsert(
            this IList<object> cacheObject,
            string key,
            CachingTime time = CachingTime.Normal,
            CacheDependency cacheDependency = null)
        {
            if (cacheObject != null && cacheObject.Count != 0)
            {
                Insert(HttpRuntime.Cache, key, cacheObject, time, cacheDependency);
            }
        }

        /// <summary>
        /// Clear All cached all items
        /// </summary>
        /// <param name="cache">
        /// The cache.
        /// </param>
        public static void Clear(this Cache cache)
        {
            var keys = new List<string>();
            var cacheEnum = cache.GetEnumerator();
            while (cacheEnum.MoveNext())
            {
                keys.Add(cacheEnum.Key.ToString());
            }

            foreach (var key in keys)
            {
                cache.Remove(key);
            }
        }

        /// <summary>
        /// Gets the specified object by key from cache.
        /// </summary>
        /// <typeparam name="TObject">
        /// The type of the object.
        /// </typeparam>
        /// <param name="cache">
        /// The cache instance .
        /// </param>
        /// <param name="key">
        /// The cache key.
        /// </param>
        /// <returns>
        /// The object instance or null
        /// </returns>
        public static TObject Get<TObject>(this Cache cache, string key) where TObject : class
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                return cache[key] as TObject;
            }

            return null;
        }

        /// <summary>
        /// Inserts the specified  object to the cache.
        /// </summary>
        /// <param name="cache">
        /// The cache.
        /// </param>
        /// <param name="key">
        /// The cache key.
        /// </param>
        /// <param name="cacheObject">
        /// The object to cache.
        /// </param>
        /// <param name="time">
        /// The cache time.
        /// </param>
        /// <param name="cacheDependency">
        /// The cache dependency.
        /// </param>
        public static void Insert(
            this Cache cache,
            string key,
            object cacheObject,
            CachingTime time = CachingTime.Normal,
            CacheDependency cacheDependency = null)
        {
            if (!string.IsNullOrWhiteSpace(key) && null != cacheObject)
            {
                cache.Insert(
                    key,
                    cacheObject,
                    cacheDependency,
                    DateTime.Now.AddMinutes((double)time),
                    Cache.NoSlidingExpiration);
            }
        }

        /// <summary>
        /// The insert.
        /// </summary>
        /// <param name="cache">
        /// The cache.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="cacheObject">
        /// The cache object.
        /// </param>
        /// <param name="time">
        /// The time.
        /// </param>
        /// <param name="cacheDependency">
        /// The cache dependency.
        /// </param>
        public static void Insert(
            this Cache cache,
            string key,
            object cacheObject,
            int time,
            CacheDependency cacheDependency = null)
        {
            if (!string.IsNullOrWhiteSpace(key) && null != cacheObject)
            {
                cache.Insert(
                    key,
                    cacheObject,
                    cacheDependency,
                    DateTime.Now.AddMinutes(time),
                    Cache.NoSlidingExpiration);
            }
        }

        /// <summary>
        /// Clear All cached all items with specific partial key within cache keys
        /// </summary>
        /// <param name="cache">
        /// The cache for a Web application
        /// </param>
        /// <param name="key">
        /// The partial Cache key
        /// </param>
        public static void RemoveKeysContaining(this Cache cache, string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                var keys = new List<string>();
                var cacheEnum = cache.GetEnumerator();
                while (cacheEnum.MoveNext())
                {
                    keys.Add(cacheEnum.Key.ToString());
                }

                foreach (var collectionKey in keys)
                {
                    if (collectionKey.IndexOf(key, StringComparison.OrdinalIgnoreCase) != -1)
                    {
                        cache.Remove(collectionKey);
                    }
                }
            }
        }

        #endregion
    }
}