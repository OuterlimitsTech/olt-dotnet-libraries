using System;
using System.Collections.Generic;

namespace OLT.Email
{
    public class OltEmailTemplateRequest : IOltEmailTemplateRequest
    {
        public virtual string TemplateName { get; set; }
        public virtual Guid EmailUid { get; set; }
        public virtual OltEmailRecipients Recipients { get; set; } = new OltEmailRecipients();
        public virtual IEnumerable<OltEmailAttachment> Attachments { get; set; }
    }
}