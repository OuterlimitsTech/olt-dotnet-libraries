using System.Collections.Generic;

namespace OLT.Email
{
    public interface IOltEmailCalendarRequest : IOltEmailRequest
    {
        OltEmailCalendarAttachment CalendarInvite { get; set; }
        string Subject { get; set; }
        string Body { get; set; }
    }
}