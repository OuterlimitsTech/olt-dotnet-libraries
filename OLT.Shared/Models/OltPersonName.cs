namespace OLT.Core
{
    public class OltPersonName : IOltPersonName
    {
        public virtual string First { get; set; }
        public virtual string Middle { get; set; }
        public virtual string Last { get; set; }
        public virtual string Suffix { get; set; }
        public virtual string FullName => System.Text.RegularExpressions.Regex.Replace(($"{First} {Middle} {Last} {Suffix}").Trim(), @"\s+", " ");
    }
}