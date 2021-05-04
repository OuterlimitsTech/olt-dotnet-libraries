using OLT.Core;

namespace OLT.Email
{
    public class OltEmailCalendarAttachment : IOltEmailAttachment
    {
        public virtual string ContentType => OltDefaults.CalendarInvite.TextCalendar;
        public virtual string FileName => OltDefaults.CalendarInvite.FileName;
        public virtual byte[] Bytes { get; set; }
    }
}