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
        /// <returns><param name="col"></param> + 1</returns>
        int Write(ExcelWorksheet worksheet, int col, int row);



        /// <summary>
        /// Writes a row to the worksheet at the given row index and returns the next row idx
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="row">Current Row</param>
        /// <param name="col">Current Col</param>
        /// <param name="rangeAction"></param>
        /// <returns><param name="col"></param> + 1</returns>
        int Write(ExcelWorksheet worksheet, int col, int row, Action<ExcelRange> rangeAction);

        
    }


  
}