using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Beastie.Net.Extensions.Extensions
{
    public static class DictionaryExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The get property value.
        /// </summary>
        /// <param name="properties">
        ///     The properties.
        /// </param>
        /// <param name="propertyName">
        ///     The property name.
        /// </param>
        /// <typeparam name="T">
        ///     The object
        /// </typeparam>
        /// <returns>
        ///     The T
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if argument is null
        /// </exception>
        public static T GetPropertyValue<T>(
            this Dictionary<string, object> properties,
            [CallerMemberName] string propertyName = null)
        {
            if (propertyName == null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            object value;
            if (properties.TryGetValue(propertyName, out value))
            {
                return (T) value;
            }

            return default(T);
        }

        /// <summary>
        ///     The set property value.
        /// </summary>
        /// <param name="properties">
        ///     The properties.
        /// </param>
        /// <param name="newValue">
        ///     The new value.
        /// </param>
        /// <param name="propertyName">
        ///     The property name.
        /// </param>
        /// <typeparam name="T">
        ///     The object
        /// </typeparam>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if argument is null
        /// </exception>
        /// <returns>
        ///     The T
        /// </returns>
        public static T SetPropertyValue<T>(
            this Dictionary<string, object> properties,
            T newValue,
            [CallerMemberName] string propertyName = null)
        {
            if (propertyName == null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            if (!EqualityComparer<T>.Default.Equals(newValue, properties.GetPropertyValue<T>(propertyName)))
            {
                properties[propertyName] = newValue;
            }

            return newValue;
        }

        /// <summary>
        ///     Converts the IDictionary to a query string
        /// </summary>
        /// <param name="collection">
        ///     The collection.
        /// </param>
        /// <param name="prependQuestionMark">
        ///     if set to <c>true</c> prepends question mark '?'.
        /// </param>
        /// <returns>
        ///     The IDictionary expressed as a string
        /// </returns>
        public static string ToQueryString(
            this IDictionary<string, string> collection,
            bool prependQuestionMark = false)
        {
            if (collection != null && collection.Count > 0)
            {
                var queryBuilder = new StringBuilder(128);

                if (prependQuestionMark)
                {
                    queryBuilder.Append('?');
                }

                var keys = collection.Keys.ToArray();

                for (var i = 0; i < keys.Length; i++)
                {
                    if (i > 0)
                    {
                        queryBuilder.Append('&');
                    }

                    var key = keys[i];
                    queryBuilder.UrlEncode(key).Append('=').UrlEncode(collection[key]);
                }

                return queryBuilder.ToString();
            }

            return string.Empty;
        }

        /// <summary>
        ///     Converts the IDictionary to a query string
        /// </summary>
        /// <param name="collection">
        ///     The collection.
        /// </param>
        /// <param name="prependQuestionMark">
        ///     if set to <c>true</c> prepends question mark '?'.
        /// </param>
        /// <returns>
        ///     The IDictionary expressed as a string
        /// </returns>
        public static string ToQueryString(
            this IDictionary<string, object> collection,
            bool prependQuestionMark = false)
        {
            if (collection != null && collection.Count > 0)
            {
                var queryBuilder = new StringBuilder(128);

                if (prependQuestionMark)
                {
                    queryBuilder.Append('?');
                }

                var keys = collection.Keys.ToArray();

                for (var i = 0; i < keys.Length; i++)
                {
                    if (i > 0)
                    {
                        queryBuilder.Append('&');
                    }

                    var key = keys[i];
                    queryBuilder.UrlEncode(key).Append('=').UrlEncode(string.Concat(collection[key]));
                }

                return queryBuilder.ToString();
            }

            return string.Empty;
        }

        #endregion
    }
}