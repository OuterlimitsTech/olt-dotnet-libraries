using System.Text.RegularExpressions;

namespace OLT.Core
{
    public static class OltRegExPatterns
    {
        public static readonly Regex Spaces = new Regex(@"\s+"); //Looks for Spaces
        public static readonly Regex DigitsOnly = new Regex(@"[^\d]");
        public static readonly Regex DecimalDigitsOnly = new Regex(@"[^\d\.]");
    }

}