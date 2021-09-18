using System;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace OLT.EPPlus
{
    /// <summary>
    /// new ExcelPackageColumn { Heading = "Secondary Parent Email", Width = 23.43m }
    /// </summary>
    public class OltExcelColumn : OltExcelCellWriter, IOltExcelColumn
    {
        public OltExcelColumn()
        {

        }

        public string Heading { get; set; }
        public decimal? Width { get; set; }
        public string Format { get; set; }

        public override int Write(ExcelWorksheet worksheet, int col, int row)
        {
            if (!string.IsNullOrWhiteSpace(Format))
            {
                worksheet.Column(col).Style.Numberformat.Format = Format;
            }

            if (Width.HasValue)
            {
                worksheet.Column(col).Width = Convert.ToDouble(Width.Value);
            }

            return base.Write(worksheet, col, row);
        }
    }

    /// <summary>
    /// new OltExcelColumn<MyOtherData> { Heading = "Secondary Parent Email", Width = 23.43m }
    /// </summary>
    public class OltExcelColumn<T> : OltExcelCellWriter<T>
    {
        
    }
}