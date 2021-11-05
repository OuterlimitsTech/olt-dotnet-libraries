using System;
using OfficeOpenXml;

namespace OLT.EPPlus
{
    public abstract class OltExcelCellWriter<T> : IOltExcelCellWriter
    {
        protected OltExcelCellWriter()
        {
        }

        protected OltExcelCellWriter(T value)
        {
            Value = value;
        }

        protected OltExcelCellWriter(T value, IOltExcelCellStyle style)
        {
            Value = value;
            Style = style;
        }

        public virtual IOltExcelCellStyle Style { get; set; }

        public virtual T Value { get; set; }


        /// <summary>
        /// writes row at given index and increments 
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns>Next Col Index</returns>
        public virtual int Write(ExcelWorksheet worksheet, int col, int row)
        {
            var colName = OltExcelPackageHelpers.ColumnIndexToColumnLetter(col);

            using (var range = worksheet.Cells[$"{colName}{row}"])
            {
                Write(range);
                ApplyStyle(range);
            }
            return col + 1;
        }

        /// <summary>
        /// Writes Formula to given Range
        /// </summary>
        /// <param name="range"></param>
        public virtual void Write(ExcelRange range)
        {
            range.Value = Value;
        }

        /// <summary>
        /// Writes Formula to given Range
        /// </summary>
        /// <param name="range"></param>
        public virtual void ApplyStyle(ExcelRange range)
        {
            Style?.ApplyStyle(range);
        }
    }

    /// <summary>
    /// new OltExcelCellWriter(value)
    /// </summary>
    public class OltExcelCellWriter : OltExcelCellWriter<object>
    {

        public OltExcelCellWriter() : base()
        {
        }

        public OltExcelCellWriter(object value) : base(value)
        {
        }

        public OltExcelCellWriter(object value, IOltExcelCellStyle style) : base(value, style)
        {
        }
    }


}