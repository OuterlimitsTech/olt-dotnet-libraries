using System;
using OfficeOpenXml;

namespace OLT.EPPlus
{
    /// <summary>
    /// new OltExcelCellWriter(value)
    /// </summary>
    public class OltExcelCellWriter : IOltExcelCellWriter
    {
        public OltExcelCellWriter()
        {
        }

        public OltExcelCellWriter(object value)
        {
            Value = value;
        }

        public OltExcelCellWriter(object value, IOltExcelCellStyle style)
        {
            Value = value;
            Style = style;
        }

        public object Value { get; set; }

        public IOltExcelCellStyle Style { get; set; }

        /// <summary>
        /// writes row at given index and increments 
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns>Next Col Index</returns>
        public int Write(ExcelWorksheet worksheet, int col, int row)
        {
            return Write(worksheet, col, row, null);
        }

        public int Write(ExcelWorksheet worksheet, int col, int row, Action<ExcelRange> rangeAction)
        {
            var colName = OltExcelPackageHelpers.ColumnIndexToColumnLetter(col);

            using (var range = worksheet.Cells[$"{colName}{row}"])
            {
                range.Value = Value;
                Style?.ApplyStyle(range);
                rangeAction?.Invoke(range);
            }

            return col + 1;
        }
    }


    /// <summary>
    /// new OltExcelCellWriter(value)
    /// </summary>
    public class OltExcelCellWriter<T> : OltExcelCellWriter
    {
        public OltExcelCellWriter() : base()
        {
        }

        public OltExcelCellWriter(T metaData, string value) : base(value)
        {
            MetaData = metaData;
        }

        public OltExcelCellWriter(T metaData, string value, IOltExcelCellStyle style) : base(value, style)
        {
            MetaData = metaData;
        }


        /// <summary>
        /// Used to store data related to the cell.  This is not written to excel and is for internal processing
        /// </summary>
        public T MetaData { get; set; }

    }

}