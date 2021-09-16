using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using OLT.Core;

namespace OLT.Libraries.UnitTest.Assets.Entity.Models.GeneralCode
{
    [Table(nameof(CodeTableType), Schema = "Code")]
    public class CodeTableType : BaseCodeValueEntity, IOltEntityCodeValueEnum
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int Id { get; set; }

        public virtual List<CodeTable> Codes { get; set; } = new List<CodeTable>();

    }
}