using System.ComponentModel.DataAnnotations;
using OLT.Core;

namespace OLT.Libraries.UnitTest.Assets.Entity.Models.GeneralCode
{
    public abstract class BaseCodeValueEntity : OltEntityIdDeletable, IOltEntityCodeValue
    {
        [StringLength(50)]
        public virtual string Code { get; set; }

        [StringLength(255), Required]
        public virtual string Name { get; set; }

        public short SortOrder { get; set; } = 9999;
    }
}