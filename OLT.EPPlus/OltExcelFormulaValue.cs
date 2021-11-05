using System;
using OfficeOpenXml;

namespace OLT.EPPlus
{

    /// <summary>
    /// new ExcelFormulaValue("'Performance Worksheet'!AF2")
    /// </summary>
    public class OltExcelFormulaValue : OltExcelCellWriter<string>
    {
        public OltExcelFormulaValue(string formula) : base(FormatValue(formula))
        {
        }

        public OltExcelFormulaValue(string formula, IOltExcelCellStyle style) : base(FormatValue(formula), style)
        {

        }

        private static string FormatValue(string formula)
        {
            return formula.StartsWith("=") ? formula.TrimStart('=') : formula;
        }

        public string Formula => Value;

        /// <summary>
        /// Writes Formula to given Range
        /// </summary>
        /// <param name="range"></param>
        public override void Write(ExcelRange range)
        {
            range.Formula = Formula;
        }
    }
}