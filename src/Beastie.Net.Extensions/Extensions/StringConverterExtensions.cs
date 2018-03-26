using System;
using System.Globalization;

namespace Beastie.Net.Extensions.Extensions
{
    public static class StringConverterExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Converts as boolean.
        /// </summary>
        /// <param name="input">
        ///     The input.
        /// </param>
        /// <param name="defaultValue">
        ///     if set to <c>true</c> [default value].
        /// </param>
        /// <returns>
        ///     The bool value of string or default bool value
        /// </returns>
        public static bool ConvertToBoolean(this string input, bool defaultValue = default(bool))
        {
            if (!string.IsNullOrEmpty(input))
            {
                return string.Equals(input, bool.TrueString, StringComparison.OrdinalIgnoreCase) /* Standard */
                       || string.Equals(input, "1", StringComparison.OrdinalIgnoreCase) /* Old and third party */
                       || string.Equals(input, "on", StringComparison.OrdinalIgnoreCase); /* Html string*/
            }

            return defaultValue;
        }

        /// <summary>
        ///     String conversion to typeof Nullable(Boolean) utility
        /// </summary>
        /// <param name="input">
        ///     String version of the object
        /// </param>
        /// <param name="defaultValue">
        ///     The default value.
        /// </param>
        /// <returns>
        ///     string param as Nullable Boolean
        /// </returns>
        /// <example>
        ///     View code: <br />
        ///     bool? id = "true".ConvertToBooleanNullable();<br />
        /// </example>
        public static bool? ConvertToBooleanNullable(this string input, bool? defaultValue = null)
        {
            if (!string.IsNullOrEmpty(input))
            {
                return string.Equals(input, bool.TrueString, StringComparison.OrdinalIgnoreCase) /* Standard */
                       || string.Equals(input, "1", StringComparison.OrdinalIgnoreCase) /* Old and third party */
                       || string.Equals(input, "on", StringComparison.OrdinalIgnoreCase); /* Html string*/
            }

            return defaultValue;
        }

        /// <summary>
        ///     Converts as byte.
        /// </summary>
        /// <param name="input">
        ///     The input.
        /// </param>
        /// <param name="defaultValue">
        ///     The default value.
        /// </param>
        /// <returns>
        ///     The byte value of string or default byte value
        /// </returns>
        public static byte ConvertToByte(this string input, byte defaultValue = default(byte))
        {
            if (input.IsValidInteger())
            {
                var temp = Convert.ToInt64(input, CultureInfo.InvariantCulture);
                if (temp <= byte.MaxValue && temp >= byte.MinValue)
                {
                    return (byte) temp;
                }
            }

            return defaultValue;
        }

        /// <summary>
        ///     String conversion to typeof Nullable(Byte) utility
        /// </summary>
        /// <param name="input">
        ///     String version of the object
        /// </param>
        /// <param name="defaultValue">
        ///     The default value.
        /// </param>
        /// <returns>
        ///     string param as Nullable Byte
        /// </returns>
        /// <example>
        ///     View code: <br />
        ///     byte? id = "245".ConvertToByteNullable();<br />
        /// </example>
        public static byte? ConvertToByteNullable(this string input, byte? defaultValue = null)
        {
            if (input.IsValidInteger())
            {
                var temp = Convert.ToInt64(input, CultureInfo.InvariantCulture);
                if (temp <= byte.MaxValue && temp >= byte.MinValue)
                {
                    return (byte) temp;
                }
            }

            return defaultValue;
        }

        /// <summary>
        ///     Converts as date time.
        /// </summary>
        /// <param name="input">
        ///     The input.
        /// </param>
        /// <param name="defaultValue">
        ///     The default value.
        /// </param>
        /// <returns>
        ///     The DateTime value of string or default DateTime value
        /// </returns>
        public static DateTime ConvertToDateTime(this string input, DateTime defaultValue)
        {
            DateTime date;
            if (DateTime.TryParse(input, out date))
            {
                return date;
            }

            return defaultValue;
        }

        /// <summary>
        ///     String conversion to typeof Nullable(DateTime) utility
        /// </summary>
        /// <param name="input">
        ///     String version of the object
        /// </param>
        /// <param name="defaultValue">
        ///     The default value.
        /// </param>
        /// <returns>
        ///     string param as Nullable Decimal
        /// </returns>
        /// <example>
        ///     View code: <br />
        ///     DateTime? id = Extensions.ConvertToDateTimeNullable("07/30/1972");<br />
        /// </example>
        public static DateTime? ConvertToDateTimeNullable(this string input, DateTime? defaultValue = null)
        {
            DateTime date;
            if (DateTime.TryParse(input, out date))
            {
                return date;
            }

            return defaultValue;
        }

