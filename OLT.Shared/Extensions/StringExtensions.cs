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

    }
}
