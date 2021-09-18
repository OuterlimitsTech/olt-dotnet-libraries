using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OLT.EPPlus;
using Xunit;

namespace OLT.Libraries.UnitTest.OLT.EPPlus
{
    public class General
    {
        private const string WorksheetName = "Test";

        private ExcelPackage CreatePackage()
        {
            return new ExcelPackage();
        }



        [Fact]
        public void ExcelRowEmpty()
        {
            var row = 10;
            using var package = CreatePackage();
            using var worksheet = package.Workbook.Worksheets.Add(WorksheetName);
            Assert.Equal(row + 1, new OltExcelRowEmpty().Write(worksheet, row));
        }

        [Fact]
        public void ExcelFormulaValue()
        {
            var style1 = new OltExcelCellStyle
            {
                Background = Color.Beige,
                HorizontalAlignment = ExcelHorizontalAlignment.Center,
                Bold = true,
                Italic = true,
                Merge = false,
                VerticalAlignment = ExcelVerticalAlignment.Bottom,
                Color = Color.Blue,
                Indent = 15,
                WrapText = false
            };

            var style2 = new OltExcelCellStyle
            {
                Background = Color.Black,
                HorizontalAlignment = ExcelHorizontalAlignment.Right,
                Bold = false,
                Italic = false,
                Merge = false,
                VerticalAlignment = ExcelVerticalAlignment.Justify,
                Color = Color.White,
                Indent = 15,
                WrapText = false
            };

            const double expected = 25;
            const double expectedRange2 = 50;
            using var package = CreatePackage();
            var worksheet = package.Workbook.Worksheets.Add(WorksheetName);
            var rows = new List<IOltExcelRowWriter>
            {
                new OltExcelRowWriter
                {
                    Cells = new List<IOltExcelCellWriter> { new OltExcelCellWriter { Value = 10 } }
                },
                new OltExcelRowWriter
                {
                    Cells = new List<IOltExcelCellWriter> { new OltExcelCellWriter(15, style1) }
                },
                new OltExcelRowWriter
                {
                    Cells = new List<IOltExcelCellWriter> { new OltExcelFormulaValue("SUM(A1:A2)") }
                },
                new OltExcelRowWriter
                {
                    Cells = new List<IOltExcelCellWriter> { new OltExcelFormulaValue("=SUM(A1:A3)", style2) }
                }
            };

            var rowIdx = 1;
            rows.ForEach(row =>
            {
                rowIdx = row.Write(worksheet, rowIdx);
            });

            
            //package.SaveAs(new FileInfo(@$"D:\test_{Guid.NewGuid()}.xlsx"));

            var rng1 = worksheet.Cells[3, 1];
            var rng2 = worksheet.Cells[4, 1];
            rng1.Calculate();
            rng2.Calculate();
            Assert.Equal(expected, rng1.Value);
            Assert.Equal(expectedRange2, rng2.Value);
        }
    }
}
