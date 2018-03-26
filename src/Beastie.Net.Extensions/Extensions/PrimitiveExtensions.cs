using System;
using System.Diagnostics;

namespace Beastie.Net.Extensions.Extensions
{
    public static class PrimitiveExtensions
    {
        #region Static Fields

        /// <summary>
        ///     Synch root object
        /// </summary>
        public static readonly object SynchRoot = new object();

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Clones the specified original and keeps the type (must return object of the same type)
        /// </summary>
        /// <typeparam name="TObject">
        ///     The type of the object.
        /// </typeparam>
        /// <param name="original">
        ///     The original.
        /// </param>
        /// <returns>
        ///     The clone or null if the original object is null or does not return an object of the same type
        /// </returns>
        public static TObject GetClone<TObject>(this TObject original) where TObject : class, ICloneable
        {
            return original?.Clone() as TObject;
        }

        /// <summary>
        ///     The method determines if object is Null/DBNull
        /// </summary>
        /// <param name="value">
        ///     The object to check against it
        /// </param>
        /// <returns>
        ///     True in the case object is not Null/DBNull and false otherwise
        /// </returns>
        [DebuggerHidden]
        public static bool IsNotNull(this object value)
        {
            return !(value == null || value is DBNull);
        }

        /// <summary>
        ///     The method determines if object is Null
        /// </summary>
        /// <param name="value">
        ///     The string to check against it
        /// </param>
        /// <returns>
        ///     True in the case string is not Null and false otherwise
        /// </returns>
        [DebuggerHidden]
        public static bool IsNotNull(this string value)
        {
            return value != null;
        }

        /// <summary>
        ///     The method determines if object is Null/DBNull
        /// </summary>
        /// <param name="value">
        ///     The object to check against it
        /// </param>
        /// <returns>
        ///     True in the case object is Null/DBNull and false otherwise
        /// </returns>
        [DebuggerHidden]
        public static bool IsNull(this object value)
        {
            return value == null || value is DBNull;
        }

        /// <summary>
        ///     The method determines if string is Null
        /// </summary>
        /// <param name="value">
        ///     The string to check against it
        /// </param>
        /// <returns>
        ///     True in the case object is Null and false otherwise
        /// </returns>
        [DebuggerHidden]
        public static bool IsNull(this string value)
        {
            return value == null;
        }

        /// <summary>
        ///     Rounds to nearest.
        /// </summary>
        /// <example>
        ///     3.6 rounds to 4
        ///     3.5 rounds to 4
        ///     3.4 rounds to 3
        /// </example>
        /// <param name="value">
        ///     The value.
        /// </param>
        /// <param name="digits">
        ///     The digits.
        /// </param>
        /// <returns>
        ///     The Rounded number
        /// </returns>
        public static decimal? RoundToNearest(this decimal? value, int digits = 0)
        {
            return value?.RoundToNearest(digits);
        }

        /// <summary>
        ///     Rounds to nearest.
        /// </summary>
        /// <example>
        ///     3.6 rounds to 4
        ///     3.5 rounds to 4
        ///     3.4 rounds to 3
        /// </example>
        /// <param name="value">
        ///     The value.
        /// </param>
        /// <param name="digits">
        ///     The digits.
        /// </param>
        /// <returns>
        ///     The Rounded number
        /// </returns>
        public static decimal RoundToNearest(this decimal value, int digits = 0)
        {
            return Math.Round(value, digits, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        ///     Rounds to nearest.
        /// </summary>
        /// <example>
        ///     3.6 rounds to 4
        ///     3.5 rounds to 4
        ///     3.4 rounds to 3
        /// </example>
        /// <param name="value">
        ///     The value.
        /// </param>
        /// <param name="digits">
        ///     The digits.
        /// </param>
        /// <returns>
        ///     The Rounded number
        /// </returns>
        public static double? RoundToNearest(this double? value, int digits = 0)
        {
            return value?.RoundToNearest(digits);
        }

        /// <summary>
        ///     Rounds to nearest.
        /// </summary>
        /// <example>
        ///     3.6 rounds to 4
        ///     3.5 rounds to 4
        ///     3.4 rounds to 3
        /// </example>
        /// <param name="value">
        ///     The value.
        /// </param>
        /// <param name="digits">
        ///     The digits.
        /// </param>
        /// <returns>
        ///     The Rounded number
        /// </returns>
        public static double RoundToNearest(this double value, int digits = 0)
        {
            return Math.Round(value, digits, MidpointRounding.AwayFromZero);
        }

        #endregion
    }
}