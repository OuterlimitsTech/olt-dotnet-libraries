using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity;

namespace OLT.Libraries.UnitTest.Assets.Rules
{
    public class DoSomethingRuleContextRequest : OltRequestContext<SqlDatabaseContext>
    {
        public DoSomethingRuleContextRequest(SqlDatabaseContext context) : base(context)
        {
        }
    }
}