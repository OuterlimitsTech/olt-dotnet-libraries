using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace OLT.Libraries.UnitTest.OLT.Extensions.General
{
    public class SystemIODeleteFilesTests
    {
       

        public static IEnumerable<object[]> DeleteFilesOlderThanData =>
            new List<object[]>
            {
                new object[] { new TestFile(), 5, 5, DateTime.Now.AddMinutes(5), true, 0 },
                new object[] { new TestFile(), 5, 5, DateTime.Now.AddMinutes(-5), true, 10 },
                new object[] { new TestFile(), 5, 5, DateTime.Now.AddMinutes(1), false, 5 },
                new object[] { new TestFile(), 5, 5, DateTime.Now.AddMinutes(-5), false, 10 },
            };

        [Theory]
        [MemberData(nameof(DeleteFilesOlderThanData))]
        public void DeleteFilesOlderThan(TestFile testFile, int createFiles, int createDirectories, DateTime olderThan, bool recursive, int expectedFiles)
        {
            var files = createFiles;

            testFile.CreateFiles(createFiles);

            for (int i = 0; i < createDirectories; i++)
            {
                files++;
                testFile.CreateFiles($"Sub_{Guid.NewGuid()}", 1);
            }

            var dirInfo = new DirectoryInfo(testFile.MasterPath);
            Assert.Equal(files, dirInfo.GetFiles("*.*", SearchOption.AllDirectories).Length);
            Assert.Equal(createDirectories, dirInfo.GetDirectories("*.*", SearchOption.AllDirectories).Length);
            dirInfo.DeleteFiles("*.*", olderThan, recursive);
            Assert.Equal(expectedFiles, dirInfo.GetFiles("*.*", SearchOption.AllDirectories).Length);
            testFile.Dispose();
        }


        public static IEnumerable<object[]> DeleteFileData =>
            new List<object[]>
            {
                new object[] { new TestFile(), 5, 5, true, 0 },
                new object[] { new TestFile(), 5, 7, false, 7 },
            };

        [Theory]
        [MemberData(nameof(DeleteFileData))]
        public void DeleteFiles(TestFile testFile, int createFiles, int createDirectories, bool recursive, int expectedFiles)
        {
            var files = createFiles;

            testFile.CreateFiles(createFiles);

            for (int i = 0; i < createDirectories; i++)
            {
                files++;
                testFile.CreateFiles($"Sub_{Guid.NewGuid()}", 1);
            }

            var dirInfo = new DirectoryInfo(testFile.MasterPath);
            Assert.Equal(files, dirInfo.GetFiles("*.*", SearchOption.AllDirectories).Length);
            Assert.Equal(createDirectories, dirInfo.GetDirectories("*.*", SearchOption.AllDirectories).Length);

            if (recursive)
            {
                dirInfo.DeleteFiles("*.*", true);
            }
            else
            {
                dirInfo.DeleteFiles("*.*");
            }
            
            Assert.Equal(expectedFiles, dirInfo.GetFiles("*.*", SearchOption.AllDirectories).Length);
            testFile.Dispose();
        }
    }
}