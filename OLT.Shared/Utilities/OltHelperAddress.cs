using System;
using System.Text;

// ReSharper disable once CheckNamespace
namespace OLT.Core
{
    public static class OltHelperAddress
    {
        private const string HtmlLineBreak = "<br>";
        public const string Regex = "\\d{5}(-?\\d{4})?";

        public static string EncodePostalCode(string postalCode)
        {
            if (string.IsNullOrEmpty(postalCode))
            {
                return null;
            }
            var str = new StringBuilder();
            for (var index = 0; index < postalCode.Length; ++index)
            {
                if (char.IsDigit(postalCode, index))
                {
                    str.Append(postalCode.Substring(index, 1));
                }
            }
            return str.ToString();
        }

        public static string DecodePostalCode(string postalCode)
        {
            if (string.IsNullOrEmpty(postalCode) || postalCode.Trim() == string.Empty)
            {
                return string.Empty;
            }
            postalCode = postalCode.Trim();
            return postalCode.Length == 9 ? $"{(object)postalCode.Substring(0, 5)}-{(object)postalCode.Substring(5)}" : postalCode;
        }

        private static string FormatAddress(string street, string city, string state, string postalCode, string lineBreak)
        {
            var stringBuilder = new StringBuilder();
            if (!string.IsNullOrEmpty(street))
                stringBuilder.Append(street + lineBreak);
            if (!string.IsNullOrEmpty(city))
                stringBuilder.Append(city);
            if (!string.IsNullOrEmpty(state))
            {
                if (!string.IsNullOrEmpty(city))
                    stringBuilder.Append(", " + state);
                else
                    stringBuilder.Append(state);
            }
            postalCode = EncodePostalCode(postalCode);
            if (!string.IsNullOrEmpty(postalCode))
                stringBuilder.Append(" " + DecodePostalCode(postalCode));
            return stringBuilder.ToString();
        }

        public static string FormatBlockAddress(string street1, string street2, string city, string state, string postalCode, string lineBreak)
        {
            if (!string.IsNullOrEmpty(street2))
                street1 = street1 + lineBreak + street2;
            return FormatAddress(street1, city, state, postalCode, lineBreak);
        }


        public static string FormatBlockAddress(string street1, string street2, string city, string state, string postalCode)
        {

            if (!string.IsNullOrEmpty(street2))
                street1 = street1 + Environment.NewLine + street2;
            return FormatAddress(street1, city, state, postalCode, Environment.NewLine);
        }

        public static string FormatHtmlBlockAddress(string street1, string street2, string city, string state, string postalCode)
        {

            if (!string.IsNullOrEmpty(street2))
                street1 = street1 + HtmlLineBreak + street2;
            return FormatAddress(street1, city, state, postalCode, HtmlLineBreak);
        }


    }
}