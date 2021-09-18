using System;
using OfficeOpenXml;

namespace OLT.EPPlus
{

    /// <summary>
    /// new ExcelFormulaValue("'Performance Worksheet'!AF2")
    /// </summary>
    public class OltExcelFormulaValue : OltExcelCellWriter
    {
        public OltExcelFormulaValue(string formula) 
        {
            Formula = formula.StartsWith("=") ? formula.TrimStart('=') : formula;
        }

        public OltExcelFormulaValue(string formula, IOltExcelCellStyle style) : this(formula)
        {
            Style = style;
        }

        public new object Value => Formula;
        public string Formula { get; }

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