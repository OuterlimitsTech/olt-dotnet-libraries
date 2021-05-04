using System;
using System.Linq;

namespace OLT.Core
{
    public enum OltFtsWildCardType
    {
        None,
        BeginsWith,
        EndsWith,
        Contains
    }

    public class OltFullTextSearchUtil
    {

        /// <summary>
        /// Preps string for EF.Functions.Contains(p.Field, search)
        /// queryable = queryable.Where(p => EF.Functions.Contains(p.NameTextSearch, text));
        /// </summary>
        /// <param name="search">Value to be parsed.  Each "word" will parsed into a SQL FTS string</param>
        /// <param name="wildCardType">Determines where "*" goes</param>
        /// <param name="matchAllWords">When true, "and" separator is used versus "or"</param>
        /// <returns>SQL FTS String</returns>
        public static string Contains(string search, OltFtsWildCardType wildCardType = OltFtsWildCardType.None, bool matchAllWords = false)
        {
            return Convert(search, wildCardType, matchAllWords);
        }
        
        /// <summary>
        /// Preps string for EF.Functions.FreeText(p.Field, search)
        /// queryable = queryable.Where(p => EF.Functions.FreeText(p.NameTextSearch, text));
        /// </summary>
        /// <param name="search">Value to be parsed.  Each "word" will parsed into a SQL FTS string</param>
        /// <param name="matchAllWords">When true, "and" separator is used versus "or"</param>
        /// <returns>SQL FTS String</returns>
        public static string FreeText(string search, bool matchAllWords = false)
        {
            return Convert(search, OltFtsWildCardType.None, matchAllWords);
            //return $"({FullTextFreeText}{Convert(search, FtsWildCardType.None, matchAllWords)})";
        }

        ////public static string FreeTextAll(string search, bool matchAllWords = false)
        ////{
        ////    return Convert(search, FtsWildCardType.None, matchAllWords);
        ////    //return $"({FullTextFreeTextAll}{Convert(search, FtsWildCardType.None, matchAllWords)})";
        ////}

        private static string Convert(string search, OltFtsWildCardType wildCardType, bool matchAllWords)
        {
            search = search.CleanForSearch();

            if (string.IsNullOrWhiteSpace(search))
            {
                return search;
            }



            if (!search.Contains(" "))
            {
                return FormatWordWildCard(search, wildCardType);
            }

            ////Pre-formatted
            //if (search.StartsWith("\"") && search.EndsWith("\""))
            //{
            //    return search;
            //}

            var words = search.Split(' ', '　').ToList();

            return matchAllWords
                ? string.Join(" and ", words.Where(c => c != "and").Select(word => FormatWordWildCard(word, wildCardType)))
                : string.Join(" or ", words.Where(c => c != "or").Select(word => FormatWordWildCard(word, wildCardType)));
        }

        private static string FormatWordWildCard(string word, OltFtsWildCardType wildCardType)
        {
            switch (wildCardType)
            {

                case OltFtsWildCardType.BeginsWith:
                    return $"\"{word}*\"";

                case OltFtsWildCardType.EndsWith:
                    return $"\"*{word}\"";

                case OltFtsWildCardType.Contains:
                    return $"\"*{word}*\"";

                case OltFtsWildCardType.None:
                    return word;

                default:
                    return word;
            }
        }
    }
}