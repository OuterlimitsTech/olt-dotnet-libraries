using System.ComponentModel;
using OLT.Core;

namespace OLT.Libraries.UnitTest.Assets.Enums
{
    public enum StatusTypes
    {
        [Description("All Active")]
        [Code("active")]
        Active = 4500,

        [Description("InActive")]
        [Code("inactive")]
        InActive = 4501,

    }
}