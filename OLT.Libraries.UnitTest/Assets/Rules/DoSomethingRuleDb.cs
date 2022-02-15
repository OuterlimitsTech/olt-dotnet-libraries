using System.Linq;
using OLT.Core;

namespace OLT.Libraries.UnitTest.Assets.Rules
{
    public class DoSomethingRuleDb : OltRuleAction<DoSomethingRuleContextRequest>, IDoSomethingRule, IDoSomethingRuleDb
    {
        public override IOltResultValidation Validate(DoSomethingRuleContextRequest request)
        {
            if (request.Context.People.Any())
            {
                return Valid;
            }
            return BadRequest("No People");
        }

        public override IOltResult Execute(DoSomethingRuleContextRequest request)
        {
            if (request.Context.People.Any())
            {
                return Success;
            }

            throw Failure("Nothing to Process");
        }

    }
}