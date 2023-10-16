using System.Globalization;

namespace Infrastructure.Utils
{
    public static class DateTimeHelper
    {
        /// <summary>
        /// Convert a DateTime object to string with format [dd/MM/yyyy] (Ex: 13/10/2023)
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string FormatDateTimeToDate(DateTime? date) => date?.ToString("dd/MM/yyyy") ?? "";

        /// <summary>
        /// Convert a DateTime object to string with format [dd/MM/yyyy HH:mm:ss] (Ex: 13/10/2023 15:30:45)
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string FormatDateTimeToDatetime(DateTime? date) => date?.ToString("dd/MM/yyyy HH:mm:ss") ?? "";

        /// <summary>
        /// Convert a string contains date with format [dd/MM/yyyy] to a DateTime object (Ex: "13/10/2023")
        /// </summary>
        /// <param name="dateString"></param>
        /// <returns></returns>
        public static DateTime? ConvertDateStringToDateTime(string? dateString)
        {
            if (string.IsNullOrEmpty(dateString))
            {
                return null;
            }

            DateTime date;
            if (DateTime.TryParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                    out date))
            {
                return date;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Convert a string contains datetime with format [dd/MM/yyyy HH:mm:ss] to a DateTime object (Ex: "13/10/2023 15:30:45")
        /// </summary>
        /// <param name="dateTimeString"></param>
        /// <returns></returns>
        public static DateTime? ConvertDateTimeStringToDateTime(string? dateTimeString)
        {
            if (string.IsNullOrEmpty(dateTimeString))
            {
                return null;
            }

            DateTime date;
            if (DateTime.TryParseExact(dateTimeString, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None,
                    out date))
            {
                return date;
            }
            else
            {
                return null;
            }
        }
    }
}
