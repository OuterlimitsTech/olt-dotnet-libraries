using System.ComponentModel;
using System.Runtime.Serialization;
using OLT.Core;

namespace OLT.Libraries.UnitTest.Assets.Entity.Models.GeneralCode
{
    public enum CodeTableTypes
    {
        [Description("Genders")]
        [Code("gender")]
        [EnumMember(Value = "gender")]
        GenderTypes = 2300,


        [Description("Sex")]
        [Code("sex")]
        [EnumMember(Value = "sex")]
        SexTypes = 3800,
    }
}