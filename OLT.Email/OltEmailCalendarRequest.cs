using System;

namespace OLT.Email
{
    public class OltEmailCalendarRequest : IOltEmailCalendarRequest
    {
        public virtual Guid EmailUid { get; set; }
        public virtual OltEmailRecipients Recipients { get; set; }
        public virtual OltEmailCalendarAttachment CalendarInvite { get; set; }
        public virtual string Subject { get; set; }
        public virtual string Body { get; set; }
    }
}