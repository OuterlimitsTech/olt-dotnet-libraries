using System;

// ReSharper disable once CheckNamespace
namespace OLT.Core
{
    public static class OltHelperPhone
    {

        /// <summary>
        /// Executes <see cref="FormatPhone"/> on the current string and formats.
        /// </summary>
        /// <param name="number">Formats string into 3175551234</param>
        /// <returns>Formatted Phone Number (317) 555-1234</returns>
        public static string ToFormattedPhone(this string number)
        {
            return FormatPhone(number, string.Empty);
        }

        /// <summary>
        /// Executes <see cref="FormatPhone"/> on the current string and formats.
        /// </summary>
        /// <param name="number">Formats string into 3175551234</param>
        /// <param name="ext">Extension</param>
        /// <returns>Formatted Phone Number (317) 555-1234</returns>
        public static string ToFormattedPhone(this string number, string ext)
        {
            return FormatPhone(number, ext);
        }


        /// <summary>
        /// Formats a Domestic Phone Number
        /// </summary>
        /// <param name="number">Formats string into 3175551234</param>
        /// <param name="ext">Extension</param>
        /// <returns>Formatted Phone Number (317) 555-1234</returns>
        public static string FormatPhone(string number, string ext)
        {
            if (string.IsNullOrEmpty(number)) return number;

            var str = string.Empty;

            for (var index = 0; index < number.Length; ++index)
            {
                if (char.IsDigit(number, index))
                    str += number.Substring(index, 1);
            }

            if (string.IsNullOrEmpty(str) || str.Length < 9)
                return number;

            str = $"({str.Substring(0, 3)}) {str.Substring(3, 3)}-{str.Substring(6)}";
            return ext.IsNotEmpty() ? $"{str} x{ext}" : str;
        }

    }
}