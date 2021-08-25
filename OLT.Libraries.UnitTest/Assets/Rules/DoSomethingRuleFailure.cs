using OLT.Core;

namespace OLT.Libraries.UnitTest.Assets.Rules
{
    public class DoSomethingRuleFailure : OltRuleAction<DoSomethingPersonRuleRequest>, IDoSomethingRule
    {
        public override IOltResultValidation Validate(DoSomethingPersonRuleRequest request)
        {
            return Valid;
        }

        public override IOltResult Execute(DoSomethingPersonRuleRequest request)
        {
            var validation = Validate(request);
            if (validation.Invalid)
            {
                return validation;
            }
            throw Failure("This is a test");
        }
    }
}