using OLT.Core;

namespace OLT.Email
{
    public class OltEmailCalendarAttachment : IOltEmailAttachment
    {
        protected const string DefaultFileName = "invite.ics";
        protected const string DefaultTextCalendar = "text/calendar";

        public virtual string ContentType => DefaultTextCalendar;
        public virtual string FileName => DefaultFileName;
        public virtual byte[] Bytes { get; set; }
    }
}