﻿
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using OfficeOpenXml;

namespace OLT.EPPlus
{
    public static class OltExcelCsvConverter
    {

        /// <summary>
        /// Converts worksheet to CSV
        /// </summary>
        /// <param name="worksheet"></param>
        /// <returns><see cref="OltExcelCsvWorksheet"/></returns>
        public static OltExcelCsvWorksheet ConvertToCsv(this ExcelWorksheet worksheet)
        {
            var maxColumnNumber = worksheet.Dimension.End.Column;
            var currentRow = new List<string>(maxColumnNumber);
            var totalRowCount = worksheet.Dimension.End.Row;
            var currentRowNum = 1;

            var memory = new MemoryStream();

            using (var writer = new StreamWriter(memory, Encoding.ASCII))
            {
                while (currentRowNum <= totalRowCount)
                {
                    BuildRow(worksheet, currentRow, currentRowNum, maxColumnNumber);
                    WriteRecordToFile(currentRow, writer, currentRowNum, totalRowCount);
                    currentRow.Clear();
                    currentRowNum++;
                }
            }

            return new OltExcelCsvWorksheet
            {
                Name = worksheet.Name,
                Csv = memory.ToArray()
            };
        }

        /// <summary>
        /// Converts all Worksheets in Workbook to a CSV byte array
        /// </summary>
        /// <param name="package"></param>
        /// <returns>Returns List of <see cref="OltExcelCsvWorksheet"/></returns>
        public static List<OltExcelCsvWorksheet> ConvertToCsv(this ExcelPackage package)
        {
            var list = new List<OltExcelCsvWorksheet>();

            package.Workbook.Worksheets
                .ToList()
                .ForEach(worksheet =>
                {
                    list.Add(worksheet.ConvertToCsv());
                });

            return list;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="record">List of cell values</param>
        /// <param name="sw">Open Writer to file</param>
        /// <param name="rowNumber">Current row num</param>
        /// <param name="totalRowCount"></param>
        /// <remarks>Avoiding writing final empty line so bulk import processes can work.</remarks>
        private static void WriteRecordToFile(List<string> record, StreamWriter sw, int rowNumber, int totalRowCount)
        {
            var commaDelimitedRecord = record.ToDelimitedString(",");

            if (rowNumber == totalRowCount)
            {
                sw.Write(commaDelimitedRecord);
            }
            else
            {
                sw.WriteLine(commaDelimitedRecord);
            }
        }

        private static void BuildRow(ExcelWorksheet worksheet, List<string> currentRow, int currentRowNum, int maxColumnNumber)
        {
            for (int i = 1; i <= maxColumnNumber; i++)
            {
                var cell = worksheet.Cells[currentRowNum, i];
                if (cell == null)
                {
                    // add a cell value for empty cells to keep data aligned.
                    AddCellValue(string.Empty, currentRow);
                }
                else
                {
                    AddCellValue(GetCellText(cell), currentRow);
                }
            }
        }

        /// <summary>
        /// Can't use .Text: http://epplus.codeplex.com/discussions/349696
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private static string GetCellText(ExcelRangeBase cell)
        {
            return cell.Value == null ? string.Empty : cell.Value.ToString();
        }

        private static void AddCellValue(string value, List<string> record)
        {
            record.Add(string.Format("{0}{1}{0}", '"', value));
        }

        /// <summary>
        /// Takes a List collection of string and returns a delimited string.  Note that it's easy to create a huge list that won't turn into a huge string because
        /// the string needs contiguous memory.
        /// </summary>
        /// <param name="list">The input List collection of string objects</param>
        /// <param name="qualifier">
        /// The default delimiter. Using a colon in case the List of string are file names,
        /// since it is an illegal file name character on Windows machines and therefore should not be in the file name anywhere.
        /// </param>
        /// <param name="insertSpaces">Whether to insert a space after each separator</param>
        /// <returns>A delimited string</returns>
        /// <remarks>This was implemented pre-linq</remarks>
        private static string ToDelimitedString(this List<string> list, string delimiter = ":", bool insertSpaces = false, string qualifier = "")
        {
            var result = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                string initialStr = list[i];
                result.Append((qualifier == string.Empty) ? initialStr : string.Format("{1}{0}{1}", initialStr, qualifier));
                if (i < list.Count - 1)
                {
                    result.Append(delimiter);
                    if (insertSpaces)
                    {
                        result.Append(' ');
                    }
                }
            }
            return result.ToString();
        }
    }
}
