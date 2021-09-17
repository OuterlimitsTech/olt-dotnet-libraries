using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using OfficeOpenXml;
using OLT.Core;
using OLT.EPPlus;
using Xunit;

namespace OLT.Libraries.UnitTest.OLT.EPPlus
{
    public class CsvTests
    {
        [Fact]
        public void ConvertToCsvTest()
        {
            var exportDirectory = Path.Combine(Path.GetTempPath(), $"_{Guid.NewGuid()}");
            var files = new List<string>();

            try
            {
                if (!Directory.Exists(exportDirectory))
                {
                    Directory.CreateDirectory(exportDirectory);
                }
                var fileStream = this.GetType().Assembly.GetEmbeddedResourceStream("ImportTest.xlsx");
                ExcelPackage.LicenseContext = LicenseContext.Commercial;
                ExcelPackage package = new ExcelPackage(fileStream);
                var worksheets = package.ConvertToCsv();
                package.Dispose();

                worksheets.ForEach(item =>
                {
                    var fileName = Path.Combine(exportDirectory,  $"{Guid.NewGuid()}_{item.Name}.csv");
                    item.Csv.ToFile(fileName);
                    files.Add(fileName);
                });

                files.ForEach(file =>
                {
                    Assert.True(File.Exists(file));
                });
                
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (Directory.Exists(exportDirectory))
                {
                    Directory.Delete(exportDirectory, true);
                }
            }

        }
    }
}