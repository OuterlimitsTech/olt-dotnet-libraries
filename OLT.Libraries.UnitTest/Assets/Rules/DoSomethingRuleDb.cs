using System.Linq;
using OLT.Core;

namespace OLT.Libraries.UnitTest.Assets.Rules
{
    public interface IDoSomethingRuleDb : IOltRuleAction<DoSomethingRuleDbRequest>
    {

    }

    public class DoSomethingRuleDb : OltRuleAction<DoSomethingRuleDbRequest>, IDoSomethingRule, IDoSomethingRuleDb
    {
        public override IOltResultValidation Validate(DoSomethingRuleDbRequest request)
        {
            if (request.Value.People.Any())
            {
                return Valid;
            }
            return BadRequest(OltValidationSeverityTypes.Error, "No People");
        }

        public override IOltResult Execute(DoSomethingRuleDbRequest request)
        {
            if (request.Value.People.Any())
            {
                return Success;
            }
            return new OltResultFailure("Nothing to Process");
        }

    }
}