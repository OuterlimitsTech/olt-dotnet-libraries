using System;

namespace OLT.Core
{
    public interface IOltFileBase64 : IOltResult
    {
        string ContentType { get; }
        string FileBase64 { get; }
        string FileName { get; }
    }
}