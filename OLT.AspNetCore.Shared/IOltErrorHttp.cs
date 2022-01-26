using System;
using System.Collections.Generic;
using System.Text;

namespace OLT.Core
{
    public interface IOltErrorHttp
    {
        string Message { get; }
        IEnumerable<string> Errors { get; }
        string ToJson();
    }
}
