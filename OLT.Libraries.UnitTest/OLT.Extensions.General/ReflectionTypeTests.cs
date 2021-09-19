using System;
using System.IO;
using System.Reflection;
using Xunit;

namespace OLT.Libraries.UnitTest.OLT.Extensions.General
{
    public class ReflectionTypeTests
    {

        private const string EmbeddedFile = "ImportTest.xlsx";
        private const string EmbeddedCsvFile1 = "ImportTest_Sheet1.csv";
        private const string EmbeddedCsvFile2 = "ImportTest_Sheet2.csv";

        [Theory]
        [InlineData(EmbeddedFile, true)]
        [InlineData("BogusResource.txt", false)]
        public void GetEmbeddedResourceStream(string resourceName, bool expectedResult)
        {
            var dir = UnitTestHelper.BuildTempPath();
            var fileName = Path.Combine(dir, "ToBytes_Import.xlsx");
            try
            {
                var stream = this.GetType().Assembly.GetEmbeddedResourceStream(resourceName);
                stream?.ToBytes().ToFile(fileName);
            }
            catch
            {
                // ignored
            }
            
            
            var result = File.Exists(fileName);
            Assert.Equal(expectedResult, result);
            Directory.Delete(dir, true);
        }

        [Theory]
        [InlineData(EmbeddedFile, true)]
        [InlineData("BogusResource.txt", false)]
        public void EmbeddedResourceToFile(string resourceName, bool expectedResult)
        {
            var dir = UnitTestHelper.BuildTempPath();
            var fileName = Path.Combine(dir, "EmbeddedResourceToFile_Test.xlsx");

            try
            {
                this.GetType().Assembly.EmbeddedResourceToFile(resourceName, fileName);
            }
            catch (Exception exception)
            {
                Assert.Equal(typeof(FileNotFoundException), exception.GetType());
            }

            var result = File.Exists(fileName);
            Assert.Equal(expectedResult, result);

            try
            {
                this.GetType().Assembly.EmbeddedResourceToFile(resourceName, fileName); //Second attempt to delete & recreate
            }
            catch(Exception exception)
            {
                Assert.Equal(typeof(FileNotFoundException), exception.GetType());
            }
            
            result = File.Exists(fileName);
            Assert.Equal(expectedResult, result);

            Directory.Delete(dir, true);
        }

        [Theory]
        [InlineData(EmbeddedCsvFile1, true)]
        [InlineData("BogusResource.txt", false)]
        public void GetEmbeddedResourceString(string resourceName, bool expectedResult)
        {
            if (expectedResult)
            {
                var value1 = this.GetType().Assembly.GetEmbeddedResourceString(resourceName);
                Assert.Equal(expectedResult, value1?.Length > 0);
            }
            else
            {
                Assert.Throws<FileNotFoundException>(() => this.GetType().Assembly.GetEmbeddedResourceString(resourceName));
            }
        }

        [Fact]
        public void EmbeddedResourceToFileException1()
        {
            Assert.Throws<ArgumentNullException>(() => this.GetType().Assembly.EmbeddedResourceToFile(null, "blah.txt"));
            Assert.Throws<ArgumentNullException>(() => this.GetType().Assembly.EmbeddedResourceToFile(EmbeddedFile, null));
            Assert.Throws<ArgumentException>(() => this.GetType().Assembly.EmbeddedResourceToFile("", "blah.txt"));
            Assert.Throws<ArgumentException>(() => this.GetType().Assembly.EmbeddedResourceToFile(EmbeddedFile, ""));
        }
    }
}