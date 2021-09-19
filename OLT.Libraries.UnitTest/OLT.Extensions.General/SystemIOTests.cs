using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OLT.Libraries.UnitTest.OLT.Extensions.General
{
    public class SystemIOTests
    {


        private string BuildTempPath()
        {
            var tempDir = Path.Combine(Path.GetTempPath(), $"OLT_UnitTest_{Guid.NewGuid()}");
            //var tempDir = Path.Combine(@"D:\FTP", "Created");
            if (!Directory.Exists(tempDir))
            {
                Directory.CreateDirectory(tempDir);
            }
            return tempDir;
        }


        [Fact]
        public void ToFile()
        {
            var fileStream = this.GetType().Assembly.GetEmbeddedResourceStream("ImportTest.xlsx");
            var ms = new MemoryStream();
            fileStream.CopyTo(ms);
            var dir = BuildTempPath();
            var fileName = Path.Combine(dir, "ToFile_Import.xlsx");
            Assert.True(ms.ToFile(fileName));
            Assert.True(File.Exists(fileName));
            Directory.Delete(dir, true);
        }


        [Fact]
        public void ToBytesAndToMemoryStream()
        {
            var ms = this.GetType().Assembly
                .GetEmbeddedResourceStream("ImportTest.xlsx")
                .ToBytes()
                .ToMemoryStream();

            var dir = BuildTempPath();
            var fileName = Path.Combine(dir, "ToBytesAndToMemoryStream_Import.xlsx");

            Assert.True(ms.ToFile(fileName));
            Assert.True(File.Exists(fileName));
            Directory.Delete(dir, true);
        }

        [Fact]
        public void FileToBytes()
        {
            var dir = BuildTempPath();
            var fileName = Path.Combine(dir, "FileToBytes_Import.xlsx");


            var ms = this.GetType().Assembly
                .GetEmbeddedResourceStream("ImportTest.xlsx")
                .ToBytes()
                .ToMemoryStream();

            ms.ToFile(fileName);

            var bytes = fileName.FileToBytes();
            Assert.Equal(ms.Length, bytes.Length);
            Assert.True(File.Exists(fileName));
            Directory.Delete(dir, true);
        }

        [Fact]
        public void FileToMemoryStream()
        {
            var dir = BuildTempPath();
            var fileName = Path.Combine(dir, "FileToMemoryStream_Import.xlsx");


            var ms = this.GetType().Assembly
                .GetEmbeddedResourceStream("ImportTest.xlsx")
                .ToBytes()
                .ToMemoryStream();

            ms.ToFile(fileName);
            var msCopy = fileName.FileToMemoryStream();
            
            Assert.Equal(ms.Length, msCopy.Length);
            Assert.True(File.Exists(fileName));
            Directory.Delete(dir, true);
        }

        [Fact]
        public void BytesToFile()
        {
            var dir = BuildTempPath();
            var fileName = Path.Combine(dir, "BytesToFile_Import.xlsx");


            var bytes = this.GetType().Assembly
                .GetEmbeddedResourceStream("ImportTest.xlsx")
                .ToBytes();

            bytes.ToFile(fileName);
            var copyBytes = fileName.FileToBytes();

            Assert.Equal(bytes.Length, copyBytes.Length);
            Assert.True(File.Exists(fileName));
            Directory.Delete(dir, true);
        }

    }
}
