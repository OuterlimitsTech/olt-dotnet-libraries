using System;
using System.Collections.Generic;
using OfficeOpenXml;

namespace OLT.EPPlus
{
    public static class OltExcelPackageHelpers
    {

        /// <summary>
        /// Get Column Index for heading text
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="heading">string to find in the first row</param>
        /// <param name="row">row to search (Default 1)</param>
        /// <returns>index or throws exception</returns>
        public static int? GetColIdx(this ExcelWorksheet worksheet, string heading, int? row = 1)
        {
            var rowIdx = row.GetValueOrDefault(1);
            for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
            {
                if (worksheet.Cells[rowIdx, col].Text.Equals(heading, StringComparison.OrdinalIgnoreCase))
                {
                    return col;
                }
            }

            return null;
        }

        /// <summary>
        /// Writes columns starting at column index zero (0) at row index
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="columns"></param>
        /// <param name="row"></param>
        public static void Write(this ExcelWorksheet worksheet, List<IOltExcelColumn> columns, int row)
        {
            Write(worksheet, columns, row, null);
        }

        /// <summary>
        /// Writes columns starting at column index zero (0) at row index
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="columns"></param>
        /// <param name="row"></param>
        /// <param name="rangeAction">void rangeAction(ExcelRange range)</param>
        public static void Write(this ExcelWorksheet worksheet, List<IOltExcelColumn> columns, int row, Action<ExcelRange> rangeAction)
        {
            var idx = 1;
            columns.ForEach(col =>
            {
                idx = col.Write(worksheet, idx, row);
            });
        }

        public static int ColumnLetterToColumnIndex(string columnLetter)
        {
            columnLetter = columnLetter.ToUpper();
            int sum = 0;

            for (int i = 0; i < columnLetter.Length; i++)
            {
                sum *= 26;
                sum += (columnLetter[i] - 'A' + 1);
            }
            return sum;
        }

        public static string ColumnIndexToColumnLetter(int colIndex)
        {
            int div = colIndex;
            string colLetter = string.Empty;

            while (div > 0)
            {
                var mod = (div - 1) % 26;
                colLetter = (char)(65 + mod) + colLetter;
                div = (div - mod) / 26;
            }
            return colLetter;
        }
    }
}