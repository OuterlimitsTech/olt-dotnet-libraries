using System;

namespace OLT.Core
{
    public interface IOltFileBase64 : IOltResult
    {
        string ContentType { get; set; }
        string FileBase64 { get; set; }
        string FileName { get; set; }
    }
}