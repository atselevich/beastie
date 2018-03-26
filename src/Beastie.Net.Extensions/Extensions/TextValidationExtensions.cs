using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Beastie.Net.Extensions.Extensions
{
    public static class TextValidationExtensions
    {
        #region Constants

        /// <summary>
        ///     A description of the First or Last Name regular expression:
        ///     Beginning of line or string
        ///     Any character in this class: [a-zA-Z0-9-\s\.'], between 1 and 40 repetitions
        ///     End of line or string
        /// </summary>
        private const string RegexPatternName = @"^[a-zA-Z0-9-\s\.']{1,40}$";

        #endregion

        #region Static Fields

        /// <summary>
        ///     Regular Expression holder
        /// </summary>
        private static Regex regexName;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets a Name Regular Expression
        /// </summary>
        private static Regex RegexName => regexName
                                          ?? (regexName =
                                              new Regex(
                                                  RegexPatternName,
                                                  RegexOptions.Singleline | RegexOptions.CultureInvariant |
                                                  RegexOptions.IgnoreCase));

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Rule determines whether the specified string contains only alpha(letters) characters.
        /// </summary>
        /// <param name="input">
        ///     string containing the data to validate.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the specified string is alpha; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidLetters(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                var inputCharacters = input.ToCharArray();
                return inputCharacters.All(char.IsLetter);
            }

            return false;
        }

        /// <summary>
        ///     Rule determines whether the specified string contains only alpha(letters) characters and space.
        /// </summary>
        /// <param name="input">
        ///     string containing the data to validate.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the specified string is alpha; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidLettersAndSpace(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                var inputCharacters = input.ToCharArray();
                return inputCharacters.All(t => char.IsLetter(t) && char.IsWhiteSpace(t));
            }

            return false;
        }

        /// <summary>
        ///     Rule determines whether the specified string contains alphanumeric characters and additional chars only
        /// </summary>
        /// <param name="input">
        ///     string containing the data to validate.
        /// </param>
        /// <param name="additionalCharacters">
        ///     Valid additional chars
        /// </param>
        /// <returns>
        ///     <c>true</c> if the string is alphanumeric; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidLettersOrDigits(this string input, params char[] additionalCharacters)
        {
            if (!string.IsNullOrEmpty(input))
            {
                var inputCharacters = input.ToCharArray();

                if (additionalCharacters.Length == 0)
                {
                    // Case then no additional chars
                    return inputCharacters.All(char.IsLetterOrDigit);
                }

                // Case then has additional chars
                var list = new List<char>(additionalCharacters.Length);

                list.AddRange(additionalCharacters);

                return inputCharacters.All(current => char.IsLetterOrDigit(current) || list.Contains(current));
            }

            return false;
        }

        /// <summary>
        ///     Rule determines whether the specified string contains only alphanumeric characters, space and additional chars only
        /// </summary>
        /// <param name="input">
        ///     string containing the data to validate.
        /// </param>
        /// <param name="additionalCharacters">
        ///     Valid additional chars
        /// </param>
        /// <returns>
        ///     <c>true</c> if the string is alphanumeric; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidLettersOrDigitsOrSpace(this string input, params char[] additionalCharacters)
        {
            if (!string.IsNullOrEmpty(input))
            {
                var inputCharacters = input.ToCharArray();

                if (additionalCharacters.Length == 0)
                {
                    // Case then no additional chars
                    return inputCharacters.All(t => char.IsLetterOrDigit(t) || char.IsWhiteSpace(t));
                }

                // Case then has additional chars
                var list = new List<char>(additionalCharacters.Length);

                list.AddRange(additionalCharacters);

                return
                    inputCharacters.All(
                        current => char.IsLetterOrDigit(current) || char.IsWhiteSpace(current) ||
                                   list.Contains(current));
            }

            return false;
        }

        /// <summary>
        ///     Rule determines whether the specified string is valid First or Last Name
        /// </summary>
        /// <param name="input">
        ///     string containing the data to validate.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the string is valid First or Last Name; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidName(this string input)
        {
            return !string.IsNullOrEmpty(input) && RegexName.IsMatch(input);
        }

        #endregion
    }
}