using System.Collections.Generic;

namespace OLT.Email
{
    public interface IOltEmailAttachmentRequest : IOltEmailRequest
    {
        IEnumerable<OltEmailAttachment> Attachments { get; }
    }
}