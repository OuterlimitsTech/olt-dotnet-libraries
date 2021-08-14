using OLT.Email;
using System.Collections.Generic;

namespace OLT.Libraries.UnitTest.Assets.Email.SendGrid
{

    public class TagEmailTemplate : OltSingleEmailTagTemplate<EmailTemplateAddress>, IOltSingleEmailTemplate<EmailTemplateAddress>, IOltEmailTemplate
    {
        public string Body1 { get; set; }
        public string Body2 { get; set; }
        public SendGridTemplateTypes Type => SendGridTemplateTypes.TagTemplate;
        
        public override IEnumerable<OltEmailTag> Tags => new List<OltEmailTag>
        {
            new OltEmailTag { Tag = nameof(To.ToName.First), Value = To.ToName.First },
            new OltEmailTag { Tag = nameof(To.Name), Value = To.Name },
            new OltEmailTag { Tag = nameof(Body1), Value = Body1 },
            new OltEmailTag { Tag = nameof(Body2), Value = Body2 },
        };

    }

}