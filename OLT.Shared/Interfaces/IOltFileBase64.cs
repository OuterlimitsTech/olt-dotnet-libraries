using System;

namespace OLT.Core
{
    public interface IOltFileBase64
    {
        string ContentType { get; }
        string FileBase64 { get; }
        string FileName { get; }
    }
}