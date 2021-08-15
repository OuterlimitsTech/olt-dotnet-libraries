namespace OLT.Email
{
    public class OltEmailAttachment : IOltEmailAttachment
    {
        public virtual string ContentType { get; set; }
        public virtual string FileName { get; set; }
        public virtual byte[] Bytes { get; set; }
    }
}