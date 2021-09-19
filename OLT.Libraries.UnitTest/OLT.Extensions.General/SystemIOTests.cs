using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OLT.Libraries.UnitTest.OLT.Extensions.General
{
    public class SystemIOTests
    {

        private const string EmbeddedFile = "ImportTest.xlsx";

        [Fact]
        public void ToFile()
        {
            var dir = UnitTestHelper.BuildTempPath();
            var fileName = Path.Combine(dir, "ToBytes_Import.xlsx");


            var ms = this.GetType().Assembly
                .GetEmbeddedResourceStream(EmbeddedFile)
                .ToBytes()
                .ToMemoryStream();


            ms.ToFile(fileName);
            Assert.True(File.Exists(fileName));

            ms.ToFile(fileName); //Checking File Exists
            Assert.True(File.Exists(fileName));

            Directory.Delete(dir, true);
        }

        [Fact]
        public void FileToBytes()
        {
            var dir = UnitTestHelper.BuildTempPath();
            var fileName = Path.Combine(dir, "FileToBytes_Import.xlsx");

            var ms = this.GetType().Assembly
                .GetEmbeddedResourceStream(EmbeddedFile)
                .ToBytes()
                .ToMemoryStream();

            ms.ToFile(fileName);

            var bytes = File.ReadAllBytes(fileName);
            Assert.Equal(ms.Length, bytes.Length);
            Assert.True(File.Exists(fileName));

            ms.ToFile(fileName); //Checking File Exists
            Assert.True(File.Exists(fileName));

            Directory.Delete(dir, true);
        }

        [Fact]
        public void FileToMemoryStream()
        {
            var dir = UnitTestHelper.BuildTempPath();
            var fileName = Path.Combine(dir, "FileToMemoryStream_Import.xlsx");


            var ms = this.GetType().Assembly
                .GetEmbeddedResourceStream(EmbeddedFile)
                .ToBytes()
                .ToMemoryStream();

            ms.ToFile(fileName);
            var msCopy = File.ReadAllBytes(fileName).ToMemoryStream();
            
            Assert.Equal(ms.Length, msCopy.Length);
            Assert.True(File.Exists(fileName));
            Directory.Delete(dir, true);
        }

        [Fact]
        public void BytesToFile()
        {
            var dir = UnitTestHelper.BuildTempPath();
            var fileName = Path.Combine(dir, "BytesToFile_Import.xlsx");


            var bytes = this.GetType().Assembly
                .GetEmbeddedResourceStream(EmbeddedFile)
                .ToBytes();

            bytes.ToFile(fileName);
            var copyBytes = File.ReadAllBytes(fileName);

            Assert.Equal(bytes.Length, copyBytes.Length);
            Assert.True(File.Exists(fileName));
            Directory.Delete(dir, true);
        }

    }
}
