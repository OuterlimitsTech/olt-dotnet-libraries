using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace System
{


    /// <summary>
    /// Extends <see cref="string"/>.
    /// </summary>
    public static partial class OltStringExtensions
    {


        public static string Base64Encode(this string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(this string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }


        public static string CleanForSearch(this string param)
        {
            var result = param.Trim()
                .Replace("\"", string.Empty)
                .Replace("*", string.Empty)
                .Replace(")", string.Empty)
                .Replace("&", string.Empty)
                .Replace("$", string.Empty)
                .Replace("(", string.Empty)
                .Replace("#", string.Empty)
                .Replace(".", string.Empty)
                .Replace(",", " ");

            return Regex.Replace(result, @"\s+", " ");  //Remove double spaces
        }

        public static List<string> ToWords(this string param)
        {
            return param.Split(' ', '　').ToList();
        }

        #region [ Left, Right, Mid ]

        /// <summary>
        /// Get substring of specified number of characters on the right.
        /// </summary>
        /// <param name="value">Extends <see cref="string"/>.</param>
        /// <param name="length">Number of characters from the right to return</param>
        /// <returns>string containing only <paramref name="length"/> characters.</returns>
        public static string Right(this string value, int length)
        {
            if (value.IsEmpty()) return value;

            if (value.Length < length)
            {
                return value;
            }

            return value.Substring(value.Length - length);
        }


        /// <summary>
        /// Get substring of specified number of characters on the right.
        /// </summary>
        public static string Left(this string value, int length)
        {
            if (value.IsEmpty()) return value;
            if (value.Length < length) return value;
            return value.Substring(0, length);
        }

        #endregion

        public static bool IsGuid(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            Guid temp;
            return Guid.TryParse(value, out temp);
        }

        public static bool IsDate(this string date)
        {
            DateTime temp;
            return DateTime.TryParse(date, out temp);
        }

        public static bool IsNumeric(this string value)
        {
            long temp;
            return long.TryParse(value, out temp);
        }

        private static readonly Regex DigitsOnly = new Regex(@"[^\d]");
        private static readonly Regex DecimalDigitsOnly = new Regex(@"[^\d\.]");

        public static string StripNonNumeric(this string root)
        {
            if (string.IsNullOrEmpty(root))
            {
                return root;
            }

            return DigitsOnly.Replace(root, "");
        }

        public static string StripNonNumeric(this string root, bool allowDecimal)
        {
            if (string.IsNullOrEmpty(root))
            {
                return root;
            }

            if (allowDecimal)
            {
                return DecimalDigitsOnly.Replace(root, "");    
            }

            return root.StripNonNumeric();
        }



        /// <summary>
        /// Will transform "some $ugly ###url wit[]h spaces" into "some-ugly-url-with-spaces"
        /// </summary>
        public static string Slugify(this string phrase, int maxLength = 50)
        {
            string str = phrase.ToLower();

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
 


        public static DateTime? ToDate(this string self)
        {
            DateTime value;
            if (string.IsNullOrWhiteSpace(self) || !DateTime.TryParse(self, out value))
                return null;
            return value;
        }

        #region [ ToGuid ]

        /// <summary>
        /// Converts <see cref="string"/> to <see cref="System.Guid"/>
        /// </summary>
        /// <param name="self">Extends <see cref="string"/>.</param>
        /// <returns>Returns converted value to <see cref="System.Guid"/>, if cast fails, Empty Guid </returns>
        public static Guid ToGuid(this string self)
        {
            Guid value;
            if (String.IsNullOrWhiteSpace(self) || !Guid.TryParse(self, out value))
                return Guid.Empty;
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
            Guid value;
            if (String.IsNullOrWhiteSpace(self) || !Guid.TryParse(self, out value))
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
            int value;
            if (String.IsNullOrWhiteSpace(self) || !Int32.TryParse(self, out value))
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
            int value;
            if (String.IsNullOrWhiteSpace(self) || !Int32.TryParse(self, out value))
                return defaultValue;
            return value;
        }

        /// <summary>
        /// Converts <see cref="long"/> to <see cref="int"/>
        /// </summary>
        /// <param name="self">Extends <see cref="long"/>.</param>
        /// <param name="defaultValue">value returned if cast fails.</param>
        /// <returns>Returns converted value to <see cref="int"/>. if cast fails, null int</returns>
        public static int ToInt(this long self, int defaultValue)
        {
            int value;
            if (!Int32.TryParse(self.ToString(), out value))
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
            long value;
            if (String.IsNullOrWhiteSpace(self) || !Int64.TryParse(self, out value))
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
            long value;
            if (String.IsNullOrWhiteSpace(self) || !Int64.TryParse(self, out value))
                return defaultValue;
            return value;
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
            decimal value;
            if (String.IsNullOrWhiteSpace(self) || !decimal.TryParse(self, out value))
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
            decimal value;
            if (String.IsNullOrWhiteSpace(self) || !decimal.TryParse(self.StripNonNumeric(true), out value))
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
            double value;
            if (String.IsNullOrWhiteSpace(self) || !double.TryParse(self, out value))
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
            double value;
            if (String.IsNullOrWhiteSpace(self) || !double.TryParse(self.StripNonNumeric(true), out value))
                return defaultValue;
            return value;
        }

        #endregion

        #region [ Boolean ]

        public static bool IsBool(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            bool temp;
            return bool.TryParse(value, out temp);
        }

        public static bool? ToBool(this string self)
        {
            bool value;
            if (String.IsNullOrWhiteSpace(self) || !Boolean.TryParse(self, out value))
                return null;
            return value;
        }
        
        public static bool ToBool(this string self, bool defaultValue)
        {
            bool value;
            if (String.IsNullOrWhiteSpace(self) || !Boolean.TryParse(self, out value))
                return defaultValue;
            return value;
        }

        #endregion

        /// <summary>
        /// Converts a hexadecimal string to a byte array. Used to convert encryption key values from the configuration
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static byte[] FromHexToByte(this string hexString)
        {
            var returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        public static string Reverse(this string value)
        {
            var charArray = new char[value.Length];
            int len = value.Length - 1;
            for (int i = 0; i <= len; i++)
                charArray[i] = value[len - i];
            return new string(charArray);

        }

        public static bool StartsWithAny(this string self, params string[] comparisons)
        {
            foreach (string x in comparisons)
            {
                if (self.StartsWith(x))
                    return true;
            }

            return false;
        }

        public static bool EqualsAny(this string self, params string[] comparisons)
        {
            foreach (string x in comparisons)
            {
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

        public static object DBNullIfEmpty(this string self, Func<string, string> process)
        {
            if (string.IsNullOrEmpty(self))
                return DBNull.Value;

            return process(self);
        }

        public static object DBNullOnCriteria(this string self, Func<string, bool> compare)
        {
            if (compare(self))
                return DBNull.Value;

            return self;
        }

        public static string DefaultIfNull(this string self, string defaultValue = "")
        {
            if (self.IsEmpty())
                return defaultValue;

            return self;
        }

        /// <summary>
        /// Executes <see cref="string.IsNullOrEmpty"/> on the current string.
        /// </summary>
        /// <param name="value">Extends <see cref="string"/>.</param>
        /// <returns>True if the current instance is an empty string or is null.</returns>
        public static bool IsEmpty(this string value)
        {
            return String.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Executes <see cref="string.IsNullOrEmpty"/> on the current string and reverses the result.
        /// </summary>
        /// <param name="value">Extends <see cref="string"/>.</param>
        /// <returns>False if the current instance is an empty string or is null.</returns>
        public static bool IsNotEmpty(this string value)
        {
            return !String.IsNullOrEmpty(value);
        }


        /// <summary>
        /// Executes <see cref="string.Compare(string,string,System.StringComparison)" /> on the current string using <see cref="StringComparison.OrdinalIgnoreCase"/>
        /// to do a case-insensitive comparison against the parameter.
        /// </summary>
        /// <param name="leftHandValue">The current instance of <see cref="string"/>.</param>
        /// <param name="rightHandValue">The <see cref="string"/> to compare against.</param>
        /// <returns>
        /// 0 if the strings are the same regardless of case. 
        /// Less than 0 if the current <see cref="string"/> is less than the parameter. 
        /// Greater than 0 is the parameters is less than the current <see cref="string"/>.
        /// </returns>
        public static int DictionaryCompareTo(this string leftHandValue, string rightHandValue)
        {
            return String.Compare(leftHandValue, rightHandValue, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Executes <see cref="string.Format(System.IFormatProvider,string,object[])"/> using the current string as the format. Uses <see cref="CultureInfo.InvariantCulture"/>.
        /// </summary>
        /// <param name="format">The current instance of <see cref="string"/>.</param>
        /// <param name="args">A param array of <see cref="object"/> used to replace tokens in the current string.</param>
        /// <returns>The current string with tokens replaced by the args.</returns>
        public static string ToFormat(this string format, params object[] args)
        {
            return String.Format(CultureInfo.InvariantCulture, format, args);
        }

        /// <summary>
        /// Executes <see cref="string.Format(System.IFormatProvider,string,object[])"/> using the current string as the format.  The passed <see cref="CultureInfo"/> is used for the <see cref="IFormatProvider"/>.
        /// </summary>
        /// <param name="format">The current instance of <see cref="string"/>.</param>
        /// <param name="cultureInfo">A <see cref="CultureInfo"/>.</param>
        /// <param name="args">A param array of <see cref="object"/> used to replace tokens in the current string.</param>
        /// <returns>The current string with tokens replaced by the args.</returns>
        public static string ToFormat(this string format, CultureInfo cultureInfo, params object[] args)
        {
            return String.Format(cultureInfo, format, args);
        }

        /// <summary>
        /// Executes <see cref="string.Join(string,string[])"/> on the current <see cref="IEnumerable{T}"/> of strings.
        /// </summary>
        /// <param name="list">The current <see cref="IEnumerable{T}"/> of strings.</param>
        /// <param name="separator">A <see cref="string"/> containing the value that will be placed between each <see cref="string"/> in the collection.</param>
        /// <returns>A <see cref="string"/> containing the joined strings.</returns>
        public static string Join(this IEnumerable<string> list, string separator)
        {
            return String.Join(separator, new List<string>(list).ToArray());
        }

        public static string Append(this string input, string text)
        {
            return input.Append(text, ", ");
        }

        public static string Append(this string input, string text, string seperator)
        {
            if (input.IsNotEmpty())
                input += seperator;
            input += text;
            return input;
        }

        /// <summary>
        /// Converts the input string into a proper case format where the first letter is caps and the rest is lower
        /// </summary>
        /// <param name="input">The <see cref="String" /> to perform the action on</param>
        /// <returns>A Proper cased <see cref="String" /></returns>
        public static string ToProperCase(this string input)
        {
            if (input.IsEmpty()) return String.Empty;
            if (input.Length == 1) return input.ToUpper();
            return input.Substring(0, 1).ToUpper() + input.Substring(1).ToLower();
        }

        /// <summary>
        /// Returns the last part of the supplied string by the requested length
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="length">The length of the number of characters to return</param>
        /// <returns>
        ///   <see cref="String"/>
        /// </returns>
        public static string Tail(this string input, int length)
        {
            return length > input.Length ? input : input.Substring(input.Length - length);
        }

        /// <summary>
        /// Inserts a space where the Pascal Case cap letters occur
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>
        ///   <see cref="String"/>
        /// </returns>
        public static string ToSentenceCase(this string str)
        {
            if (str == null || String.IsNullOrWhiteSpace(str))
            {
                return str;
            }
            return Regex.Replace(str, "[a-z][A-Z]", m => m.Value[0] + " " + char.ToLower(m.Value[1]));
        }

        /// <summary>
        /// returns the first part specified input based on the requested length
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static string Head(this string input, int length)
        {
            return length > input.Length ? input : input.Substring(0, length);
        }

        public static TextReader ToTextReader(this string value)
        {
            StringReader sr = new StringReader(value);
            return sr;
        }

        #region [ Memory Stream ]

        public static System.IO.MemoryStream ToMemoryStream(string fileName)
        {
            if (!System.IO.File.Exists(fileName))
            {
                return null;
            }

            byte[] file = System.IO.File.ReadAllBytes(fileName);
            return ToMemoryStream(file);
        }

        public static System.IO.MemoryStream ToMemoryStream(this byte[] stream)
        {

            var memStream = new System.IO.MemoryStream();
            memStream.Write(stream, 0, stream.Length);
            return memStream;



        }

        public static bool ToFile(this System.IO.MemoryStream stream, string saveToFileName)
        {

            if (System.IO.File.Exists(saveToFileName))
            {
                System.IO.File.Delete(saveToFileName);
            }


            var fileOutput = System.IO.File.Create(saveToFileName, (stream.Length - 1).ToInt(0));
            stream.WriteTo(fileOutput);
            fileOutput.Close();
            return true;
        }


        public static bool ToFile(this byte[] stream, string saveToFileName)
        {

            if (System.IO.File.Exists(saveToFileName))
            {
                System.IO.File.Delete(saveToFileName);
            }

            var fileOutput = System.IO.File.Create(saveToFileName, stream.Length - 1);
            fileOutput.Write(stream, 0, stream.Length - 1);
            fileOutput.Close();
            return true;
            

        }


        public static Byte[] FileToBytes(string fileName)
        {

            if (!System.IO.File.Exists(fileName))
            {
                return Array.Empty<Byte>();
            }

            var info = new System.IO.FileInfo(fileName);
            var fStream = info.OpenRead();

            var fileData = new byte[fStream.Length];

            fStream.Read(fileData, 0, (fStream.Length).ToInt(0));
            fStream.Close();

            return fileData;
        }


        public static byte[] ToBytes(this System.IO.Stream stream)
        {
            byte[] val = new byte[stream.Length];
            int idx = 0;
            stream.Read(val, idx, (int)stream.Length);
            stream.Close();
            return val;
        }

     

        #endregion

        public static byte[] ToBytes(this string value)
        {
            var encoding = new System.Text.ASCIIEncoding();
            return encoding.GetBytes(value);
        }

        /// <summary>
        /// Adds a double single quote for every single quote to allow for proper SQL Server
        /// This is also used by extensions ToDelimitedString
        /// </summary>
        /// <param name="value">Extends <see cref="string"/>.</param>
        /// <returns></returns>
        private static string DuplicateTicksForSql(this string value)
        {
            return value.Replace("'", "''");
        }

        /// <summary>
        /// Takes a List collection of string and returns a delimited string.  Note that it's easy to create a huge list that won't turn into a huge string because
        /// the string needs contiguous memory.
        /// </summary>
        /// <param name="list">The input List collection of string objects</param>
        /// <param name="qualifier">
        /// The default delimiter. Using a colon in case the List of string are file names,
        /// since it is an illegal file name character on Windows machines and therefore should not be in the file name anywhere.
        /// </param>
        /// <param name="insertSpaces">Whether to insert a space after each separator</param>
        /// <returns>A delimited string</returns>
        /// <remarks>This was implemented pre-linq</remarks>
        public static string ToDelimitedString(this List<string> list, string delimiter = ":", bool insertSpaces = false, string qualifier = "", bool duplicateTicksForSQL = false)
        {
            var result = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                string initialStr = duplicateTicksForSQL ? list[i].DuplicateTicksForSql() : list[i];
                result.Append((qualifier == string.Empty) ? initialStr : string.Format("{1}{0}{1}", initialStr, qualifier));
                if (i < list.Count - 1)
                {
                    result.Append(delimiter);
                    if (insertSpaces)
                    {
                        result.Append(' ');
                    }
                }
            }
            return result.ToString();
        }
    }
}
