using System;
using OfficeOpenXml;

namespace OLT.EPPlus
{
    public interface IOltExcelCellWriter
    {
        IOltExcelCellStyle Style { get; set; }

        /// <summary>
        /// Writes a row to the worksheet at the given row index and returns the next row idx
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="row">Current Row</param>
        /// <param name="col">Current Col</param>
        /// <returns>col + 1</returns>
        int Write(ExcelWorksheet worksheet, int col, int row);


        /// <summary>
        /// Writes value to given Range
        /// </summary>
        /// <param name="range"></param>
        void Write(ExcelRange range);
    }


  
}