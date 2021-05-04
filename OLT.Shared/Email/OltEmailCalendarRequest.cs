using System;

namespace OLT.Email
{
    public class OltEmailCalendarRequest : IOltEmailCalendarRequest
    {
        public Guid EmailUid { get; set; }
        public OltEmailRecipients Recipients { get; set; }
        public OltEmailCalendarAttachment CalendarInvite { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}