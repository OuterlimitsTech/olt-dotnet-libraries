using System.Collections.Generic;
using OfficeOpenXml;

namespace OLT.EPPlus
{
    /// <summary>
    /// Writes all Cells to the Row
    /// </summary>
    public class OltExcelRowWriter : IOltExcelRowWriter
    {

        public OltExcelRowWriter()
        {
        }


        public List<IOltExcelCellWriter> Cells { get; set; } = new List<IOltExcelCellWriter>();

        /// <summary>
        /// writes row at given index and increments starting with column 1 of the cells
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="row"></param>
        /// <returns><paramref name="row"/> + 1</returns>
        public virtual int Write(ExcelWorksheet worksheet, int row)
        {
            return Write(worksheet, row, 1);
        }

        /// <summary>
        /// writes row at given index and increments
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="row"></param>
        /// <param name="col">column to start at</param>
        /// <returns><paramref name="row"/> + 1</returns>
        public virtual int Write(ExcelWorksheet worksheet, int row, int col)
        {
            Cells.ForEach(cell =>
            {
                col = cell.Write(worksheet, col, row);
            });

            return row + 1;
        }
    }

}