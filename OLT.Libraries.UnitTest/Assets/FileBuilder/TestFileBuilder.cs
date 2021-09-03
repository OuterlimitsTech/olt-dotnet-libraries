using System;
using System.Collections.Generic;
using System.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OLT.Core;
using OLT.EPPlus;

namespace OLT.Libraries.UnitTest.Assets.FileBuilder
{
    public class TestFileBuilder : OltFileBuilder<TestFileBuilderRequest>
    {
        public override string BuilderName => nameof(TestFileBuilder);

        public override IOltFileBase64 Build(TestFileBuilderRequest request)
        {
            using var excelPackage = new ExcelPackage();
            var worksheet = excelPackage.Workbook.Worksheets.Add($"Count {request.Data.Count():N0}");

            worksheet.Write(ExcelColumns, 1);
            var rows = new List<OltExcelRowWriter>();

            request.Data.ForEach(service =>
            {
                var cells = new List<IOltExcelCellWriter>
                {
                    new OltExcelCellWriter(service.PersonId),
                    new OltExcelCellWriter(service.Email),
                    new OltExcelCellWriter(service.First),
                    new OltExcelCellWriter(service.Last),
                };


                rows.Add(new OltExcelRowWriter
                {
                    Cells = cells
                });
            });

            var rowIdx = 2;
            rows.ForEach(row =>
            {
                rowIdx = row.Write(worksheet, rowIdx);
            });

            return new OltFileBase64
            {
                FileName = $"{BuilderName} [{DateTimeOffset.Now:s}].xlsx",
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                FileBase64 = Convert.ToBase64String(excelPackage.GetAsByteArray())
            };
        }


        #region [ Excel ]

        private List<IOltExcelColumn> ExcelColumns
        {
            get
            {
                var columns = new List<IOltExcelColumn>
                {
                    new OltExcelColumn
                    {
                        Heading = "Person Id",
                        Width = 22.5m,
                        Style = new OltExcelCellStyle
                        {
                            HorizontalAlignment = ExcelHorizontalAlignment.Center
                        }
                    },
                    new OltExcelColumn
                    {
                        Heading = "Email",
                        Width = 15.5m,
                        Style = new OltExcelCellStyle
                        {
                            WrapText = true,
                            HorizontalAlignment = ExcelHorizontalAlignment.Left
                        }
                    },
                    new OltExcelColumn
                    {
                        Heading = "First Name",
                        Width = 15.5m,
                        Style = new OltExcelCellStyle
                        {
                            WrapText = true,
                            HorizontalAlignment = ExcelHorizontalAlignment.Justify
                        }
                    },
                    new OltExcelColumn
                    {
                        Heading = "Last Name",
                        Width = 15.5m,
                        Style = new OltExcelCellStyle
                        {
                            WrapText = true,
                            HorizontalAlignment = ExcelHorizontalAlignment.Right
                        }
                    },

                };
                return columns;
            }
        }

        #endregion
    }
}