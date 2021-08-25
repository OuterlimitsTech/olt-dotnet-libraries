using OLT.Core;

namespace OLT.Libraries.UnitTest.Assets.Rules
{
    public class DoSomethingRuleOne : OltRuleAction<DoSomethingRuleRequest>, IDoSomethingRule
    {
        public override IOltResultValidation Validate(DoSomethingRuleRequest request)
        {
            return Valid;
        }

        public override IOltResult Execute(DoSomethingRuleRequest request)
        {
            return Success;
        }

    }
}