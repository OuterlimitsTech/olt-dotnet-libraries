// ReSharper disable once CheckNamespace
namespace System
{
    public static class DateTimeExtensions
    {
        public static string GetDayNumberSuffix(this DateTime date)
        {
            if (date == null)
                throw new ArgumentNullException(nameof(date));

            var day = date.Day;

            switch (day)
            {
                case 1:
                case 21:
                case 31:
                    return "st";

                case 2:
                case 22:
                    return "nd";

                case 3:
                case 23:
                    return "rd";

                default:
                    return "th";
            }
        }


        static readonly string[] formats = {
            // Basic formats
            "yyyyMMddTHHmmsszzz",
            "yyyyMMddTHHmmsszz",
            "yyyyMMddTHHmmssZ",
            // Extended formats
            "yyyy-MM-ddTHH:mm:sszzz",
            "yyyy-MM-ddTHH:mm:sszz",
            "yyyy-MM-ddTHH:mm:ssZ",
            // All of the above with reduced accuracy
            "yyyyMMddTHHmmzzz",
            "yyyyMMddTHHmmzz",
            "yyyyMMddTHHmmZ",
            "yyyy-MM-ddTHH:mmzzz",
            "yyyy-MM-ddTHH:mmzz",
            "yyyy-MM-ddTHH:mmZ",
            // Accuracy reduced to hours
            "yyyyMMddTHHzzz",
            "yyyyMMddTHHzz",
            "yyyyMMddTHHZ",
            "yyyy-MM-ddTHHzzz",
            "yyyy-MM-ddTHHzz",
            "yyyy-MM-ddTHHZ"
        };


        public static DateTime ParseISO8601String(this string str)
        {
            return DateTime.ParseExact(str, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeUniversal).ToUniversalTime();
        }


        public static long ToJulian(this DateTime dateTime)
        {
            int day = dateTime.Day;
            int month = dateTime.Month;
            int year = dateTime.Year;

            if (month < 3)
            {
                month = month + 12;
                year = year - 1;
            }

            return day + (153 * month - 457) / 5 + 365 * year + (year / 4) - (year / 100) + (year / 400) + 1721119;
        } 
 


        public static string Format(this DateTime value, string format = "MM/dd/yyyy")
        {
            if (value == DateTime.MinValue)
                return string.Empty;

            return string.Format("{0:" + format + "}", value);
        }

        /// <summary>  
        /// For calculating only age  
        /// </summary>  
        /// <param name="dateOfBirth">Date of birth</param>  
        /// <returns> age e.g. 26</returns>  
        public static int CalculateAge(this DateTime dateOfBirth)
        {
            int age = 0;
            age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age = age - 1;

            return age;
        }


        public static string ToIso8601DateTimeString(
            this DateTimeOffset value,
            bool includeTimeZone = true,
            bool includeMilliseconds = true)
        {
            string str1 = value.Year.ToString().PadLeft(4, '0') + "-" + value.Month.ToString().PadLeft(2, '0') + "-" + value.Day.ToString().PadLeft(2, '0') + "T" + value.Hour.ToString().PadLeft(2, '0') + ":" + value.Minute.ToString().PadLeft(2, '0') + ":" + value.Second.ToString().PadLeft(2, '0');
            string str2 = includeMilliseconds ? "." + value.Millisecond.ToString().PadLeft(3, '0') : string.Empty;
            string str3 = string.Empty;
            if (includeTimeZone)
            {
                if (value.ToUniversalTime() == value)
                {
                    str3 = "Z";
                }
                else
                {
                    string str4 = value.Offset < TimeSpan.Zero ? "-" : "+";
                    TimeSpan offset = value.Offset;
                    int num1 = Math.Abs(offset.Hours);
                    offset = value.Offset;
                    int num2 = Math.Abs(offset.Minutes);
                    str3 = str4 + num1.ToString().PadLeft(2, '0') + ":" + num2.ToString().PadLeft(2, '0');
                }
            }
            return str1 + str2 + str3;
        }
    }
}
