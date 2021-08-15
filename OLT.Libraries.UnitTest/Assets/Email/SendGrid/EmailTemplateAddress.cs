using OLT.Core;
using OLT.Email;

namespace OLT.Libraries.UnitTest.Assets.Email.SendGrid
{
    public class EmailTemplateAddress : OltEmailAddress
    {
        public override string Name => ToName.FullName;
        public OltPersonName ToName { get; set; }
    }

}