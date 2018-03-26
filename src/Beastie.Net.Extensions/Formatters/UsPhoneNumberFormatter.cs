using System;
using System.Globalization;
using System.Text;
using Beastie.Net.Extensions.Extensions;

namespace Beastie.Net.Extensions.Formatters
{
    public sealed class UsPhoneNumberFormatter : IFormatProvider, ICustomFormatter
    {
        #region Methods

        private string HandleOtherFormats(string format, object args)
        {
            if (args is IFormattable) return ((IFormattable) args).ToString(format, CultureInfo.CurrentCulture);

            if (args != null) return args.ToString();

            return string.Empty;
        }

        #endregion

        /// <summary>
        ///     US Phone Number Formats
        /// </summary>
        public static class Formats
        {
            #region Constants

            /// <summary>
            ///     Dashed Format 555-555-5555 x0000
            /// </summary>
            public const string Dashed = "{0:D}";

            /// <summary>
            ///     Dotted Format 555.555.5555 x0000
            /// </summary>
            public const string Dotted = "{0:d}";

            /// <summary>
            ///     Parentheses and Dashed Format (555) 555-5555 x0000
            /// </summary>
            public const string ParenthesisDash = "{0:P}";

            #endregion
        }

        #region Public Methods and Operators

        /// <summary>
        ///     Converts the value of a specified object to an equivalent string representation using specified format and
        ///     culture-specific formatting information.
        /// </summary>
        /// <param name="format">
        ///     A format string containing formatting specifications.
        /// </param>
        /// <param name="args">
        ///     An object to format.
        /// </param>
        /// <param name="formatProvider">
        ///     An object that supplies format information about the current instance.
        /// </param>
        /// <returns>
        ///     The string representation of the value of <paramref name="args" />, formatted as specified by
        ///     <paramref name="format" /> and <paramref name="formatProvider" />.
        /// </returns>
        public string Format(string format, object args, IFormatProvider formatProvider)
        {
            /* d: 555.555.5555 x0000
             * D: 555-555-5555 x0000
             * P: (555) 555-5555 x0000
             * */
            var result = string.Concat(args).RemoveNonDigitChars();
            if (!string.IsNullOrWhiteSpace(result) && result.Length >= 10 && !result.StartsWith("1"))
            {
                var sb = new StringBuilder(21);

                switch (format)
                {
                    case "d":
                        sb.Append(result, 0, 3).Append('.').Append(result, 3, 3).Append('.').Append(result, 6, 4);
                        break;
                    case "D":
                        sb.Append(result, 0, 3).Append('-').Append(result, 3, 3).Append('-').Append(result, 6, 4);
                        break;
                    case "P":
                        sb.Append('(')
                            .Append(result, 0, 3)
                            .Append(") ")
                            .Append(result, 3, 3)
                            .Append('-')
                            .Append(result, 6, 4);
                        break;
                    default:
                        return HandleOtherFormats(format, args);
                }

                if (result.Length > 10) sb.Append(" x").Append(result.Substring(10));

                return sb.ToString();
            }

            return string.Empty;
        }

        /// <summary>
        ///     Returns an object that provides formatting services for the specified type.
        /// </summary>
        /// <param name="formatType">
        ///     An object that specifies the type of format object to return.
        /// </param>
        /// <returns>
        ///     An instance of the object specified by <paramref name="formatType" />, if the
        ///     <see cref="T:System.IFormatProvider" /> implementation can supply that type of object; otherwise, null.
        /// </returns>
        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter)) return this;

            return null;
        }

        #endregion
    }
}