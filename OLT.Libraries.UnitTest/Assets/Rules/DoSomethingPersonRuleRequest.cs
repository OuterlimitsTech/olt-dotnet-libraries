using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Models;

namespace OLT.Libraries.UnitTest.Assets.Rules
{
    public class DoSomethingPersonRuleRequest : OltRequest<PersonAutoMapperModel>
    {
        public DoSomethingPersonRuleRequest(PersonAutoMapperModel value) : base(value)
        {
        }
    }
}