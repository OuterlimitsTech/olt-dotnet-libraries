using OfficeOpenXml;

namespace OLT.EPPlus
{
    /// <summary>
    /// Adds empty Row to worksheet
    /// </summary>
    public class OltExcelRowEmpty : IOltExcelRowWriter
    {
        /// <summary>
        /// Adds empty Row to worksheet
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="row">Current Row</param>
        /// <returns><paramref name="row"/> + 1</returns>
        public virtual int Write(ExcelWorksheet worksheet, int row)
        {
            return row + 1;
        }
    }
}