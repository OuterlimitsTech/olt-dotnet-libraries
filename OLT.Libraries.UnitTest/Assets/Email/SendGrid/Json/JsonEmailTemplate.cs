using System.Collections.Generic;
using System.Linq;
using OLT.Core;
using OLT.Email;

namespace OLT.Libraries.UnitTest.Assets.Email.SendGrid.Json
{
    public class EmailDataCommunicationJson
    {
        public string Body1 { get; set; }
        public string Body2 { get; set; }
    }

    public class EmailDataBuildVersionJson
    {
        public string Version { get; set; }
    }

    public class EmailDataJson
    {
        public OltPersonName Recipient { get; set; } = new OltPersonName();
        public EmailDataCommunicationJson Communication { get; set; } = new EmailDataCommunicationJson();
        public EmailDataBuildVersionJson Build { get; set; } = new EmailDataBuildVersionJson();
    }

    public class JsonEmailTemplate : OltEmailJsonTemplate<EmailTemplateAddress, EmailDataJson>, IOltEmailTemplate<EmailTemplateAddress>, IOltEmailTemplate
    {
        public override IEnumerable<EmailTemplateAddress> To { get; set; }
        public override EmailDataJson TemplateData { get; set; }
    }
}