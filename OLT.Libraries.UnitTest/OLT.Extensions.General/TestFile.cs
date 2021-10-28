using System;
using System.IO;
using System.Reflection;

namespace OLT.Libraries.UnitTest.OLT.Extensions.General
{
    public class TestFile
    {
        private string _masterPath;
        private const string EmbeddedFile = "ImportTest.xlsx";

        public TestFile()
        {
            
        }

        public string MasterPath
        {
            get
            {
                if (_masterPath.IsEmpty())
                {
                    _masterPath = UnitTestHelper.BuildTempPath();
                }

                return _masterPath;
            }
        }

        public void CreateFiles(int number)
        {
            for (int i = 0; i < number; i++)
            {
                this.GetType().Assembly
                    .GetEmbeddedResourceStream(EmbeddedFile)
                    .ToBytes()
                    .ToFile(Path.Combine(MasterPath, $"Import_{Guid.NewGuid()}.xlsx"));

            }
        }

        public string CreateFiles(string subDir, int number)
        {
            
            var dir = subDir.StartsWith(MasterPath) ? subDir : Path.Combine(MasterPath, subDir);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            for (int i = 0; i < number; i++)
            {
                this.GetType().Assembly
                    .GetEmbeddedResourceStream(EmbeddedFile)
                    .ToBytes()
                    .ToFile(Path.Combine(dir, $"Import_{Guid.NewGuid()}.xlsx"));

            }

            return dir;
        }

        public void Dispose()
        {
            Directory.Delete(MasterPath, true);
        }
    }
}