using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security.AntiXss;
using Beastie.Net.Extensions.Constants;
using Beastie.Net.Extensions.Formatters;

namespace Beastie.Net.Extensions.Extensions
{
    public static class StringExtensions
    {
        #region Constants

        /// <summary>
        ///     The regex special character.
        /// </summary>
        private const string RegexSpecialCharacter = @"\[\\\^\$\.\|\?\*\+\(\)";

        #endregion

        #region Static Fields

        /// <summary>
        ///     Regular Expression holder
        /// </summary>
        private static Regex regexEmptySpaces;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets a Us Zip Code Regular Expression
        /// </summary>
        private static Regex RegexEmptySpaces
            =>
                regexEmptySpaces
                ?? (regexEmptySpaces =
                    new Regex(
                        @"\s{2,}",
                        RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Singleline
                        | RegexOptions.Compiled));

        #endregion

        #region Public Methods and Operators

        public static string AggregateToString(this IEnumerable<string> input, string delimiter)
        {
            if (input != null)
            {
                return input.Aggregate(
                    string.Empty,
                    (current, entry) => current + (current.IsNullOrEmptyOrWhiteSpace() ? entry : delimiter + entry));
            }

            return string.Empty;
        }


        public static string AggregateToString(this IEnumerable<string> input, char delimiter)
        {
            if (input != null)
            {
                return input.Aggregate(
                    string.Empty,
                    (current, entry) => current + (current.IsNullOrEmptyOrWhiteSpace() ? entry : delimiter + entry));
            }

            return string.Empty;
        }

        public static string Clip(this string text, int length, bool ellipsis = true)
        {
            if (text.IsNotNullOrEmpty())
            {
                if (text.Length > length)
                {
                    if (ellipsis)
                    {
                        length -= 3;
                    }

                    var length1 = text.LastIndexOf(" ", length, StringComparison.InvariantCulture);
                    if (length1 < 0)
                    {
                        length1 = length;
                    }

                    text = text.Substring(0, length1);
                    if (ellipsis)
                    {
                        text = text + "...";
                    }
                }
            }

            return text;
        }


        public static bool Contains(this string inputValue, string comparisonValue, StringComparison comparisonType)
        {
            if (!string.IsNullOrEmpty(inputValue) && !string.IsNullOrEmpty(comparisonValue))
            {
                return inputValue.IndexOf(comparisonValue, comparisonType) != -1;
            }

            return false;
        }

        public static string DecodeFromBase64(this string encodedValue)
        {
            if (encodedValue != null)
            {
                return encodedValue.DecodeFromBase64(Encoding.UTF8);
            }

            return string.Empty;
        }


        public static string DecodeFromBase64(this string encodedValue, Encoding encoding)
        {
            if (encodedValue != null)
            {
                encoding = encoding ?? Encoding.UTF8;

                var bytes = Convert.FromBase64String(encodedValue);

                return encoding.GetString(bytes);
            }

            return string.Empty;
        }


        public static string EncodeBase64(this string value)
        {
            if (value != null)
            {
                return value.EncodeToBase64(Encoding.UTF8);
            }

            return string.Empty;
        }


        public static string EncodeSpecialCharForRegexExpression(this string strIn)
        {
            // http://www.regular-expressions.info/characters.html
            // List of special Character for Regex => [\^$.|?*+()

            // Esacape character by adding prefix "\"
            return Regex.Replace(
                strIn,
                RegexSpecialCharacter,
                delegate(Match match)
                {
                    var v = match.ToString();
                    return @"\" + v[0];
                });
        }

        public static string EncodeToCsv(this string input)
        {
            if (input.IndexOfAny(" ,\n".ToCharArray()) < 0 && input.Trim() == input)
            {
                return input;
            }

            var sb = new StringBuilder();
            sb.Append('"');
            foreach (var c in input)
            {
                sb.Append(c);
                if (c == '"')
                {
                    sb.Append(c);
                }
            }

            sb.Append('"');
            return sb.ToString();
        }

        /// </example>
        public static string EnsureEndsWith(
            this string value,
            string suffix,
            StringComparison comparison = StringComparison.InvariantCultureIgnoreCase)
        {
            if (!string.IsNullOrEmpty(value) && value.EndsWith(suffix, comparison))
            {
                return value;
            }

            return string.Concat(value, suffix);
        }


