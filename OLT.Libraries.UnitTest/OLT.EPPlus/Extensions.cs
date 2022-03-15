////using System;
////using System.Collections.Generic;
////using System.IO;
////using System.Linq;
////using System.Reflection;
////using FluentAssertions;
////using OfficeOpenXml;
////using OLT.Core;
////using OLT.EPPlus;
////using Xunit;

////namespace OLT.Libraries.UnitTest.OLT.EPPlus
////{
////    public class Extensions
////    {
////        public Extensions()
////        {
////            ExcelPackage.LicenseContext = LicenseContext.Commercial;
////        }

////        private ExcelPackage GetPackage()
////        {
////            var fileStream = this.GetType().Assembly.GetEmbeddedResourceStream("ImportTest.xlsx");
////            return new ExcelPackage(fileStream);
////        }


////        ////[Fact]
////        ////public void ConvertToCsvNewTest()
////        ////{
////        ////    var exportDirectory = UnitTestHelper.BuildTempPath();
////        ////    var files = new List<string>();

////        ////    try
////        ////    {

////        ////        var format = new ExcelOutputTextFormat
////        ////        {
////        ////            TextQualifier = '"',
////        ////        };

////        ////        using (var package = GetPackage())
////        ////        {
////        ////            package.Workbook.Worksheets
////        ////                .ToList()
////        ////                .ForEach(worksheet =>
////        ////                {
////        ////                    var fileName = Path.Combine(exportDirectory, $"{Guid.NewGuid()}_{worksheet.Name}.csv");
////        ////                    files.Add(fileName);
////        ////                    using var rng = worksheet.Cells[worksheet.Dimension.FullAddress];
////        ////                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(rng.ToText(format));
////        ////                    buffer.ToFile(fileName);

////        ////                });
////        ////        }

////        ////        files.ForEach(file =>
////        ////        {
////        ////            Assert.True(File.Exists(file));
////        ////        });
////        ////    }
////        ////    catch (Exception)
////        ////    {
////        ////        throw;
////        ////    }
////        ////    finally
////        ////    {
////        ////        if (Directory.Exists(exportDirectory))
////        ////        {
////        ////            Directory.Delete(exportDirectory, true);
////        ////        }
////        ////    }

////        ////}

////        [Fact]
////        public void ConvertToCsvTest()
////        {
////            var exportDirectory = UnitTestHelper.BuildTempPath();
////            var files = new List<string>();

////            try
////            {
////                using var package = GetPackage();
////                var worksheets = package.ConvertToCsv();

////                worksheets.ForEach(item =>
////                {
////                    var fileName = Path.Combine(exportDirectory, $"{Guid.NewGuid()}_{item.Name}.csv");
////                    item.Csv.ToFile(fileName);
////                    files.Add(fileName);
////                });

////                files.ForEach(file =>
////                {
////                    Assert.True(File.Exists(file));
////                });
////            }
////            catch (Exception)
////            {
////                throw;
////            }
////            finally
////            {
////                if (Directory.Exists(exportDirectory))
////                {
////                    Directory.Delete(exportDirectory, true);
////                }
////            }

////        }


////        [Fact]
////        public void GetCellText()
////        {
////            using var package = GetPackage();
////            using var worksheet = package.Workbook.Worksheets.First();
////            worksheet.Cells[2, 1].Value = "Hello";
////            Assert.Equal("Hello", worksheet.Cells[2, 1].GetCellText());
////        }

////        [Fact]
////        public void GetCellTextValueNull()
////        {
////            using var package = GetPackage();
////            using var worksheet = package.Workbook.Worksheets.First();
////            worksheet.Cells[2, 1].Value = null;
////            Assert.Null(worksheet.Cells[2, 1].GetCellText());
////        }

////        [Fact]
////        public void GetCellTextWhitespace()
////        {
////            using var package = GetPackage();
////            using var worksheet = package.Workbook.Worksheets.First();
////            worksheet.Cells[2, 1].Value = " Test ";
////            worksheet.Cells[2, 1].GetCellText();
////            Assert.Equal(" Test ", worksheet.Cells[2, 1].GetCellText());
////        }

////        [Fact]
////        public void GetCellTextNumber()
////        {
////            using var package = GetPackage();
////            using var worksheet = package.Workbook.Worksheets.First();
////            worksheet.Cells[2, 1].Value = 1039;
////            Assert.Equal("1039", worksheet.Cells[2, 1].GetCellText());
////        }


////        [Fact]
////        public void GetCellTextNull()
////        {
////            Assert.Null(OltExcelCsvConverter.GetCellText(null));
////        }

////        [Theory]
////        [InlineData("First Name", null, 2)]
////        [InlineData("First Name", 1, 2)]
////        [InlineData("First Name", 2, null)]
////        [InlineData("first Name", 1, 2)]
////        [InlineData("First name", 1, 2)]
////        [InlineData("First Name", 50000, null)]
////        public void GetColIdx(string heading, int? row, int? expected)
////        {
////            using var package = GetPackage();
////            using var worksheet = package.Workbook.Worksheets.First();
////            Assert.Equal(worksheet.GetColIdx(heading, row), expected);
////        }

////        [Theory]
////        [InlineData(1, "A")]
////        [InlineData(33, "AG")]
////        [InlineData(702, "ZZ")]
////        [InlineData(633, "XI")]
////        public void ColumnIndexToColumnLetter(int colIdx, string expected)
////        {
////            var result = OltExcelPackageHelpers.ColumnIndexToColumnLetter(colIdx);
////            Assert.Equal(result, expected);
////        }

        
////        [Theory]
////        [InlineData(0)]
////        [InlineData(703)]
////        [InlineData(50000)]
////        public void ColumnIndexToColumnLetterInvalid(int colIdx)
////        {
////            Action action = () => OltExcelPackageHelpers.ColumnIndexToColumnLetter(colIdx);
////            action.Should()
////                .Throw<ArgumentOutOfRangeException>()
////                .WithMessage("Must be between 1 and 702 (Parameter 'colIndex')");
////        }


////        [Theory]
////        [InlineData("A", 1)]
////        [InlineData("AG", 33)]
////        [InlineData("ZZ", 702)]
////        [InlineData("XZ", 650)]
////        public void ColumnLetterToColumnIndex(string columnLetter, int expected)
////        {
////            var result = OltExcelPackageHelpers.ColumnLetterToColumnIndex(columnLetter);
////            Assert.Equal(result, expected);
////        }

////        [Fact]
////        public void ColumnLetterToColumnIndexNull()
////        {
////            Action action = () => OltExcelPackageHelpers.ColumnLetterToColumnIndex(null);
////            action.Should()
////                .Throw<ArgumentNullException>();
////        }

////        [Theory]
////        [InlineData("AZZ")]
////        [InlineData("A1")]
////        [InlineData("")]
////        public void ColumnLetterToColumnIndexOutofRange(string columnLetter)
////        {
////            Action action = () => OltExcelPackageHelpers.ColumnLetterToColumnIndex(columnLetter);
////            action.Should()
////                .Throw<ArgumentOutOfRangeException>()
////                .WithMessage("Must only be 1-2 alpha characters (Parameter 'columnLetter')");
////        }
////    }
////}