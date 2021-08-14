using System.ComponentModel;
using OLT.Core;

namespace OLT.Libraries.UnitTest.OLT.Shared
{
    public enum TestEnumTypes
    {
        [Code("test-1", 1000)]
        [Description("Test 1")]
        Test1 = 1000,
    }
}