////using System.Collections.Generic;
////using OfficeOpenXml;
////using OLT.Libraries.UnitTest.Assets.FileBuilder;
////using OLT.Libraries.UnitTest.Assets.Models;
////using Xunit;

////namespace OLT.Libraries.UnitTest.OLT.EPPlus
////{
////    public class Exporter
////    {

////        [Fact]
////        public void FileBuilder()
////        {
////            ExcelPackage.LicenseContext = LicenseContext.Commercial;

////            var list = new List<PersonDto>
////            {
////                UnitTestHelper.CreatePersonDto(),
////                UnitTestHelper.CreatePersonDto(),
////                UnitTestHelper.CreatePersonDto(),
////                UnitTestHelper.CreatePersonDto(),
////            };

////            var builder = new TestFileBuilder();
////            var result = builder.Build(new TestFileBuilderRequest {Data = list});
            
////            Assert.True(result.FileBase64.Length > 0);
////        }

////    }
////}