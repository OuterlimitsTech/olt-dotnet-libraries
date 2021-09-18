using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using OLT.Core;

// ReSharper disable once CheckNamespace
namespace System
{

    /// <summary>
    /// Extends <see cref="string"/>.
    /// </summary>
    public static class OltStringExtensions
    {



        #region [ Empty/Null Checks ]

        /// <summary>
        /// Executes <see cref="string.IsNullOrEmpty"/> on the current string.
        /// </summary>
        /// <param name="self">Extends <see cref="string"/>.</param>
        /// <returns>True if the current instance is an empty string or is null.</returns>
        public static bool IsEmpty(this string self)
        {
            return string.IsNullOrEmpty(self);
        }

        /// <summary>
        /// Executes <see cref="string.IsNullOrEmpty"/> on the current string and reverses the result.
        /// </summary>
        /// <param name="self">Extends <see cref="string"/>.</param>
        /// <returns>False if the current instance is an empty string or is null.</returns>
        public static bool IsNotEmpty(this string self)
        {
            return !string.IsNullOrEmpty(self);
        }

        #endregion

        public static string RemoveSpecialCharacters(this string self)
        {
            Regex r = new Regex("(?:[^a-z0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            return r.Replace(self, string.Empty);
        }

        public static string CleanForSearch(this string self)
        {
            var result = self.RemoveSpecialCharacters().Trim();
            return Regex.Replace(result, @"\s+", " ");  //Remove double spaces
        }

        public static List<string> ToWords(this string self)
        {
            return self.Split(' ', '　').ToList();
        }


        #region [ Left, Right ]

        /// <summary>
        /// Get substring of specified number of characters on the right.
        /// </summary>
        /// <param name="self">Extends <see cref="string"/>.</param>
        /// <param name="length">Number of characters from the right to return</param>
        /// <returns>string containing only <paramref name="length"/> characters.</returns>
        public static string Right(this string self, int length)
        {
            if (self.IsEmpty()) return self;

            if (self.Length < length)
            {
                return self;
            }

            return self.Substring(self.Length - length);
        }


        /// <summary>
        /// Get substring of specified number of characters on the right.
        /// </summary>
        /// <param name="self">Extends <see cref="string"/>.</param>
        /// <param name="length">the number of characters to take</param>
        public static string Left(this string self, int length)
        {
            if (self.IsEmpty()) return self;
            if (self.Length < length) return self;
            return self.Substring(0, length);
        }

        #endregion

        #region [ Guid ]
        
        /// <summary>
        /// Checks <see cref="string"/> is a <see cref="System.Guid"/>
        /// </summary>
        /// <param name="self">Extends <see cref="string"/>.</param>
        public static bool IsGuid(this string self)
        {
            if (string.IsNullOrWhiteSpace(self))
            {
                return false;
            }

            Guid temp;
            return Guid.TryParse(self, out temp);
        }


        /// <summary>
        /// Converts <see cref="string"/> to <see cref="System.Guid"/>
        /// </summary>
        /// <param name="self">Extends <see cref="string"/>.</param>
        /// <returns>Returns converted value to <see cref="System.Guid"/>, if cast fails, Empty Guid </returns>
        public static Guid? ToGuid(this string self)
        {
            if (string.IsNullOrWhiteSpace(self) || !Guid.TryParse(self, out var value))
                return null;
            return value;
        }

        /// <summary>
        /// Converts <see cref="string"/> to <see cref="System.Guid"/>
        /// </summary>
        /// <param name="self">Extends <see cref="string"/>.</param>
        /// <param name="defaultValue">value returned if cast fails.</param>
        /// <returns>Returns converted value to <see cref="System.Guid"/>. If cast fails, default value</returns>
        public static Guid ToGuid(this string self, Guid defaultValue)
        {
            if (string.IsNullOrWhiteSpace(self) || !Guid.TryParse(self, out var value))
                return defaultValue;
            return value;
        }

        #endregion

        #region [ Numeric ]

        /// <summary>
        /// Checks <see cref="string"/> is of type <see cref="long"/> or <see cref="double"/>
        /// </summary>
        /// <param name="self">Extends <see cref="string"/>.</param>
        public static bool IsNumeric(this string self)
        {
            long longVal;
            double doubleVal;
            return long.TryParse(self, out longVal) || double.TryParse(self, out doubleVal);
        }


        /// <summary>
        /// Removes non numeric values from <see cref="string"/> using <see cref="OltRegExPatterns.DigitsOnly"/>
        /// </summary>
        /// <param name="self">Extends <see cref="string"/>.</param>
        public static string StripNonNumeric(this string self)
        {
            if (string.IsNullOrEmpty(self))
            {
                return self;
            }

            return OltRegExPatterns.DigitsOnly.Replace(self, "");
        }

        /// <summary>
        /// Removes non numeric values from <see cref="string"/>, but keeps decimals when true using <see cref="OltRegExPatterns.DecimalDigitsOnly"/>
        /// </summary>
        /// <param name="self">Extends <see cref="string"/>.</param>
        /// <param name="allowDecimal">Keep decimals</param>
        public static string StripNonNumeric(this string self, bool allowDecimal)
        {
            if (string.IsNullOrEmpty(self))
            {
                return self;
            }

            if (allowDecimal)
            {
                return OltRegExPatterns.DecimalDigitsOnly.Replace(self, "");
            }

            return self.StripNonNumeric();
        }

        #endregion

        #region [ Hex ]

        /// <summary>
        /// Converts a hexadecimal string to a byte array. Used to convert encryption key values from the configuration
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static bool IsHex(this string hexString)
        {
            // For C-style hex notation (0xFF) you can use @"\A\b(0[xX])?[0-9a-fA-F]+\b\Z"
            return System.Text.RegularExpressions.Regex.IsMatch(hexString, @"\A\b[0-9a-fA-F]+\b\Z");
        }

        /// <summary>
        /// Converts a hexadecimal string to a byte array. Used to convert encryption key values from the configuration
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns>byte array or empty if invalid</returns>
        public static byte[] FromHexToByte(this string hexString)
        {
            if (IsHex(hexString))
            {
                var returnBytes = new byte[hexString.Length / 2];
                for (int i = 0; i < returnBytes.Length; i++)
                    returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
                return returnBytes;
            }

            return Array.Empty<byte>();
        }

        #endregion

        /// <summary>
        /// Will transform "some $ugly ###url wit[]h spaces" into "some-ugly-url-with-spaces"
        /// </summary>
        /// <param name="self">Extends <see cref="string"/>.</param>
        /// <param name="maxLength"></param>
        public static string Slugify(this string self, int maxLength = 50)
        {
            string str = self.ToLower();

            // invalid chars, make into spaces
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces/hyphens into one space       
            str = Regex.Replace(str, @"[\s-]+", " ").Trim();
            // cut and trim it
            str = str.Substring(0, str.Length <= maxLength ? str.Length : maxLength).Trim();
            // hyphens
            str = Regex.Replace(str, @"\s", "-");

            return str;
        }


    }
}