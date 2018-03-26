using System;

namespace Beastie.Net.Extensions.Extensions
{
    public static class TypeValidationExtensions
    {

        #region Public Methods and Operators

        /// <summary>
        ///     Rule determines whether the specified string is Boolean representation as string
        /// </summary>
        /// <param name="input">
        ///     string containing the data to validate.
        /// </param>
        /// <returns>
        ///     <c>true</c> if it validates; otherwise, <c>false</c>.
        /// </returns>
        /// <example>
        ///     View code: <br />
        ///     <code title="C# File" lang="C#">
        /// /* An extension method attached to any typeof(string) object instance */ 
        ///     bool isValid = "True".IsValidBoolean();
        ///  OR
        ///     bool isValid = Extensions.IsValidBoolean("True");
        /// </code>
        /// </example>
        public static bool IsValidBoolean(this string input)
        {
            return !string.IsNullOrEmpty(input)
                   && (string.Equals(input, bool.TrueString, StringComparison.OrdinalIgnoreCase)
                       || string.Equals(input, bool.FalseString, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        ///     Rule determines whether the specified string is valid Guid
        /// </summary>
        /// <param name="input">
        ///     string containing the data to validate.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the string is Guid; otherwise, <c>false</c>.
        /// </returns>
        /// <example>
        ///     View code: <br />
        ///     <code title="C# File" lang="C#">
        /// /* An extension method attached to any typeof(string) object instance */
        ///     bool isValid = "A2CC11F4-10D5-4dd2-B746-24FD4B7C4503".IsValidZipCodeFive();
        /// OR
        ///     if(this.Page.HiddenPrimaryID.Value.IsValidGuid())
        ///     {
        ///         // Do something
        ///     }
        /// OR
        ///     bool isValid = Extensions.IsValidGuid("A2CC11F4-10D5-4dd2-B746-24FD4B7C4503");
        /// </code>
        /// </example>
        public static bool IsValidGuid(this string input)
        {
            return input.IsNotNullOrEmpty() && Guid.TryParse(input, out Guid newGuid);
        }

        #endregion
    }
}