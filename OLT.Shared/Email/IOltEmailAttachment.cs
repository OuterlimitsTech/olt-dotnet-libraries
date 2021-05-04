namespace OLT.Email
{
    public interface IOltEmailAttachment
    {
        string ContentType { get; }
        string FileName { get; }
        byte[] Bytes { get; }
    }
}