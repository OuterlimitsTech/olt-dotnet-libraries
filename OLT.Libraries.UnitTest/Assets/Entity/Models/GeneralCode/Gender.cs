using OLT.Core;

namespace OLT.Libraries.UnitTest.Assets.Entity.Models.GeneralCode
{
    public class Gender : CodeTable, IOltEntityCodeValueEnum
    {
        public override CodeTableTypes CodeTableTypeEnum => CodeTableTypes.GenderTypes;
    }
}