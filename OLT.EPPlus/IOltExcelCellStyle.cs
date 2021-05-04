using OfficeOpenXml;

namespace OLT.EPPlus
{
    public interface IOltExcelCellStyle
    {
        /// <summary>
        /// Sets Style to given range of cells
        /// </summary>
        /// <param name="range"></param>
        void ApplyStyle(ExcelRange range);
    }
}