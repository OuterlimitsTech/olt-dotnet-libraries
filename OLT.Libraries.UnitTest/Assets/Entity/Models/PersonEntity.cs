using System.ComponentModel.DataAnnotations;
using OLT.Core;

namespace OLT.Libraries.UnitTest.Assets.Entity.Models
{
    // ReSharper disable once InconsistentNaming
    public class PersonEntity : OltEntityIdDeletable
    {
        [StringLength(100)]
        public string NameFirst { get; set; }
        [StringLength(100)]
        public string NameMiddle { get; set; }
        [StringLength(100)]
        public string NameLast { get; set; }
    }
}