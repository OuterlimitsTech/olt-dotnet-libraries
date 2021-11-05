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
            using var package = CreatePackage();
            using var worksheet = package.Workbook.Worksheets.Add(WorksheetName);
            var compare = new OltExcelFormulaValue("=SUM(A1:A3)");
            Assert.Equal("SUM(A1:A3)", compare.Value);
            Assert.Equal("SUM(A1:A3)", compare.Formula);
        }

        [Fact]
        public void ExcelCellWriters()
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

            const string generalFormat= "General";
            const string format = "000";
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
                    Cells = new List<IOltExcelCellWriter> { new OltExcelFormulaValue("SUM(A2:A3)") }
                },
                new OltExcelRowWriter
                {
                    Cells = new List<IOltExcelCellWriter> { new OltExcelFormulaValue("=SUM(A2:A4)", style2) }
                }
            };

            var rowIdx = 2;
            var cols = new List<IOltExcelColumn>
            {
                new OltExcelColumn { Heading ="Heading1", Format = format },
                new OltExcelColumn { Heading ="Heading2" }
            };
            worksheet.Write(cols, 1);

            rows.ForEach(row =>
            {
                rowIdx = row.Write(worksheet, rowIdx);
            });


            
            Assert.Equal(worksheet.Cells["A1"].Value, cols[0].Heading);
            Assert.Equal(worksheet.Cells["B1"].Value, cols[1].Heading);

            var rng1 = worksheet.Cells[4, 1];
            var rng2 = worksheet.Cells[5, 1];
            rng1.Calculate();
            rng2.Calculate();
            Assert.Equal(expected, rng1.Value);
            Assert.Equal(expectedRange2, rng2.Value);
            Assert.Equal(worksheet.Column(1).Style.Numberformat.Format, format);
            Assert.Equal(worksheet.Column(2).Style.Numberformat.Format, generalFormat);
        }
    }
}
