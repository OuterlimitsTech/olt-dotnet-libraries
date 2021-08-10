using System;
using OLT.Core;

namespace OLT.Email
{
    public interface IOltEmailRequest : IOltRequest
    {
        Guid EmailUid { get; }
        OltEmailRecipients Recipients { get; }
    }
}