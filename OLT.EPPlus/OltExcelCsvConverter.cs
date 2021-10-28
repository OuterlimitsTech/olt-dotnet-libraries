
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
                AddCellValue(GetCellText(worksheet.Cells[currentRowNum, i]), currentRow);
            }
        }

        /// <summary>
        /// Can't use .Text: http://epplus.codeplex.com/discussions/349696
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static string GetCellText(this ExcelRangeBase cell)
        {
            return cell?.Value?.ToString();
        }

        private static void AddCellValue(string value, List<string> record)
        {
            record.Add(string.Format("{0}{1}{0}", '"', value));
        }


    }
}
