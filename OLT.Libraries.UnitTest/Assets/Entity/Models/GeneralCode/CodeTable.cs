using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OLT.Libraries.UnitTest.Assets.Entity.Models.GeneralCode
{
    //TPH Model
    [Table(nameof(CodeTable), Schema = "Code")]
    public abstract class CodeTable : BaseCodeValueEntity
    {
        protected CodeTable()
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            CodeTableTypeId = Convert.ToInt32(CodeTableTypeEnum);
        }

        public int? CodeTableTypeId { get; set; }
        public virtual CodeTableType CodeTableType { get; set; }


        public int? ParentCodeId { get; set; }
        public virtual CodeTable ParentCode { get; set; }

        [NotMapped]
        public abstract CodeTableTypes CodeTableTypeEnum { get; }

        public string GetDiscriminator()
        {
            return this.GetType().Name;
        }
    }
}