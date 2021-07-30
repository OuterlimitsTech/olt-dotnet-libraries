using OLT.Core;

namespace OLT.Libraries.UnitTest.Models.Entity
{
    public class OltPersonEntity : OltEntityIdDeletable
    {
        public string NameFirst { get; set; }
        public string NameMiddle { get; set; }
        public string NameLast { get; set; }
    }
}