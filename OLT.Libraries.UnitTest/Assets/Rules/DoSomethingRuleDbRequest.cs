using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity;

namespace OLT.Libraries.UnitTest.Assets.Rules
{
    public class DoSomethingRuleDbRequest : OltRequest<SqlDatabaseContext>
    {
        public DoSomethingRuleDbRequest(SqlDatabaseContext value) : base(value)
        {
        }
    }
}