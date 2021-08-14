using OLT.Email;

namespace OLT.Libraries.UnitTest.Assets.Email.SendGrid.Abstract
{
    public interface IEmailTemplateDefinition : IOltEmailTemplate
    {
        SendGridTemplateTypes Type { get; }
    }



    public interface IEmailTemplate : IEmailTemplateDefinition, IOltEmailTemplate<EmailTemplateAddress>
    {

    }


    public abstract class BaseEmailTemplate : OltEmailTagTemplate<EmailTemplateAddress>, IEmailTemplate
    {
        public abstract SendGridTemplateTypes Type { get; }
    }
}