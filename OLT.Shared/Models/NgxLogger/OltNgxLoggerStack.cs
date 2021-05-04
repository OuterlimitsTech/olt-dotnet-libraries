using System;
using System.Collections.Generic;

namespace OLT.Core
{
    public class OltNgxLoggerStack
    {
        public virtual int ColumnNumber { get; set; }
        public virtual int LineNumber { get; set; }
        public virtual string FileName { get; set; }
        public virtual string FunctionName { get; set; }
        public virtual string Source { get; set; }

        public override string ToString()
        {
            var list = new List<string>
            {
                $"Column Number: {ColumnNumber}",
                $"Line Number: {LineNumber}",
                $"FileName: {FileName}",
                $"FunctionName: {FunctionName}",
                $"Source: {Source}",
            };
            return string.Join(Environment.NewLine, list);
        }
    }
}
