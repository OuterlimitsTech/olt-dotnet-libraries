using System.Drawing;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace OLT.EPPlus
{
    /// <summary>
    /// Used to set style on cell or range of cells
    /// </summary>
    public class OltExcelCellStyle : IOltExcelCellStyle
    {
        public int? Indent { get; set; }
        public bool Bold { get; set; }
        public bool Italic { get; set; }
        public bool Merge { get; set; }
        public Color? Color { get; set; }
        public Color? Background { get; set; }
        public bool WrapText { get; set; }
        public ExcelVerticalAlignment? VerticalAlignment { get; set; }
        public ExcelHorizontalAlignment? HorizontalAlignment { get; set; }


        /// <summary>
        /// Sets Style to given range of cells
        /// </summary>
        /// <param name="range"></param>
        public virtual void ApplyStyle(ExcelRange range)
        {
            if (Indent.HasValue)
            {
                range.Style.Indent = Indent.Value;
            }

            if (HorizontalAlignment.HasValue)
            {
                range.Style.HorizontalAlignment = HorizontalAlignment.Value;
            }

            if (VerticalAlignment.HasValue)
            {
                range.Style.VerticalAlignment = VerticalAlignment.Value;
            }

            range.Merge = Merge;
            range.Style.WrapText = WrapText;
            range.Style.Font.Italic = Italic;
            range.Style.Font.Bold = Bold;

            if (Color.HasValue)
            {
                range.Style.Font.Color.SetColor(Color.Value);
            }

            if (Background.HasValue)
            {
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Background.Value);
            }

        }
    }
}