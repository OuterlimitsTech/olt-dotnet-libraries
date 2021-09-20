using System;
using System.Collections.Generic;
using System.Text;

// ReSharper disable once CheckNamespace
namespace System
{
    /// <summary>
    /// Extends <see cref="DateTime"/>.
    /// </summary>
    public static class OltDateTimeExtensions
    {
        /// <summary>
        /// Adds Ordinal 1 = 1st, 2 = 2nd, 3 = 3rd
        /// </summary>
        /// <param name="self">Extends <see cref="DateTime"/>.</param>
        /// <returns></returns>
        public static string GetDayNumberSuffix(this DateTime self)
        {
            return self.Day.AddOrdinal();
        }

        /// <summary>  
        /// For calculating only age  
        /// </summary>  
        /// <param name="dateOfBirth">Date of birth</param>  
        /// <returns> age e.g. 26</returns>  
        public static int CalculateAge(this DateTime dateOfBirth)
        {
            return (int)((DateTime.Now - dateOfBirth).TotalDays / 365.242199);
        }

        /// <summary>
        /// Formats to "yyyy-MM-ddTHH:mm:ss.fffZ"
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToISO8601(this DateTimeOffset value)
        {
            return value.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        }

    }
}
