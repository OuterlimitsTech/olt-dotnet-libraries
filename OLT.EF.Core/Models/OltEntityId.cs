using System.ComponentModel.DataAnnotations;

namespace OLT.Core
{
    public abstract class OltEntityId : OltEntityAudit, IOltEntityId
    {
        [Key]
        public virtual int Id { get; set; }
    }
}