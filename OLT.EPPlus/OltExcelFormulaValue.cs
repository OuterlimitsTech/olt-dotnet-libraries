using System;
using OfficeOpenXml;

namespace OLT.EPPlus
{

    /// <summary>
    /// new ExcelFormulaValue("'Performance Worksheet'!AF2")
    /// </summary>
    public class OltExcelFormulaValue : IOltExcelCellWriter
    {
        public OltExcelFormulaValue(string formula)
        {
            Formula = formula;
        }

        public string Formula { get; set; }
        public IOltExcelCellStyle Style { get; set; }

        /// <summary>
        /// Writes a row to the worksheet at the given row index and returns the next row idx
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="row">Current Row</param>
        /// <param name="col">Current Col</param>
        /// <returns><param name="col"></param> + 1</returns>
        public int Write(ExcelWorksheet worksheet, int col, int row)
        {
            return Write(worksheet, col, row, null);
        }

        /// <summary>
        /// Writes a row to the worksheet at the given row index and returns the next row idx
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="row">Current Row</param>
        /// <param name="col">Current Col</param>
        /// <param name="rangeAction"></param>
        /// <returns><param name="col"></param> + 1</returns>
        public virtual int Write(ExcelWorksheet worksheet, int row, int col, Action<ExcelRange> rangeAction)
        {
            var colName = OltExcelPackageHelpers.ColumnIndexToColumnLetter(col);

            using (var r = worksheet.Cells[$"{colName}{row}"])
            {
                r.Formula = Formula;
                rangeAction?.Invoke(r);
            }

            return col + 1;
        }
    }
}