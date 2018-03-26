using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Beastie.Net.Extensions.Extensions
{
    public static class LinqExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Compacts the specified source by removing any null items.
        /// </summary>
        /// <typeparam name="T">
        ///     The type
        /// </typeparam>
        /// <param name="source">
        ///     The source.
        /// </param>
        /// <returns>
        ///     The source enumerable without null items
        /// </returns>
        public static IEnumerable<T> Compact<T>(this IEnumerable<T> source) where T : class
        {
            if (source != null)
            {
                foreach (var item in source)
                {
                    if (item != null)
                    {
                        yield return item;
                    }
                }
            }
        }

        /// <summary>
        ///     Selects Distinct By Property
        /// </summary>
        /// <param name="source">
        ///     The source.
        /// </param>
        /// <param name="keySelector">
        ///     The key selector.
        /// </param>
        /// <typeparam name="TSource">
        ///     the source
        /// </typeparam>
        /// <typeparam name="TKey">
        ///     the key
        /// </typeparam>
        /// <returns>
        ///     IEnumerable of distinct by items
        /// </returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            return source.DistinctBy(keySelector, null);
        }

        /// <summary>
        ///     The distinct by.
        /// </summary>
        /// <param name="source">
        ///     The source.
        /// </param>
        /// <param name="keySelector">
        ///     The key selector.
        /// </param>
        /// <param name="comparer">
        ///     The comparer.
        /// </param>
        /// <typeparam name="TSource">
        /// </typeparam>
        /// <typeparam name="TKey">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="IEnumerable" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey> comparer)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector == null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            var knownKeys = new HashSet<TKey>(comparer);
            return source.Where(element => knownKeys.Add(keySelector(element)));
        }

        /// <summary>
        ///     The is not null or empty.
        /// </summary>
        /// <param name="collection">
        ///     The collection.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsNotNullOrEmpty(this IEnumerable collection)
        {
            return collection != null && collection.GetEnumerator().MoveNext();
        }

        /// <summary>
        ///     The is not null or empty.
        /// </summary>
        /// <param name="collection">
        ///     The collection.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection != null && collection.GetEnumerator().MoveNext();
        }

        /// <summary>
        ///     Determines whether [is null or empty] [the specified collection].
        /// </summary>
        /// <param name="collection">
        ///     The collection.
        /// </param>
        /// <returns>
        ///     <c>true</c> if [is null or empty] [the specified collection]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmpty(this IEnumerable collection)
        {
            return collection == null || collection.GetEnumerator().MoveNext();
        }

        /// <summary>
        ///     Determines whether [is null or empty] [the specified collection].
        /// </summary>
        /// <typeparam name="T">
        ///     The Generic Type
        /// </typeparam>
        /// <param name="collection">
        ///     The collection.
        /// </param>
        /// <returns>
        ///     <c>true</c> if [is null or empty] [the specified collection]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            if (collection == null)
            {
                return true;
            }

            using (var enumerator = collection.GetEnumerator())
            {
                return !enumerator.MoveNext();
            }
        }

        /// <summary>
        ///     Pages the specified source.
        /// </summary>
        /// <typeparam name="TSource">
        ///     The type of the source.
        /// </typeparam>
        /// <param name="source">
        ///     The source.
        /// </param>
        /// <param name="pageIndex">
        ///     The page.
        /// </param>
        /// <param name="pageSize">
        ///     The page size.
        /// </param>
        /// <returns>
        ///     The Page collection records
        /// </returns>
        /// <remarks>
        ///     This extension method is design for relatively small collections ~2550 size with safety in mind. What's why
        ///     page index and page size are bytes[0-255]
        /// </remarks>
        public static IQueryable<TSource> Page<TSource>(
            this IQueryable<TSource> source,
            byte pageIndex,
            byte pageSize = 10)
        {
            if (source != null && pageIndex > 0 && pageSize > 0)
            {
                return source.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }

            return source;
        }

        /// <summary>
        ///     Pages the specified source.
        /// </summary>
        /// <typeparam name="TSource">
        ///     The type of the source.
        /// </typeparam>
        /// <param name="source">
        ///     The source.
        /// </param>
        /// <param name="pageIndex">
        ///     The page  index.
        /// </param>
        /// <param name="pageSize">
        ///     The page size.
        /// </param>
        /// <returns>
        ///     The Page collection records
        /// </returns>
        /// <remarks>
        ///     This extension method is design for relatively small collections ~2550 size with safety in mind. What's why
        ///     page index and page size are bytes[0-255]
        /// </remarks>
        public static IEnumerable<TSource> Page<TSource>(
            this IEnumerable<TSource> source,
            int pageIndex,
            int pageSize = 10)
        {
            if (source != null && pageIndex > 0 && pageSize > 0)
            {
                return source.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }

            return source;
        }

        /// <summary>
        ///     Paginates the specified <![CDATA[IQueryable<T>]]> collection  using LINQ query.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the collection item
        /// </typeparam>
        /// <param name="source">
        ///     The query source.
        /// </param>
        /// <param name="skip">
        ///     The skip items number.
        /// </param>
        /// <param name="take">
        ///     The take items number.
        /// </param>
        /// <returns>
        ///     The paged collection items
        /// </returns>
        public static IQueryable<T> Paginate<T>(this IQueryable<T> source, byte skip, byte take = 10)
        {
            if (source != null && skip > 0 && take > 0)
            {
                return source.Skip(skip).Take(take);
            }

            return source;
        }

        /// <summary>
        ///     Random item from the specified source.
        /// </summary>
        /// <typeparam name="T">
        ///     The object instance
        /// </typeparam>
        /// <param name="source">
        ///     The source.
        /// </param>
        /// <returns>
        ///     The random item from source
        /// </returns>
        public static T Random<T>(this IEnumerable<T> source)
        {
            if (source != null)
            {
                return source.Randomize().FirstOrDefault();
            }

            return default(T);
        }

        /// <summary>
        ///     Randomizes the specified source.
        /// </summary>
        /// <typeparam name="T">
        ///     The generic list object types
        /// </typeparam>
        /// <param name="source">
        ///     The source.
        /// </param>
        /// <returns>
        ///     The randomized list
        /// </returns>
        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source)
        {
            if (source != null)
            {
                return source.ToArray().OrderBy(item => Guid.NewGuid());
            }

            return Enumerable.Empty<T>();
        }

        /// <summary>
        ///     Randomizes the specified source.
        /// </summary>
        /// <typeparam name="T">
        ///     The generic list object types
        /// </typeparam>
        /// <param name="source">
        ///     The source.
        /// </param>
        /// <param name="take">
        ///     The take.
        /// </param>
        /// <returns>
        ///     The randomized list
        /// </returns>
        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source, byte take)
        {
            if (source != null)
            {
                return source.Randomize().Take(take);
            }

            return Enumerable.Empty<T>();
        }

        #endregion
    }
}