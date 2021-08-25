using OLT.Core;

namespace OLT.Libraries.UnitTest.Assets.Rules
{
    public class DoSomethingRuleIntValue : OltRuleAction<DoSomethingRuleIntRequest>, IDoSomethingRule
    {
        public override IOltResultValidation Validate(DoSomethingRuleIntRequest request)
        {
            return request.Value > 5 ? Valid : BadRequest(OltSeverityTypes.Error, "Invalid Value, must be greater than 5");
        }

        public override IOltResult Execute(DoSomethingRuleIntRequest request)
        {
            return Success;
        }

    }
}