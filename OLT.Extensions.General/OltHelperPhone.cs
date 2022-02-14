using System;
using System.Text;

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

            //var builder = new StringBuilder();

            //for (var index = 0; index < number.Length; ++index)
            //{
            //    if (char.IsDigit(number, index))
            //    {
            //        builder.Append(number.Substring(index, 1));
            //    }
            //}

            //var str = builder.ToString();

            var str = OltStringExtensions.StripNonNumeric(number);
            
            if (string.IsNullOrEmpty(str) || str.Length < 9)
                return number;

            var extValue = OltStringExtensions.StripNonNumeric(ext);
            str = $"({str.Substring(0, 3)}) {str.Substring(3, 3)}-{str.Substring(6)}";
            return !string.IsNullOrWhiteSpace(extValue) ? $"{str} x{extValue}" : str;
        }

    }
}
