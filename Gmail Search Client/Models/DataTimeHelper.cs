using System;
namespace Gmail_Search_Client.Models
{
    static public class DataTimeHelper
    {
        /// <summary>
        /// Converts the received date to a date that corresponds to the local time
        /// </summary>
        /// <param name="dateEtered"></param>
        /// <returns></returns>
        public static DateTime ToCurrentTimeZone(DateTime dateEtered)
        {
            int summerTime = 1;
            DateTime dateToReturn = TimeZoneInfo.Local.IsDaylightSavingTime(dateTime: dateEtered) ?
                     dateEtered + TimeZoneInfo.Local.BaseUtcOffset + new TimeSpan(summerTime, 0, 0):
                     dateEtered + TimeZoneInfo.Local.BaseUtcOffset;
            return dateToReturn;
        }

        /// <summary>
        /// Converts a Unix time expressed as the number of milliseconds that have elapsed 
        /// since 1970-01-01T00:00:00Z to a System.DateTimeOffset value.
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static DateTime FromUnixTimeMilliseconds(long inputValue)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(milliseconds: inputValue).DateTime;
        }
    }
}
