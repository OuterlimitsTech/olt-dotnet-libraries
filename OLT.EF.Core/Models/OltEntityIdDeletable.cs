using System.ComponentModel.DataAnnotations;

namespace OLT.Core
{
    public abstract class OltEntityIdDeletable : OltEntityDeletable, IOltEntityId
    {
        [Key]
        public virtual int Id { get; set; }
    }
}