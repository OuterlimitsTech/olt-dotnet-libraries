using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
        public static byte[] ToBytes<T>(this string value) where T : System.Text.Encoding, new()
        {
            var encoding = new T();
            return encoding.GetBytes(value);
        }

        public static byte[] ToASCIIBytes(this char value)
        {
            return value.ToString().ToASCIIBytes();
        }

        public static byte[] ToASCIIBytes(this string value)
        {
            return ToBytes<System.Text.ASCIIEncoding>(value);
        }

        public static byte[] ToUTF8Bytes(this char value)
        {
            return value.ToString().ToUTF8Bytes();
        }

        public static byte[] ToUTF8Bytes(this string value)
        {            
            return ToBytes<System.Text.UTF8Encoding>(value);
        }

        #region [ Empty/Null Checks ]

        /// <summary>
        /// checks string. If null returns default
        /// </summary>
        /// <param name="self">Extends <see cref="string"/>.</param>
        /// <param name="defaultValue">Default value return if null</param>
        /// <returns>return string or default value if null</returns>
        public static string GetValueOrDefault(this string self, string defaultValue)
        {
            return self ?? defaultValue;
        }

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

        public static string RemoveDoubleSpaces(this string self)
        {
            if (string.IsNullOrEmpty(self))
            {
                return self;
            }
            return OltRegExPatterns.Spaces.Replace(self.Trim(), " ");
        }

        /// <summary>
        /// Removes special characters from <see cref="string"/> using <see cref="OltRegExPatterns.RemoveSpecialCharacters"/>
        /// </summary>
        /// <param name="self">Extends <see cref="string"/>.</param>
        public static string RemoveSpecialCharacters(this string self)
        {
            if (string.IsNullOrEmpty(self))
            {
                return self;
            }            
            return OltRegExPatterns.RemoveSpecialCharacters.Replace(self, string.Empty);
        }

        public static string CleanForSearch(this string self)
        {
            return self?.RemoveSpecialCharacters().Trim().RemoveDoubleSpaces();
        }

        public static List<string> ToWords(this string self)
        {
            return self?.RemoveDoubleSpaces().Split(' ').ToList();
        }


        #region [ Left, Head, Right, Tail ]

        /// <summary>
        /// Get substring of specified number of characters on the right.
        /// </summary>
        /// <param name="self">Extends <see cref="string"/>.</param>
        /// <param name="length">Number of characters from the right to return</param>
        /// <returns>string containing only <paramref name="length"/> characters.</returns>
        public static string Right(this string self, int length)
        {
            return Tail(self, length);
        }

        /// <summary>
        /// Returns the last part of the supplied string by the requested length
        /// </summary>
        /// <param name="self">Extends <see cref="string"/>.</param>
        /// <param name="length">The length of the number of characters to return</param>
        /// <returns>
        ///   <see cref="string"/>
        /// </returns>
        public static string Tail(this string self, int length)
        {
            return length > self?.Length ? self : self?.Substring(self.Length - length);
        }


        /// <summary>
        /// returns the first part specified input based on the requested length
        /// </summary>
        /// <param name="self">Extends <see cref="string"/>.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static string Head(this string self, int length)
        {
            return length > self?.Length ? self : self?.Substring(0, length);
        }

        /// <summary>
        /// Get substring of specified number of characters on the right.
        /// </summary>
        /// <param name="self">Extends <see cref="string"/>.</param>
        /// <param name="length">the number of characters to take</param>
        public static string Left(this string self, int length)
        {
            return Head(self, length);
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

            return Guid.TryParse(self, out var temp);
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
            return long.TryParse(self, out var longVal) || double.TryParse(self, out var doubleVal);
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
            if (hexString == null)
            {
                return false;
            }
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

        #region [ Boolean ]

        public static readonly string[] BoolTrueStrings = { "1", "yes", "y" };
        public static readonly string[] BoolFalseStrings = { "0", "no", "n" };

        public static bool IsBool(this string self)
        {
            return ToBoolInternal(self, out var value);
        }

        public static bool? ToBool(this string self)
        {
            ToBoolInternal(self, out var value);
            return value;
        }

        public static bool ToBool(this string self, bool defaultValue)
        {
            ToBoolInternal(self, out var value);
            return value.GetValueOrDefault(defaultValue);
        }

        private static bool ToBoolInternal(string self, out bool? value)
        {
            if (string.IsNullOrWhiteSpace(self))
            {
                value = null;
                return false;
            }

            var eval = self.ToLower();
            if (BoolTrueStrings.Any(compareTo => string.Equals(eval, compareTo, StringComparison.OrdinalIgnoreCase)))
            {
                value = true;
                return true;
            }

            if (BoolFalseStrings.Any(compareTo => string.Equals(eval, compareTo, StringComparison.OrdinalIgnoreCase)))
            {
                value = false;
                return true;
            }

            if (bool.TryParse(eval, out var val))
            {
                value = val;
                return true;
            }

            value = null;
            return false;
        }


        #endregion

        #region [ ToDecimal ]

        /// <summary>
        /// Converts <see cref="string"/> to <see cref="decimal"/>
        /// </summary>
        /// <param name="self">Extends <see cref="string"/>.</param>
        /// <returns>Returns converted value to <see cref="decimal"/>, if cast fails, null <see cref="decimal"/></returns>
        public static decimal? ToDecimal(this string self)
        {
            if (string.IsNullOrWhiteSpace(self) || !decimal.TryParse(self, out var value))
                return null;
            return value;
        }

        /// <summary>
        /// Converts <see cref="string"/> to <see cref="int"/>
        /// </summary>
        /// <param name="self">Extends <see cref="string"/>.</param>
        /// <param name="defaultValue">value returned if cast fails.</param>
        /// <returns>Returns converted value to <see cref="decimal"/>. if cast fails, return defaultValue</returns>
        public static decimal ToDecimal(this string self, decimal defaultValue)
        {
            if (string.IsNullOrWhiteSpace(self) || !decimal.TryParse(self.StripNonNumeric(true), out var value))
                return defaultValue;
            return value;
        }

        #endregion

        #region [ ToDouble ]

        /// <summary>
        /// Converts <see cref="string"/> to <see cref="double"/>
        /// </summary>
        /// <param name="self">Extends <see cref="string"/>.</param>
        /// <returns>Returns converted value to <see cref="double"/>, if cast fails, null <see cref="double"/></returns>
        public static double? ToDouble(this string self)
        {
            if (string.IsNullOrWhiteSpace(self) || !double.TryParse(self, out var value))
                return null;
            return value;
        }

        /// <summary>
        /// Converts <see cref="string"/> to <see cref="int"/>
        /// </summary>
        /// <param name="self">Extends <see cref="string"/>.</param>
        /// <param name="defaultValue">value returned if cast fails.</param>
        /// <returns>Returns converted value to <see cref="double"/>. if cast fails, return defaultValue</returns>
        public static double ToDouble(this string self, double defaultValue)
        {
            if (string.IsNullOrWhiteSpace(self) || !double.TryParse(self.StripNonNumeric(true), out var value))
                return defaultValue;
            return value;
        }

        #endregion
        
        #region [ ToInt ]

        /// <summary>
        /// Converts <see cref="string"/> to <see cref="int"/>
        /// </summary>
        /// <param name="self">Extends <see cref="string"/>.</param>
        /// <returns>Returns converted value to <see cref="int"/>, if cast fails, null int</returns>
        public static int? ToInt(this string self)
        {
            if (string.IsNullOrWhiteSpace(self) || !int.TryParse(self, out var value))
                return null;
            return value;
        }

        /// <summary>
        /// Converts <see cref="string"/> to <see cref="int"/>
        /// </summary>
        /// <param name="self">Extends <see cref="string"/>.</param>
        /// <param name="defaultValue">value returned if cast fails.</param>
        /// <returns>Returns converted value to <see cref="int"/>. if cast fails, null int</returns>
        public static int ToInt(this string self, int defaultValue)
        {
            if (string.IsNullOrWhiteSpace(self) || !int.TryParse(self, out var value))
                return defaultValue;
            return value;
        }


        #endregion

        #region [ ToLong ]

        /// <summary>
        /// Converts <see cref="string"/> to <see cref="long"/>
        /// </summary>
        /// <param name="self">Extends <see cref="string"/>.</param>
        /// <returns>Returns converted value to <see cref="long"/>, if cast fails, null long</returns>
        public static long? ToLong(this string self)
        {
            if (string.IsNullOrWhiteSpace(self) || !long.TryParse(self, out var value))
                return null;
            return value;
        }

        /// <summary>
        /// Converts <see cref="string"/> to <see cref="long"/>
        /// </summary>
        /// <param name="self">Extends <see cref="string"/>.</param>
        /// <param name="defaultValue">value returned if cast fails.</param>
        /// <returns>Returns converted value to <see cref="long"/>. if cast fails, defaultValue</returns>
        public static long ToLong(this string self, long defaultValue)
        {
            if (string.IsNullOrWhiteSpace(self) || !long.TryParse(self, out var value))
                return defaultValue;
            return value;
        }

        #endregion

        #region [ DateTime ]


        public static bool IsDate(this string date)
        {
            return DateTime.TryParse(date, out var temp);
        }

        public static DateTime? ToDate(this string self)
        {
            if (string.IsNullOrWhiteSpace(self) || !DateTime.TryParse(self, out var value))
                return null;
            return value;
        }

        public static DateTime? ToDate(this string self, DateTime defaultValue)
        {
            return ToDate(self).GetValueOrDefault(defaultValue);
        }

        #endregion

        /// <summary>
        /// Reverses String
        /// </summary>
        /// <param name="self">Extends <see cref="string"/>.</param>
        public static string Reverse(this string self)
        {
            if (string.IsNullOrEmpty(self))
            {
                return self;
            }
            var charArray = new char[self.Length];
            int len = self.Length - 1;
            for (int i = 0; i <= len; i++)
                charArray[i] = self[len - i];
            return new string(charArray);
        }

        public static bool StartsWithAny(this string self, params string[] comparisons)
        {
            if (self == null)
            {
                return false;
            }
            for (int i = 0; i < comparisons.Length; i++)
            {
                string x = comparisons[i];
                if (self.StartsWith(x))
                    return true;
            }

            return false;
        }

        public static bool EqualsAny(this string self, params string[] comparisons)
        {
            if (self == null)
            {
                return false;
            }
            for (int i = 0; i < comparisons.Length; i++)
            {
                string x = comparisons[i];
                if (self.Equals(x))
                    return true;
            }

            return false;
        }

        public static object DBNullIfEmpty(this string self)
        {
            if (string.IsNullOrEmpty(self))
                return DBNull.Value;

            return self;
        }

        /// <summary>
        /// Will transform "some $ugly ###url wit[]h spaces" into "some-ugly-url-with-spaces"
        /// </summary>
        /// <param name="self">Extends <see cref="string"/>.</param>
        /// <param name="maxLength"></param>
        public static string Slugify(this string self, int maxLength = 50)
        {
            if (self.IsEmpty())
            {
                return self;
            }
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

        /// <summary>
        /// Adds a double single quote for every single quote to allow for proper SQL Server
        /// This is also used by extensions ToDelimitedString
        /// </summary>
        /// <param name="value">Extends <see cref="string"/>.</param>
        /// <returns></returns>
        public static string DuplicateTicksForSql(this string value)
        {
            return value?.Replace("'", "''");
        }


        /// <summary>
        /// Inserts a space where the Pascal Case cap letters occur
        /// </summary>
        /// <param name="self"></param>
        /// <returns>
        ///   <see cref="string"/>
        /// </returns>
        public static string ToSentenceCase(this string self)
        {
            return string.IsNullOrWhiteSpace(self) ? self : Regex.Replace(self, "[a-z][A-Z]", m => m.Value[0] + " " + char.ToLower(m.Value[1]));
        }


        /// <summary>
        /// Appends text to string using separator.
        /// </summary>
        /// <param name="self">Extends <see cref="string"/>.</param>
        /// <param name="text">Text to append</param>
        /// <param name="separator"></param>
        /// <returns>A <see cref="string"/> containing the joined strings.</returns>
        public static string Append(this string self, string text, string separator)
        {
            if (self.IsNotEmpty())
                self += separator;
            self += text;
            return self;
        }


        /// <summary>
        /// Converts the input string into a proper case format where the first letter is caps and the rest is lower
        /// </summary>
        /// <param name="input">The <see cref="String" /> to perform the action on</param>
        /// <returns>A Proper cased <see cref="String" /></returns>
        public static string ToProperCase(this string input)
        {
            if (input == null) return null;
            if (input.IsEmpty()) return string.Empty;
            if (input.Length == 1) return input.ToUpper();
            return input.Substring(0, 1).ToUpper() + input.Substring(1).ToLower();
        }


        #region [ Base64 Encode/Decode ]

        public static string Base64Encode(this string plainText)
        {
            if (plainText == null) return null;
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(this string base64EncodedData)
        {
            if (base64EncodedData == null) return null;
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        #endregion

    }
}