        /// <summary>
        ///     Converts as decimal.
        /// </summary>
        /// <param name="input">
        ///     The input.
        /// </param>
        /// <param name="defaultValue">
        ///     The default value.
        /// </param>
        /// <returns>
        ///     The decimal value of string or default decimal value
        /// </returns>
        public static decimal ConvertToDecimal(this string input, decimal defaultValue = default(decimal))
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                decimal result;
                if (decimal.TryParse(input, out result))
                {
                    return result;
                }
            }

            return defaultValue;
        }

        /// <summary>
        ///     String conversion to typeof Nullable(Decimal) utility
        /// </summary>
        /// <param name="input">
        ///     String version of the object
        /// </param>
        /// <param name="defaultValue">
        ///     The default value.
        /// </param>
        /// <returns>
        ///     string param as Nullable Decimal
        /// </returns>
        /// <example>
        ///     View code: <br />
        ///     decimal? id = "24536.365".ConvertToDecimalNullable();<br />
        /// </example>
        public static decimal? ConvertToDecimalNullable(this string input, decimal? defaultValue = null)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                decimal result;
                if (decimal.TryParse(input, out result))
                {
                    return result;
                }
            }

            return defaultValue;
        }

        /// <summary>
        ///     Converts as double.
        /// </summary>
        /// <param name="input">
        ///     The input.
        /// </param>
        /// <param name="defaultValue">
        ///     The default value.
        /// </param>
        /// <returns>
        ///     The double value of string or default double value
        /// </returns>
        public static double ConvertToDouble(this string input, double defaultValue = default(double))
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                double result;
                if (double.TryParse(input, out result))
                {
                    return result;
                }
            }

            return defaultValue;
        }

        /// <summary>
        ///     String conversion to typeof Nullable(Double) utility
        /// </summary>
        /// <param name="input">
        ///     String version of the object
        /// </param>
        /// <param name="defaultValue">
        ///     The default value.
        /// </param>
        /// <returns>
        ///     string param as Nullable Double
        /// </returns>
        /// <example>
        ///     View code: <br />
        ///     Double? id = "24536.365".ConvertToDoubleNullable();<br />
        /// </example>
        public static double? ConvertToDoubleNullable(this string input, double? defaultValue = null)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                double result;
                if (double.TryParse(input, out result))
                {
                    return result;
                }
            }

            return defaultValue;
        }

        /// <summary>
        ///     Converts as float.
        /// </summary>
        /// <param name="input">
        ///     The input.
        /// </param>
        /// <param name="defaultValue">
        ///     The default value.
        /// </param>
        /// <returns>
        ///     The float value of string or default float value
        /// </returns>
        public static float ConvertToFloat(this string input, float defaultValue = default(float))
        {
            if (!string.IsNullOrEmpty(input))
            {
                float result;
                if (float.TryParse(input, out result))
                {
                    return result;
                }
            }

            return defaultValue;
        }

        /// <summary>
        ///     String conversion to typeof Nullable(float) utility
        /// </summary>
        /// <param name="input">
        ///     String version of the object
        /// </param>
        /// <param name="defaultValue">
        ///     The default value.
        /// </param>
        /// <returns>
        ///     string param as Nullable float
        /// </returns>
        /// <example>
        ///     View code: <br />
        ///     float? id = "24536.365".ConvertToFloatNullable();<br />
        /// </example>
        public static float? ConvertToFloatNullable(this string input, float? defaultValue = null)
        {
            if (!string.IsNullOrEmpty(input))
            {
                float result;
                if (float.TryParse(input, out result))
                {
                    return result;
                }
            }

            return defaultValue;
        }

        /// <summary>
        ///     Converts as GUID.
        /// </summary>
        /// <param name="input">
        ///     The input.
        /// </param>
        /// <param name="defaultValue">
        ///     The default value.
        /// </param>
        /// <returns>
        ///     The Guid value of string or default Guid value
        /// </returns>
        public static Guid ConvertToGuid(this string input, Guid defaultValue)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                var guid = input.RemoveNonLetterOrDigitChars();

                if (!string.IsNullOrEmpty(guid) && guid.Length == 32 && guid.IsValidLettersOrDigits())
                {
                    Guid parsedGuid;
                    if (Guid.TryParse(input, out parsedGuid))
                    {
                        return parsedGuid;
                    }
                }
            }

            return defaultValue;
        }

        /// <summary>
        ///     String conversion to typeof Nullable(Guid) utility
        /// </summary>
        /// <param name="input">
        ///     String version of the object
        /// </param>
        /// <param name="defaultValue">
        ///     The default value.
        /// </param>
        /// <returns>
        ///     string param as Nullable Guid
        /// </returns>
        /// <example>
        ///     View code: <br />
        ///     Guid? id = Extensions.ConvertToGuidNullable("CE9693C5-3E6A-40ac-8248-9824547E7229");<br />
        /// </example>
        public static Guid? ConvertToGuidNullable(this string input, Guid? defaultValue = null)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                var guid = input.RemoveNonLetterOrDigitChars();

                if (!string.IsNullOrEmpty(guid) && guid.Length == 32 && guid.IsValidLettersOrDigits())
                {
                    Guid parsedGuid;
                    if (Guid.TryParse(input, out parsedGuid))
                    {
                        return parsedGuid;
                    }
                }
            }

            return defaultValue;
        }

        /// <summary>
        ///     Converts as int.
        /// </summary>
        /// <param name="input">
        ///     The input.
        /// </param>
        /// <param name="defaultValue">
        ///     The default value.
        /// </param>
        /// <returns>
        ///     The int value of string or default int value
        /// </returns>
        public static int ConvertToInt(this string input, int defaultValue = default(int))
        {
            if (input.IsValidInteger())
            {
                var temp = Convert.ToInt64(input, CultureInfo.InvariantCulture);
                if (temp <= int.MaxValue && temp >= int.MinValue)
                {
                    return (int) temp;
                }
            }

            return defaultValue;
        }

        /// <summary>
        ///     String conversion to typeof Nullable(int) utility
        /// </summary>
        /// <param name="input">
        ///     String version of the object
        /// </param>
        /// <param name="defaultValue">
        ///     The default value.
        /// </param>
        /// <returns>
        ///     string param as Nullable int
        /// </returns>
        /// <example>
        ///     View code: <br />
        ///     int? id = "24536".ConvertToIntNullable();<br />
        /// </example>
        public static int? ConvertToIntNullable(this string input, int? defaultValue = null)
        {
            if (input.IsValidInteger())
            {
                var temp = Convert.ToInt64(input, CultureInfo.InvariantCulture);
                if (temp <= int.MaxValue && temp >= int.MinValue)
                {
                    return (int) temp;
                }
            }

            return defaultValue;
        }

        /// <summary>
        ///     Converts as long.
        /// </summary>
        /// <param name="input">
        ///     The input.
        /// </param>
        /// <param name="defaultValue">
        ///     The default value.
        /// </param>
        /// <returns>
        ///     The long value of string or default long value
        /// </returns>
        public static long ConvertToLong(this string input, long defaultValue = default(long))
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                long result;
                if (long.TryParse(input, out result))
                {
                    return result;
                }
            }

            return defaultValue;
        }

        /// <summary>
        ///     String conversion to typeof Nullable(long) utility
        /// </summary>
        /// <param name="input">
        ///     String version of the object
        /// </param>
        /// <param name="defaultValue">
        ///     The default value.
        /// </param>
        /// <returns>
        ///     string param as Nullable long
        /// </returns>
        /// <example>
        ///     View code: <br />
        ///     long? id = "24536".ConvertToLongNullable();<br />
        /// </example>
        public static long? ConvertToLongNullable(this string input, long? defaultValue = null)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                long result;
                if (long.TryParse(input, out result))
                {
                    return result;
                }
            }

            return defaultValue;
        }

        /// <summary>
        ///     Converts the Boolean to bit.
        /// </summary>
        /// <param name="value">
        ///     The value.
        /// </param>
        /// <returns>
        ///     The bit value
        /// </returns>
        public static string ConvertBooleanAsBit(this string value)
        {
            if (string.Equals(value, "yes", StringComparison.InvariantCultureIgnoreCase)
                || string.Equals(value, "true", StringComparison.InvariantCultureIgnoreCase)
                || string.Equals(value, "1", StringComparison.InvariantCultureIgnoreCase)
                || string.Equals(value, "y", StringComparison.InvariantCultureIgnoreCase))
            {
                return "1";
            }

            return "0";
        }

        #endregion
    }
}