using OLT.Core;

namespace OLT.Libraries.UnitTest.Assets.Entity.Models.GeneralCode
{
    public class Sex : CodeTable, IOltEntityCodeValueEnum
    {
        public override CodeTableTypes CodeTableTypeEnum => CodeTableTypes.SexTypes;
    }
}