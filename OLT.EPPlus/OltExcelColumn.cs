using System;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace OLT.EPPlus
{
    /// <summary>
    /// new ExcelPackageColumn { Heading = "Secondary Parent Email", Width = 23.43m }
    /// </summary>
    public class OltExcelColumn : IOltExcelColumn
    {
        public string Heading { get; set; }
        public decimal? Width { get; set; }
        public string Format { get; set; }

        public IOltExcelCellStyle Style { get; set; } = new OltExcelCellStyle();

        public virtual int Write(ExcelWorksheet worksheet, int col, int row)
        {
            return Write(worksheet, col, row, null);
        }

        public virtual int Write(ExcelWorksheet worksheet, int col, int row, Action<ExcelRange> rangeAction)
        {
            if (!string.IsNullOrWhiteSpace(Format))
            {
                worksheet.Column(col).Style.Numberformat.Format = Format;
            }

            if (Width.HasValue)
            {
                worksheet.Column(col).Width = Convert.ToDouble(Width.Value);
            }

            var excelCol = OltExcelPackageHelpers.ColumnIndexToColumnLetter(col);

            using (var range = worksheet.Cells[$"{excelCol}{row}"])
            {
                range.Value = Heading;
                Style?.ApplyStyle(range);
                rangeAction?.Invoke(range);
            }

            return col + 1;
        }
    }

    /// <summary>
    /// new ExcelPackageColumn { Heading = "Secondary Parent Email", Width = 23.43m }
    /// </summary>
    public class OltExcelColumn<T> : OltExcelColumn
    {
        /// <summary>
        /// Used to store data related to the column.  This is not written to excel and is for internal processing
        /// </summary>
        public T MetaData { get; set; }
        
    }
}