        public static string EnsureStartsWith(
            this string value,
            string prefix,
            StringComparison comparison = StringComparison.InvariantCultureIgnoreCase)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (value.StartsWith(prefix, comparison))
                {
                    return value;
                }
            }

            return string.Concat(prefix, value);
        }


        public static string GetExtensionFromContentType(this string contentType)
        {
            var retVal = string.Empty;
            switch (contentType.ToLower())
            {
                case ContentTypes.Jpeg:
                case ContentTypes.Jpg:
                    retVal = "jpg";
                    break;
                case ContentTypes.Gif:
                    retVal = "gif";
                    break;
                case ContentTypes.Png:
                case ContentTypes.Xpng:
                    retVal = "png";
                    break;
                case ContentTypes.Tiff:
                    retVal = "tiff";
                    break;
                case ContentTypes.Pdf:
                case ContentTypes.Xpdf:
                    retVal = "pdf";
                    break;
            }

            return retVal;
        }


        public static bool HasWhiteSpace(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            return input.All(char.IsWhiteSpace);
        }

        public static string HtmlEncode(this string input, bool useNamedEntities = false)
        {
            return AntiXssEncoder.HtmlEncode(input, useNamedEntities);
        }

        public static string HtmlEncode(this string input)
        {
            return HtmlEncode(input, false);
        }

        public static StringBuilder HtmlEncode(this StringBuilder builder, string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return builder;
            }

            builder.Append(HtmlEncode(input));

            return builder;
        }


        public static bool IsLower(this string value)
        {
            // Consider string to be lowercase if it has no uppercase letters.
            return value.All(t => !char.IsUpper(t));
        }

        public static bool IsNotNullOrEmpty(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }


        public static bool IsNotNullOrEmptyOrWhiteSpace(this string input)
        {
            return !string.IsNullOrEmpty(input) && !input.HasWhiteSpace();
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNullOrEmptyOrWhiteSpace(this string input)
        {
            return string.IsNullOrEmpty(input) || input.HasWhiteSpace();
        }

        public static bool IsUpper(this string value)
        {
            // Consider string to be uppercase if it has no lowercase letters.
            return value.All(t => !char.IsLower(t));
        }


        public static string NullIfEmpty(this string value)
        {
            return value.IsNullOrEmpty() ? null : value;
        }


        public static string Remove(this string value, string stringToRemove)
        {
            // From Standard Extensions Library idea
            if (!string.IsNullOrEmpty(value))
            {
                return value.Replace(stringToRemove, string.Empty);
            }

            return value;
        }

        public static string RemoveNonDigitChars(
            this string input,
            bool appendWhiteSpaceInstead = false,
            params char[] exceptCharacters)
        {
            if (!string.IsNullOrEmpty(input))
            {
                var builder = new StringBuilder(input.Length);

                if (exceptCharacters.Length == 0)
                {
                    foreach (var current in input)
                    {
                        if (char.IsDigit(current))
                        {
                            builder.Append(current);
                        }
                        else
                        {
                            if (appendWhiteSpaceInstead)
                            {
                                builder.Append(' ');
                            }
                        }
                    }
                }
                else
                {
                    // Case then has additional chars
                    var list = new List<char>(exceptCharacters.Length);
                    list.AddRange(exceptCharacters);

                    foreach (var current in input)
                    {
                        if (char.IsDigit(current) || list.Contains(current))
                        {
                            builder.Append(current);
                        }
                        else
                        {
                            if (appendWhiteSpaceInstead)
                            {
                                builder.Append(' ');
                            }
                        }
                    }
                }

                return builder.ToString().Trim();
            }

            return string.Empty;
        }

        public static string RemoveNonFileNameChars(this string input, bool appendWhiteSpaceInstead = false)
        {
            if (!string.IsNullOrEmpty(input))
            {
                var builder = new StringBuilder(input.Length);
                foreach (var current in input)
                {
                    if (char.IsLetterOrDigit(current) || char.IsWhiteSpace(current) || '-'.Equals(current)
                        || '.'.Equals(current) || '_'.Equals(current))
                    {
                        builder.Append(current);
                    }
                    else
                    {
                        if (appendWhiteSpaceInstead)
                        {
                            builder.Append(' ');
                        }
                    }
                }

                return builder.ToString().Trim();
            }

            return string.Empty;
        }

        public static string RemoveNonLetterOrDigitChars(
            this string input,
            bool appendWhiteSpaceInstead = false,
            params char[] exceptCharacters)
        {
            if (!string.IsNullOrEmpty(input))
            {
                var builder = new StringBuilder(input.Length);
                if (exceptCharacters.Length == 0)
                {
                    foreach (var current in input)
                    {
                        if (char.IsLetterOrDigit(current))
                        {
                            builder.Append(current);
                        }
                        else
                        {
                            if (appendWhiteSpaceInstead)
                            {
                                builder.Append(' ');
                            }
                        }
                    }
                }
                else
                {
                    // Case then has additional chars
                    var list = new List<char>(exceptCharacters.Length);
                    list.AddRange(exceptCharacters);

                    foreach (var current in input)
                    {
                        if (char.IsLetterOrDigit(current) || list.Contains(current))
                        {
                            builder.Append(current);
                        }
                        else
                        {
                            if (appendWhiteSpaceInstead)
                            {
                                builder.Append(' ');
                            }
                        }
                    }
                }

                return builder.ToString().Trim();
            }

            return string.Empty;
        }

        public static string RemoveNonLettersChars(
            this string input,
            bool appendWhiteSpaceInstead = false,
            params char[] exceptCharacters)
        {
            if (!string.IsNullOrEmpty(input))
            {
                var builder = new StringBuilder(input.Length);

                if (exceptCharacters.Length == 0)
                {
                    foreach (var current in input)
                    {
                        if (char.IsLetter(current))
                        {
                            builder.Append(current);
                        }
                        else
                        {
                            if (appendWhiteSpaceInstead)
                            {
                                builder.Append(' ');
                            }
                        }
                    }
                }
                else
                {
                    // Case then has additional chars
                    var list = new List<char>(exceptCharacters.Length);
                    list.AddRange(exceptCharacters);

                    foreach (var current in input)
                    {
                        if (char.IsLetter(current) || list.Contains(current))
                        {
                            builder.Append(current);
                        }
                        else
                        {
                            if (appendWhiteSpaceInstead)
                            {
                                builder.Append(' ');
                            }
                        }
                    }
                }

                return builder.ToString().Trim();
            }

            return string.Empty;
        }

        public static string RemoveNonNumberChars(
            this string input,
            bool appendWhiteSpaceInstead = false,
            params char[] exceptCharacters)
        {
            if (!string.IsNullOrEmpty(input))
            {
                var builder = new StringBuilder(input.Length);

                if (exceptCharacters.Length == 0)
                {
                    foreach (var current in input)
                    {
                        if (char.IsDigit(current) || '.'.Equals(current))
                        {
                            builder.Append(current);
                        }
                        else
                        {
                            if (appendWhiteSpaceInstead)
                            {
                                builder.Append(' ');
                            }
                        }
                    }
                }
                else
                {
                    // Case then has additional chars
                    var list = new List<char>(exceptCharacters.Length);
                    list.AddRange(exceptCharacters);

                    foreach (var current in input)
                    {
                        if (char.IsDigit(current) || '.'.Equals(current) || list.Contains(current))
                        {
                            builder.Append(current);
                        }
                        else
                        {
                            if (appendWhiteSpaceInstead)
                            {
                                builder.Append(' ');
                            }
                        }
                    }
                }

                return builder.ToString().Trim();
            }

            return string.Empty;
        }

        public static string RemoveWhitespace(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return new string(value.ToCharArray().Where(c => !char.IsWhiteSpace(c)).ToArray());
            }

            return value;
        }


        public static string Replace(this string value, string stringToReplace, string replacement)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return value.Replace(stringToReplace, replacement);
            }

            return value;
        }


        public static string ReplaceNonDigitsChars(
            this string input,
            string replacement,
            params char[] exceptCharacters)
        {
            if (!string.IsNullOrEmpty(input))
            {
                var builder = new StringBuilder(input.Length);

                if (exceptCharacters.Length == 0)
                {
                    foreach (var current in input)
                    {
                        if (char.IsDigit(current))
                        {
                            builder.Append(current);
                        }
                        else
                        {
                            builder.Append(replacement);
                        }
                    }
                }
                else
                {
                    // Case then has additional chars
                    var list = new List<char>(exceptCharacters.Length);
                    list.AddRange(exceptCharacters);

                    foreach (var current in input)
                    {
                        if (char.IsDigit(current) || list.Contains(current))
                        {
                            builder.Append(current);
                        }
                        else
                        {
                            builder.Append(replacement);
                        }
                    }
                }

                return builder.ToString();
            }

            return string.Empty;
        }


        public static string ReplaceNonLetterOrDigitChars(
            this string input,
            string replacement,
            params char[] exceptCharacters)
        {
            if (!string.IsNullOrEmpty(input))
            {
                var builder = new StringBuilder(input.Length);

                if (exceptCharacters.Length == 0)
                {
                    foreach (var current in input)
                    {
                        if (char.IsLetterOrDigit(current))
                        {
                            builder.Append(current);
                        }
                        else
                        {
                            builder.Append(replacement);
                        }
                    }
                }
                else
                {
                    // Case then has additional chars
                    var list = new List<char>(exceptCharacters.Length);
                    list.AddRange(exceptCharacters);

                    foreach (var current in input)
                    {
                        if (char.IsLetterOrDigit(current) || list.Contains(current))
                        {
                            builder.Append(current);
                        }
                        else
                        {
                            builder.Append(replacement);
                        }
                    }
                }

                return builder.ToString();
            }

            return string.Empty;
        }

        public static string ReplaceNonLettersChars(
            this string input,
            string replacement,
            params char[] exceptCharacters)
        {
            if (!string.IsNullOrEmpty(input))
            {
                var builder = new StringBuilder(input.Length);

                if (exceptCharacters.Length == 0)
                {
                    foreach (var current in input)
                    {
                        if (char.IsLetter(current))
                        {
                            builder.Append(current);
                        }
                        else
                        {
                            builder.Append(replacement);
                        }
                    }
                }
                else
                {
                    // Case then has additional chars
                    var list = new List<char>(exceptCharacters.Length);
                    list.AddRange(exceptCharacters);

                    foreach (var current in input)
                    {
                        if (char.IsLetter(current) || list.Contains(current))
                        {
                            builder.Append(current);
                        }
                        else
                        {
                            builder.Append(replacement);
                        }
                    }
                }

                return builder.ToString();
            }

            return string.Empty;
        }


        public static string ReplaceNonNumberChars(
            this string input,
            string replacement,
            params char[] exceptCharacters)
        {
            if (!string.IsNullOrEmpty(input))
            {
                var firstIndex = input.IndexOf('.');
                if (firstIndex > -1)
                {
                    var lastIndex = input.LastIndexOf('.');
                    if (firstIndex != lastIndex)
                    {
                        throw new HttpException("The string can't contain more then one '.' char!");
                    }
                }

                var builder = new StringBuilder(input.Length);
                if (exceptCharacters.Length == 0)
                {
                    for (var i = 0; i < input.Length; i++)
                    {
                        var current = input[i];
                        if (i == 0 && '.'.Equals(current))
                        {
                            /* Add 0 prefix for cases like '.05' */
                            builder.Append(0);
                            builder.Append(current);
                            continue;
                        }

                        if (char.IsDigit(current) || '.'.Equals(current))
                        {
                            builder.Append(current);
                        }
                        else
                        {
                            builder.Append(replacement);
                        }
                    }
                }
                else
                {
                    // Case then has additional chars
                    var list = new List<char>(exceptCharacters.Length);
                    list.AddRange(exceptCharacters);

                    for (var i = 0; i < input.Length; i++)
                    {
                        var current = input[i];
                        if (i == 0 && '.'.Equals(current))
                        {
                            /* Add 0 prefix for cases like '.05' */
                            builder.Append(0);
                            builder.Append(current);
                            continue;
                        }

                        if (char.IsDigit(current) || '.'.Equals(current) || list.Contains(current))
                        {
                            builder.Append(current);
                        }
                        else
                        {
                            builder.Append(replacement);
                        }
                    }
                }

                return builder.ToString();
            }

            return string.Empty;
        }


        public static string ReplaceSpecialCharactersAndMultipleSpacesWithSingleSpace(
            this string words,
            char replacement,
            bool makeLowercase)
        {
            var newString = string.Empty;
            if (!string.IsNullOrEmpty(words))
            {
                newString = Regex.Replace(words, @"[^\w]", " "); // replace special characters with space
                newString = Regex.Replace(newString, @"\s+", " "); // replace multiple space with a single space
                newString = Regex.Replace(newString, @"^[ \t]+|[ \t]+$", string.Empty);

                // remove leading and trailing space

                // replace with a replacement character
                newString = newString.Replace(' ', replacement);
            }

            return makeLowercase ? newString.ToLower() : newString;
        }


        public static IList<string> Split(this string value, Regex regex)
        {
            if (!string.IsNullOrWhiteSpace(value) || regex != null)
            {
                // ReSharper disable AssignNullToNotNullAttribute
                return regex.Split(value);

                // ReSharper restore AssignNullToNotNullAttribute
            }

            return new Collection<string>();
        }


        public static IEnumerable<string> SplitAndTrim(this string input, params char[] chars)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                var result = input.Trim().Split(chars, StringSplitOptions.RemoveEmptyEntries);
                return result.Select(x => x.TrimEmptySpaces());
            }

            return new string[] { };
        }

        public static string ToCamelCase(this string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                var inputCharacters = input.ToCharArray();
                inputCharacters[0] = char.ToLowerInvariant(inputCharacters[0]);

                return new string(inputCharacters);
            }

            return input;
        }


        public static string ToLowerCaseFirstLetter(this string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                return char.ToUpperInvariant(input[0]) + input.Substring(1);
            }

            return input;
        }


        public static string ToPascalCase(this string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                return char.ToUpperInvariant(input[0]) + input.Substring(1);
            }

            return input;
        }


        public static string ToPhoneFormat(this string phoneNumber, string format = "###-###-####")
        {
            var cleaned = phoneNumber.RemoveNonDigitChars();
            if (!string.IsNullOrWhiteSpace(cleaned))
            {
                long phone;
                if (long.TryParse(cleaned, out phone))
                {
                    return phone.ToString(format);
                }

                return phoneNumber;
            }

            return string.Empty;
        }


        public static string ToPhoneFormat(this long phoneNumber, string format = "###-###-####")
        {
            if (phoneNumber > 1000000000 && !string.IsNullOrWhiteSpace(format))
            {
                if (phoneNumber > 10000000000000)
                {
                    return phoneNumber.ToString(format + " ext ####");
                }

                if (phoneNumber > 1000000000000)
                {
                    return phoneNumber.ToString(format + " ext ###");
                }

                return phoneNumber.ToString(format);
            }

            return phoneNumber.ToString(CultureInfo.InvariantCulture);
        }

        public static string ToPhoneFormat(this long? phoneNumber, string format = "###-###-####")
        {
            if (phoneNumber.HasValue)
            {
                return ToPhoneFormat(phoneNumber.Value, format);
            }

            return string.Empty;
        }

        public static string ToPropertyName(this string name)
        {
            var tokens = name.Split(' ', '_', '-');
            for (var i = 0; i < tokens.Length; i++)
            {
                tokens[i] = tokens[i].ToUpperCaseFirstLetters();
            }

            return string.Concat(tokens).RemoveNonLetterOrDigitChars();
        }


        public static string ToUpperCaseFirstLetters(this string input, params char[] separators)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (separators.Length == 0)
                {
                    return char.ToUpperInvariant(input[0]) + input.Substring(1);
                }

                var inputCharacters = input.ToCharArray();

                // Case then has additional chars
                var list = new List<char>(separators.Length);
                list.AddRange(separators);

                for (var i = 0; i < inputCharacters.Length; i++)
                {
                    if (i == 0)
                    {
                        inputCharacters[0] = char.ToUpperInvariant(inputCharacters[0]);
                    }
                    else
                    {
                        if (list.Contains(inputCharacters[i - 1]))
                        {
                            inputCharacters[i] = char.ToUpperInvariant(inputCharacters[i]);
                        }
                    }
                }

                return new string(inputCharacters);
            }

            return input;
        }

        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1631:DocumentationMustMeetCharacterPercentage",
            Justification = "Reviewed. Suppression is OK here.")]
        public static string ToUsPhoneFormat(this object phoneNumber, string format = "{0:D}")
        {
            /* d: 555.555.5555 x0000
             * D: 555-555-5555 x0000
             * P: (555) 555-5555 x0000
             * */
            return string.Format(new UsPhoneNumberFormatter(), format, phoneNumber);
        }


        public static string TrimDuplicateEmptySpaces(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            return RegexEmptySpaces.Replace(value.Trim(), " ");
        }


        public static string TrimEmptySpaces(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            return value.Trim();
        }

        public static string UrlEncode(this string input)
        {
            return AntiXssEncoder.UrlEncode(input).Replace("%20", "+");
        }

        public static StringBuilder UrlEncode(this StringBuilder builder, string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return builder;
            }

            builder.Append(UrlEncode(input));

            return builder;
        }

        public static string XmlEncode(this string input)
        {
            return input.HtmlEncode();
        }

        #endregion
    }
}