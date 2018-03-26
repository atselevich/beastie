using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Beastie.Net.Extensions.Extensions
{
    public static class NameValueCollectionExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Takes readonly NameValueCollection, creates a new one based on original and adds Key Value. Used for chaining
        ///     extensions or getting a modified Request.QueryString.
        /// </summary>
        /// <param name="collection">
        ///     The collection.
        /// </param>
        /// <param name="key">
        ///     The key.
        /// </param>
        /// <param name="value">
        ///     The value.
        /// </param>
        /// <returns>
        ///     The <see cref="NameValueCollection" />.
        /// </returns>
        public static NameValueCollection AddKeyValueToQueryString(
            this NameValueCollection collection,
            string key,
            string value)
        {
            var newCollection = collection.ToQueryString().ParseAsQueryString();
            newCollection.Add(key, value);
            return newCollection;
        }

        /// <summary>
        ///     Gets an long from the specified key
        /// </summary>
        /// <param name="collection">
        ///     The NameValueCollection collection instance
        /// </param>
        /// <param name="key">
        ///     The NameValueCollection collection key
        /// </param>
        /// <param name="defaultValue">
        ///     The default value.
        /// </param>
        /// <returns>
        ///     The converted value or NULL
        /// </returns>
        public static long? GetAsLongNullable(
            this NameValueCollection collection,
            string key,
            long? defaultValue = null)
        {
            if (collection != null && !string.IsNullOrWhiteSpace(key))
            {
                return collection[key].ConvertToLongNullable(defaultValue);
            }

            return null;
        }

        /// <summary>
        ///     Gets a Boolean from the specified key
        /// </summary>
        /// <param name="collection">
        ///     The NameValueCollection collection instance
        /// </param>
        /// <param name="key">
        ///     The NameValueCollection collection key
        /// </param>
        /// <param name="defaultValue">
        ///     The default value.
        /// </param>
        /// <returns>
        ///     The converted value or NULL
        /// </returns>
        public static bool? GetBooleanNullable(
            this NameValueCollection collection,
            string key,
            bool? defaultValue = null)
        {
            if (collection != null && !string.IsNullOrWhiteSpace(key))
            {
                return collection[key].ConvertToBooleanNullable(defaultValue);
            }

            return null;
        }

        /// <summary>
        ///     Gets a double from the specified key
        /// </summary>
        /// <param name="collection">
        ///     The NameValueCollection collection instance
        /// </param>
        /// <param name="key">
        ///     The NameValueCollection collection key
        /// </param>
        /// <param name="defaultValue">
        ///     The default value.
        /// </param>
        /// <returns>
        ///     The converted value or NULL
        /// </returns>
        public static double? GetDouble(this NameValueCollection collection, string key, double? defaultValue = null)
        {
            if (collection != null && !string.IsNullOrWhiteSpace(key))
            {
                return collection[key].ConvertToDoubleNullable(defaultValue);
            }

            return null;
        }

        /// <summary>
        ///     Gets the first or default key containing partial key.
        /// </summary>
        /// <param name="collection">
        ///     The collection.
        /// </param>
        /// <param name="key">
        ///     The key to match.
        /// </param>
        /// <returns>
        ///     The value containing key or null
        /// </returns>
        public static string GetFirstOrDefaultPartialKey(this NameValueCollection collection, string key)
        {
            return collection.GetKeysContainingPartialKey(key).FirstOrDefault();
        }

        /// <summary>
        ///     Gets a float from the specified key
        /// </summary>
        /// <param name="collection">
        ///     The NameValueCollection collection instance
        /// </param>
        /// <param name="key">
        ///     The NameValueCollection collection key
        /// </param>
        /// <param name="defaultValue">
        ///     The default value.
        /// </param>
        /// <returns>
        ///     The converted value or NULL
        /// </returns>
        public static float? GetFloat(this NameValueCollection collection, string key, float? defaultValue = null)
        {
            if (collection != null && !string.IsNullOrWhiteSpace(key))
            {
                return collection[key].ConvertToFloatNullable(defaultValue);
            }

            return null;
        }

        /// <summary>
        ///     The get int.
        /// </summary>
        /// <param name="collection">
        ///     The collection.
        /// </param>
        /// <param name="key">
        ///     The key.
        /// </param>
        /// <param name="defaultValue">
        ///     The default value.
        /// </param>
        /// <returns>
        ///     The <see cref="int" />.
        /// </returns>
        public static int GetInt(this NameValueCollection collection, string key, int defaultValue = 0)
        {
            if (collection != null && !string.IsNullOrWhiteSpace(key))
            {
                return collection[key].ConvertToInt(defaultValue);
            }

            return defaultValue;
        }

        /// <summary>
        ///     Gets an int from the specified key
        /// </summary>
        /// <param name="collection">
        ///     The NameValueCollection collection instance
        /// </param>
        /// <param name="key">
        ///     The NameValueCollection collection key
        /// </param>
        /// <param name="defaultValue">
        ///     The default value.
        /// </param>
        /// <returns>
        ///     The converted value or NULL
        /// </returns>
        public static int? GetIntNullable(this NameValueCollection collection, string key, int? defaultValue = null)
        {
            if (collection != null && !string.IsNullOrWhiteSpace(key))
            {
                return collection[key].ConvertToIntNullable(defaultValue);
            }

            return null;
        }

        /// <summary>
        ///     Gets the keys containing partial key.
        /// </summary>
        /// <param name="collection">
        ///     The collection.
        /// </param>
        /// <param name="key">
        ///     The key to match.
        /// </param>
        /// <returns>
        ///     The IEnumerable of strings matching key
        /// </returns>
        public static IEnumerable<string> GetKeysContainingPartialKey(this NameValueCollection collection, string key)
        {
            if (collection != null && !string.IsNullOrEmpty(key))
            {
                key = key.ToLowerInvariant();
                IEnumerable<string> keys = collection.AllKeys;

                return from value in keys
                    where !string.IsNullOrEmpty(value) && value.ToLowerInvariant().Contains(key)
                    select value;
            }

            return new string[] { };
        }

        public static bool HasValues(this NameValueCollection nameValueCollection)
        {
            foreach (string key in nameValueCollection)
            {
                if (nameValueCollection[key].IsNotNullOrEmpty())
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///     Takes readonly NameValueCollection, creates a new one based on original and removes Key. Used for chaining
        ///     extensions or getting a modified Request.QueryString.
        /// </summary>
        /// <param name="collection">
        ///     The collection.
        /// </param>
        /// <param name="key">
        ///     The key.
        /// </param>
        /// <returns>
        ///     The <see cref="NameValueCollection" />.
        /// </returns>
        public static NameValueCollection RemoveKeyFromQueryString(this NameValueCollection collection, string key)
        {
            var newCollection = collection.ToQueryString().ParseAsQueryString();
            newCollection.Remove(key);
            return newCollection;
        }

        /// <summary>
        ///     Converts a collection to a NameValueCollection
        /// </summary>
        /// <typeparam name="TObject">
        ///     The type of the object.
        /// </typeparam>
        /// <param name="collection">
        ///     The collection.
        /// </param>
        /// <param name="nameSelector">
        ///     The name selector.
        /// </param>
        /// <param name="valueSelector">
        ///     The value selector.
        /// </param>
        /// <param name="excludeNullOrEmptyValues">
        ///     if set to <c>true</c> [exclude null or empty values].
        /// </param>
        /// <returns>
        ///     The name value collection
        /// </returns>
        public static NameValueCollection ToNameValueCollection<TObject>(
            this IEnumerable<TObject> collection,
            Func<TObject, string> nameSelector,
            Func<TObject, string> valueSelector,
            bool excludeNullOrEmptyValues = true)
        {
            if (collection != null)
            {
                var arr = collection.ToArray();
                var nameValueCollection =
                    new NameValueCollection(arr.Length, StringComparer.InvariantCultureIgnoreCase);

                foreach (var obj in arr)
                {
                    var name = nameSelector(obj);
                    if (!string.IsNullOrEmpty(name))
                    {
                        if (excludeNullOrEmptyValues)
                        {
                            var value = valueSelector(obj);
                            if (!string.IsNullOrEmpty(value))
                            {
                                nameValueCollection[name] = value;
                            }
                        }
                        else
                        {
                            nameValueCollection[name] = valueSelector(obj);
                        }
                    }
                }

                return nameValueCollection;
            }

            return new NameValueCollection();
        }

        /// <summary>
        ///     The current MNameValueCollection to the query string.
        /// </summary>
        /// <param name="collection">
        ///     The collection.
        /// </param>
        /// <param name="prependQuestionMark">
        ///     if set to <c>true</c> prepend question marks.
        /// </param>
        /// <returns>
        ///     The current collection to the query string
        /// </returns>
        public static string ToQueryString(this NameValueCollection collection, bool prependQuestionMark = false)
        {
            var queryString = new StringBuilder(64);
            if (collection != null)
            {
                var count = collection.Count;
                for (var i = 0; i < count; i++)
                {
                    if (prependQuestionMark && i == 0)
                    {
                        queryString.Append('?');
                    }

                    var key = collection.GetKey(i);
                    var value = collection[key];

                    if (!string.IsNullOrEmpty(value))
                    {
                        queryString.UrlEncode(key).Append('=').UrlEncode(value);
                    }

                    if (i != count - 1)
                    {
                        queryString.Append('&');
                    }
                }
            }

            return queryString.ToString();
        }

        #endregion
    }
}