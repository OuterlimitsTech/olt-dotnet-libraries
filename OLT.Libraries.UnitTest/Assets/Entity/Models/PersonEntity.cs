using OLT.Core;

namespace OLT.Libraries.UnitTest.Assets.Entity.Models
{
    // ReSharper disable once InconsistentNaming
    public class PersonEntity : OltEntityIdDeletable
    {
        public string NameFirst { get; set; }
        public string NameMiddle { get; set; }
        public string NameLast { get; set; }
    }
}