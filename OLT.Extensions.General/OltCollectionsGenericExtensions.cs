using System.Text;
using System.Threading.Tasks;


// ReSharper disable once CheckNamespace
namespace System.Collections.Generic
{
    public static class OltCollectionsGenericExtensions
    {

        /// <summary>
        /// Executes <see cref="string.Join(string,string[])"/> on the current <see cref="IEnumerable{T}"/> of strings.
        /// </summary>
        /// <param name="list">The current <see cref="IEnumerable{T}"/> of strings.</param>
        /// <param name="separator">A <see cref="string"/> containing the value that will be placed between each <see cref="string"/> in the collection.</param>
        /// <returns>A <see cref="string"/> containing the joined strings.</returns>
        public static string Join(this IEnumerable<string> list, string separator)
        {
            return string.Join(separator, new List<string>(list).ToArray());
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
        /// <param name="delimiter"></param>
        /// <param name="insertSpaces">Whether to insert a space after each separator</param>
        /// <returns>A delimited string</returns>
        /// <remarks>This was implemented pre-linq and <see cref="StringBuilder"/> is faster with a large amount of data</remarks>
        public static string ToDelimitedString(this List<string> list, string delimiter = ":", bool insertSpaces = false, string qualifier = "")
        {
            var result = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                string initialStr = list[i];
                result.Append(qualifier == string.Empty ? initialStr : string.Format("{1}{0}{1}", initialStr, qualifier));
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