using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Web;

namespace Beastie.Net.Extensions.Extensions
{
    public static class HttpUtilityExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Minimally converts a string to an HTML-encoded string.
        /// </summary>
        /// <param name="input">
        ///     The string input to encode.
        /// </param>
        /// <returns>
        ///     An encoded string.
        /// </returns>
        [DebuggerHidden]
        public static string HtmlAttributeEncode(this string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                return HttpUtility.HtmlAttributeEncode(input);
            }

            return string.Empty;
        }

        /// <summary>
        ///     Converts a string that has been HTML-encoded for HTTP transmission into a decoded string.
        /// </summary>
        /// <param name="input">
        ///     The string input to decode
        /// </param>
        /// <returns>
        ///     A decoded string.
        /// </returns>
        [DebuggerHidden]
        public static string HtmlDecode(this string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                return HttpUtility.HtmlDecode(input);
            }

            return input;
        }

        /// <summary>
        ///     Parses a query string into a System.Collections.Specialized.NameValueCollection using System.Text.Encoding.UTF8
        ///     encoding.
        /// </summary>
        /// <param name="query">
        ///     The query string to parse.
        /// </param>
        /// <returns>
        ///     A System.Collections.Specialized.NameValueCollection of query parameters and values.
        /// </returns>
        [DebuggerHidden]
        public static NameValueCollection ParseAsQueryString(this string query)
        {
            if (!string.IsNullOrWhiteSpace(query))
            {
                return HttpUtility.ParseQueryString(query);
            }

            return new NameValueCollection(StringComparer.InvariantCultureIgnoreCase);
        }

        /// <summary>
        ///     Parses a query string into a System.Collections.Specialized.NameValueCollection using the specified
        ///     System.Text.Encoding.
        /// </summary>
        /// <param name="query">
        ///     The query string to parse.
        /// </param>
        /// <param name="encoding">
        ///     The System.Text.Encoding to use.
        /// </param>
        /// <returns>
        ///     A System.Collections.Specialized.NameValueCollection of query parameters and values.
        /// </returns>
        [DebuggerHidden]
        public static NameValueCollection ParseAsQueryString(this string query, Encoding encoding)
        {
            if (!string.IsNullOrWhiteSpace(query))
            {
                return HttpUtility.ParseQueryString(query, encoding);
            }

            return null;
        }

        /// <summary>
        ///     Converts a string that has been encoded for transmission in a URL into a decoded string.
        /// </summary>
        /// <param name="input">
        ///     The string input to decode.
        /// </param>
        /// <returns>
        ///     A decoded string.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings",
            Justification = "The string representation of the Uri")]
        [DebuggerHidden]
        public static string UrlDecode(this string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                return HttpUtility.UrlDecode(input);
            }

            return string.Empty;
        }

        /// <summary>
        ///     Converts a URL-encoded byte array into a decoded string using the specified decoding object.
        /// </summary>
        /// <param name="bytes">
        ///     The array of bytes to decode.
        /// </param>
        /// <param name="encoding">
        ///     The System.Text.Encoding that specifies the decoding scheme.
        /// </param>
        /// <returns>
        ///     A decoded string.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings",
            Justification = "The string representation of the Uri")]
        [DebuggerHidden]
        public static string UrlDecode(this byte[] bytes, Encoding encoding)
        {
            if (bytes != null)
            {
                return HttpUtility.UrlDecode(bytes, encoding);
            }

            return string.Empty;
        }

        /// <summary>
        ///     Converts a URL-encoded string into a decoded string, using the specified encoding object.
        /// </summary>
        /// <param name="input">
        ///     The input.
        /// </param>
        /// <param name="encoding">
        ///     The System.Text.Encoding that specifies the decoding scheme.
        /// </param>
        /// <returns>
        ///     A decoded string.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings",
            Justification = "The string representation of the Uri")]
        [DebuggerHidden]
        public static string UrlDecode(this string input, Encoding encoding)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                return HttpUtility.UrlDecode(input, encoding);
            }

            return string.Empty;
        }

        /// <summary>
        ///     Converts a URL-encoded byte array into a decoded string using the specified
        ///     encoding object, starting at the specified position in the array, and continuing
        ///     for the specified number of bytes.
        /// </summary>
        /// <param name="bytes">
        ///     The array of bytes to decode.
        /// </param>
        /// <param name="offset">
        ///     The position in the byte to begin decoding.
        /// </param>
        /// <param name="count">
        ///     The number of bytes to decode.
        /// </param>
        /// <param name="encoding">
        ///     The System.Text.Encoding that specifies the decoding scheme.
        /// </param>
        /// <returns>
        ///     A decoded string.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings",
            Justification = "The string representation of the Uri")]
        [DebuggerHidden]
        public static string UrlDecode(this byte[] bytes, int offset, int count, Encoding encoding)
        {
            if (bytes != null)
            {
                return HttpUtility.UrlDecode(bytes, offset, count, encoding);
            }

            return string.Empty;
        }

        /// <summary>
        ///     Converts a URL-encoded array of bytes into a decoded array of bytes.
        /// </summary>
        /// <param name="bytes">
        ///     The array of bytes to decode.
        /// </param>
        /// <returns>
        ///     A decoded array of bytes.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings",
            Justification = "The string representation of the Uri")]
        [DebuggerHidden]
        public static byte[] UrlDecodeToBytes(this byte[] bytes)
        {
            if (bytes != null)
            {
                return HttpUtility.UrlDecodeToBytes(bytes);
            }

            return null;
        }

        /// <summary>
        ///     Converts a URL-encoded string into a decoded array of bytes.
        /// </summary>
        /// <param name="input">
        ///     The string input to decode.
        /// </param>
        /// <returns>
        ///     A decoded array of bytes.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings",
            Justification = "The string representation of the Uri")]
        [DebuggerHidden]
        public static byte[] UrlDecodeToBytes(this string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                return HttpUtility.UrlDecodeToBytes(input);
            }

            return null;
        }

        /// <summary>
        ///     Converts a URL-encoded string into a decoded array of bytes using the specified
        ///     decoding object.
        /// </summary>
        /// <param name="input">
        ///     The string input to decode.
        /// </param>
        /// <param name="encoding">
        ///     The System.Text.Encoding that specifies the decoding scheme.
        /// </param>
        /// <returns>
        ///     A decoded array of bytes.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings",
            Justification = "The string representation of the Uri")]
        [DebuggerHidden]
        public static byte[] UrlDecodeToBytes(this string input, Encoding encoding)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                return HttpUtility.UrlDecodeToBytes(input, encoding);
            }

            return null;
        }

        /// <summary>
        ///     Converts a URL-encoded array of bytes into a decoded array of bytes, starting
        ///     at the specified position in the array and continuing for the specified number
        ///     of bytes.
        /// </summary>
        /// <param name="bytes">
        ///     The array of bytes to decode.
        /// </param>
        /// <param name="offset">
        ///     The position in the byte array at which to begin decoding.
        /// </param>
        /// <param name="count">
        ///     The number of bytes to decode.
        /// </param>
        /// <returns>
        ///     A decoded array of bytes.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings",
            Justification = "The string representation of the Uri")]
        [DebuggerHidden]
        public static byte[] UrlDecodeToBytes(this byte[] bytes, int offset, int count)
        {
            if (bytes != null)
            {
                return HttpUtility.UrlDecodeToBytes(bytes, offset, count);
            }

            return null;
        }

        /// <summary>
        ///     Converts a byte array into an encoded URL string.
        /// </summary>
        /// <param name="bytes">
        ///     The array of bytes to encode.
        /// </param>
        /// <returns>
        ///     An encoded string.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings",
            Justification = "The string representation of the Uri")]
        [DebuggerHidden]
        public static string UrlEncode(this byte[] bytes)
        {
            if (bytes != null)
            {
                return HttpUtility.UrlEncode(bytes);
            }

            return string.Empty;
        }

        /// <summary>
        ///     Encodes a URL string using the specified encoding object.
        /// </summary>
        /// <param name="input">
        ///     The text input to encode.
        /// </param>
        /// <param name="encoding">
        ///     The System.Text.Encoding that specifies the decoding scheme.
        /// </param>
        /// <returns>
        ///     An encoded string.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings",
            Justification = "The string representation of the Uri")]
        [DebuggerHidden]
        public static string UrlEncode(this string input, Encoding encoding)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                return HttpUtility.UrlEncode(input, encoding);
            }

            return string.Empty;
        }

        /// <summary>
        ///     Converts a byte array into a URL-encoded string, starting at the specified
        ///     position in the array and continuing for the specified number of bytes.
        /// </summary>
        /// <param name="bytes">
        ///     The array of bytes to encode.
        /// </param>
        /// <param name="offset">
        ///     The position in the byte array at which to begin encoding.
        /// </param>
        /// <param name="count">
        ///     The number of bytes to encode.
        /// </param>
        /// <returns>
        ///     An encoded string.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings",
            Justification = "The string representation of the Uri")]
        [DebuggerHidden]
        public static string UrlEncode(this byte[] bytes, int offset, int count)
        {
            if (bytes != null)
            {
                return HttpUtility.UrlEncode(bytes, offset, count);
            }

            return string.Empty;
        }

        /// <summary>
        ///     Converts an array of bytes into a URL-encoded array of bytes.
        /// </summary>
        /// <param name="bytes">
        ///     The array of bytes to encode.
        /// </param>
        /// <returns>
        ///     An encoded array of bytes.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings",
            Justification = "The string representation of the Uri")]
        [DebuggerHidden]
        public static byte[] UrlEncodeToBytes(this byte[] bytes)
        {
            if (bytes != null)
            {
                return HttpUtility.UrlEncodeToBytes(bytes);
            }

            return null;
        }

        /// <summary>
        ///     Converts a string into a URL-encoded array of bytes.
        /// </summary>
        /// <param name="input">
        ///     The string input to encode.
        /// </param>
        /// <returns>
        ///     An encoded array of bytes.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings",
            Justification = "The string representation of the Uri")]
        [DebuggerHidden]
        public static byte[] UrlEncodeToBytes(this string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                return HttpUtility.UrlEncodeToBytes(input);
            }

            return null;
        }

        /// <summary>
        ///     Converts a string into a URL-encoded array of bytes using the specified encoding
        ///     object.
        /// </summary>
        /// <param name="input">
        ///     The string input to encode.
        /// </param>
        /// <param name="encoding">
        ///     The System.Text.Encoding that specifies the decoding scheme.
        /// </param>
        /// <returns>
        ///     An encoded array of bytes.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings",
            Justification = "The string representation of the Uri")]
        [DebuggerHidden]
        public static byte[] UrlEncodeToBytes(this string input, Encoding encoding)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                return HttpUtility.UrlEncodeToBytes(input, encoding);
            }

            return null;
        }

        /// <summary>
        ///     Converts an array of bytes into a URL-encoded array of bytes, starting at
        ///     the specified position in the array and continuing for the specified number
        ///     of bytes.
        /// </summary>
        /// <param name="bytes">
        ///     The array of bytes to encode.
        /// </param>
        /// <param name="offset">
        ///     The position in the byte array at which to begin encoding.
        /// </param>
        /// <param name="count">
        ///     The number of bytes to encode.
        /// </param>
        /// <returns>
        ///     An encoded array of bytes.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings",
            Justification = "The string representation of the Uri")]
        [DebuggerHidden]
        public static byte[] UrlEncodeToBytes(this byte[] bytes, int offset, int count)
        {
            if (bytes != null)
            {
                return HttpUtility.UrlEncodeToBytes(bytes, offset, count);
            }

            return null;
        }

        /// <summary>
        ///     Converts a string into a Unicode string.
        /// </summary>
        /// <param name="input">
        ///     The string input to convert.
        /// </param>
        /// <returns>
        ///     A Unicode string in Unicode Value notation.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings",
            Justification = "The string representation of the Uri")]
        [DebuggerHidden]
        public static string UrlEncodeUnicode(this string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                return HttpUtility.UrlEncode(input);
            }

            return string.Empty;
        }

        /// <summary>
        ///     Converts a Unicode string into an array of bytes.
        /// </summary>
        /// <param name="input">
        ///     The string input to convert.
        /// </param>
        /// <returns>
        ///     A byte array.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings",
            Justification = "The string representation of the Uri")]
        [DebuggerHidden]
        public static byte[] UrlEncodeUnicodeToBytes(this string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                return HttpUtility.UrlEncodeToBytes(input);
            }

            return null;
        }

        /// <summary>
        ///     Encodes the path portion of a URL string for reliable HTTP transmission from
        ///     the Web server to a client.
        /// </summary>
        /// <param name="input">
        ///     The text input to URL-encode.
        /// </param>
        /// <returns>
        ///     The URL-encoded text.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings",
            Justification = "The string representation of the Uri")]
        [DebuggerHidden]
        public static string UrlPathEncode(this string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                return HttpUtility.UrlPathEncode(input);
            }

            return string.Empty;
        }

        #endregion
    }
}