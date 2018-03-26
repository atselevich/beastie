using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Beastie.Net.Extensions.Extensions
{
    public static class DateTimeExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Gets end of the day DateTime value for provided date.
        /// </summary>
        /// <param name="date">
        ///     The specified date.
        /// </param>
        /// <returns>
        ///     The specified date 1 second before the end of the day {12/1/2016 12:00:00 AM} will be {12/1/2016 11:59:59 PM}
        /// </returns>
        public static DateTime EndOfTheDay(this DateTime date)
        {
            return date > DateTime.MinValue && date < DateTime.MaxValue ? date.AddDays(1).AddSeconds(-1) : date;
        }

        /// <summary>
        ///     The get most recent.
        /// </summary>
        /// <param name="dayOfWeek">
        ///     The day of week.
        /// </param>
        /// <returns>
        ///     The <see cref="DateTime" />.
        /// </returns>
        public static DateTime GetMostRecent(this DayOfWeek dayOfWeek)
        {
            var dateTime = DateTime.Now;
            while (dateTime.DayOfWeek != DayOfWeek.Sunday)
            {
                dateTime = dateTime.AddDays(-1);
            }

            return dateTime;
        }

        #endregion

        #region Constants

        /// <summary>
        ///     Regular expression
        /// </summary>
        private const string RegexExpressionIsoDateTime =
                @"^(?<Year>\d{4})-{0,1}(?<Month>\d{2})-{0,1}(?<Day>\d{2})(([tT:dD]{0,1})(?<Hour>\d{2}))?(:{0,1}(?<Minutes>\d{2}))?(:{0,1}(?<Seconds>\d{2}))?"
            ;

        #endregion

        #region Static Fields

        /// <summary>
        ///     The REGEX ISO date time.
        /// </summary>
        private static Regex regexIsoDateTime;

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the of REGEX ISO date time.
        /// </summary>
        /// <value>The REGEX ISO date time.</value>
        /// <example>
        ///     View code: <br />
        ///     <code>
        /// <![CDATA[
        ///  A description of the regular expression:
        ///  Beginning of line or string
        ///  [Year]: A named capture group. [\d{4}]
        ///      Any digit, exactly 4 repetitions
        ///  -, between 0 and 1 repetitions
        ///  [Month]: A named capture group. [\d{2}]
        ///      Any digit, exactly 2 repetitions
        ///  -, between 0 and 1 repetitions
        ///  [Day]: A named capture group. [\d{2}]
        ///      Any digit, exactly 2 repetitions
        ///  [1]: A numbered capture group. [([tT:dD]{0,1})(?<Hour>\d{2})], zero or one repetitions
        ///      ([tT:dD]{0,1})(?<Hour>\d{2})
        ///          [2]: A numbered capture group. [[tT:dD]{0,1}]
        ///              Any character in this class: [tT:dD], between 0 and 1 repetitions
        ///          [Hour]: A named capture group. [\d{2}]
        ///              Any digit, exactly 2 repetitions
        ///  [3]: A numbered capture group. [:{0,1}(?<Minutes>\d{2})], zero or one repetitions
        ///      :{0,1}(?<Minutes>\d{2})
        ///          :, between 0 and 1 repetitions
        ///          [Minutes]: A named capture group. [\d{2}]
        ///              Any digit, exactly 2 repetitions
        ///  [4]: A numbered capture group. [:{0,1}(?<Seconds>\d{2})], zero or one repetitions
        ///      :{0,1}(?<Seconds>\d{2})
        ///          :, between 0 and 1 repetitions
        ///          [Seconds]: A named capture group. [\d{2}]
        ///              Any digit, exactly 2 repetitions
        /// ]]>
        /// </code>
        /// </example>
        public static Regex RegexIsoDateTime => regexIsoDateTime
                                                ?? (regexIsoDateTime =
                                                    new Regex(
                                                        RegexExpressionIsoDateTime,
                                                        RegexOptions.CultureInvariant | RegexOptions.IgnoreCase |
                                                        RegexOptions.Singleline
                                                        | RegexOptions.Compiled));

        #endregion


        /// <summary>
        ///     Converts the iso date time like '20130904T020426'.
        /// </summary>
        /// <param name="isoDate">
        ///     The iso date.
        /// </param>
        /// <param name="ignoreTime">
        ///     if set to <c>true</c> [ignore time].
        /// </param>
        /// <param name="dateTimeFormat">
        ///     The date time format.
        /// </param>
        /// <param name="dateTimeStyle">
        ///     The date time style.
        /// </param>
        /// <returns>
        ///     The datetime from ISO string like '20130904T020426'
        /// </returns>
        public static DateTime? ConvertToIsoDateTime(
            this string isoDate,
            bool ignoreTime = true,
            string dateTimeFormat = "yyyyMMddTHHmmss",
            DateTimeStyles dateTimeStyle = DateTimeStyles.AssumeLocal)
        {
            DateTime dateTime;
            if (!string.IsNullOrWhiteSpace(isoDate)
                && DateTime.TryParseExact(
                    isoDate,
                    dateTimeFormat,
                    CultureInfo.InvariantCulture,
                    dateTimeStyle,
                    out dateTime))
            {
                if (ignoreTime)
                {
                    return dateTime.Date;
                }

                return dateTime;
            }

            return null;
        }

        /// <summary>
        ///     Converts a DateTime value to w3c format
        ///     based on http://www.w3.org/TR/NOTE-datetime notes
        /// </summary>
        /// <param name="date">
        ///     The date.
        /// </param>
        /// <returns>
        ///     The date in W3C format
        /// </returns>
        /// <see href="http://www.bytechaser.com/en/functions/k4s6frpca5/convert-date-time-to-w3c-datetime-format.aspx" />
        public static string ConvertDateAsW3CTime(this DateTime date)
        {
            // Get the UTC offset from the date value
            var utcOffset = TimeZone.CurrentTimeZone.GetUtcOffset(date);

            var time = date.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss");

            // append the offset e.g. z=0, add 1 hour is +01:00
            time += utcOffset == TimeSpan.Zero
                ? "Z"
                : $"{(utcOffset > TimeSpan.Zero ? "+" : "-")}{utcOffset.Hours:00}:{utcOffset.Minutes:00}";

            return time;
        }

        /// <summary>
        ///     Formats the HTTP cookie date time.
        /// </summary>
        /// <param name="datetime">
        ///     The datetime.
        /// </param>
        /// <returns>
        ///     The Date as string for cookie time
        /// </returns>
        public static string FormatAsHttpCookieDateTime(this DateTime datetime)
        {
            if (datetime < DateTime.MaxValue.AddDays(-1.0) && datetime > DateTime.MinValue.AddDays(1.0))
            {
                datetime = datetime.ToUniversalTime();
            }

            return datetime.ToString("ddd, dd-MMM-yyyy HH':'mm':'ss 'GMT'", DateTimeFormatInfo.InvariantInfo);
        }

        /// <summary>
        ///     Formats date time to correct ISO date.
        /// </summary>
        /// <param name="datetime">
        ///     The date time.
        /// </param>
        /// <param name="ignoreTime">
        ///     if set to <c>true</c> ignore time part.
        /// </param>
        /// <returns>
        ///     The date time in ISO format
        /// </returns>
        public static string ToIsoDateFormat(this DateTime datetime, bool ignoreTime = true)
        {
            if (ignoreTime)
            {
                return datetime.Date.ToString("yyyyMMddTHHmmss");
            }

            return datetime.ToString("yyyyMMddTHHmmss");
        }

        /// <summary>
        ///     The get time span.
        /// </summary>
        /// <param name="dateTime">
        ///     The date time arg.
        /// </param>
        /// <returns>
        ///     The <see cref="TimeSpan" />.
        /// </returns>
        public static TimeSpan ToTimeSpan(this DateTime dateTime)
        {
            int hours;
            int.TryParse(dateTime.ToString("HH"), out hours);

            int minutes;
            int.TryParse(dateTime.ToString("mm"), out minutes);

            int seconds;
            int.TryParse(dateTime.ToString("ss"), out seconds);

            return new TimeSpan(hours, minutes, seconds);
        }

        /// <summary>
        ///     Convert date to the US date.
        /// </summary>
        /// <param name="date">
        ///     The specified date.
        /// </param>
        /// <returns>
        ///     The specified date formatted as a 'MM/dd/yyyy' date string
        /// </returns>
        public static string ToUsDate(this DateTime date) /* ReSharper restore InconsistentNaming */
        {
            return date.ToString("MM/dd/yyyy");
        }
    }
}