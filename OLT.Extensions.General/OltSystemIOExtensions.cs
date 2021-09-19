

// ReSharper disable once CheckNamespace
namespace System.IO
{
    public static class OltSystemIOExtensions
    {
        /// <summary>
        /// Converts <see cref="long"/> to <see cref="int"/>
        /// </summary>
        /// <param name="self">Extends <see cref="long"/>.</param>
        /// <param name="defaultValue">value returned if cast fails.</param>
        /// <returns>Returns converted value to <see cref="int"/>. if cast fails, null int</returns>
        private static int ToInt(this long self, int defaultValue)
        {
            if (!int.TryParse(self.ToString(), out var value))
                return defaultValue;
            return value;
        }


        public static bool ToFile(this MemoryStream stream, string saveToFileName)
        {
            if (System.IO.File.Exists(saveToFileName))
            {
                System.IO.File.Delete(saveToFileName);
            }

            var fileOutput = System.IO.File.Create(saveToFileName, (stream.Length - 1).ToInt(0));
            stream.WriteTo(fileOutput);
            fileOutput.Close();
            return true;
        }


        public static System.IO.MemoryStream ToMemoryStream(this byte[] stream)
        {

            var memStream = new System.IO.MemoryStream();
            memStream.Write(stream, 0, stream.Length);
            return memStream;
        }


        public static System.IO.MemoryStream FileToMemoryStream(this string fileName)
        {
            if (!System.IO.File.Exists(fileName))
            {
                return null;
            }

            byte[] file = System.IO.File.ReadAllBytes(fileName);
            return ToMemoryStream(file);
        }

        public static byte[] ToBytes(this System.IO.Stream stream)
        {
            byte[] val = new byte[stream.Length];
            int idx = 0;
            stream.Read(val, idx, (int)stream.Length);
            stream.Close();
            return val;
        }

        public static bool ToFile(this byte[] stream, string saveToFileName)
        {

            if (System.IO.File.Exists(saveToFileName))
            {
                System.IO.File.Delete(saveToFileName);
            }

            var fileOutput = System.IO.File.Create(saveToFileName, stream.Length);
            fileOutput.Write(stream, 0, stream.Length);
            fileOutput.Close();
            return true;
        }


        public static byte[] FileToBytes(this string fileName)
        {

            if (!System.IO.File.Exists(fileName))
            {
                return Array.Empty<byte>();
            }

            var info = new System.IO.FileInfo(fileName);
            var fStream = info.OpenRead();

            var fileData = new byte[fStream.Length];

            fStream.Read(fileData, 0, (fStream.Length).ToInt(0));
            fStream.Close();

            return fileData;
        }





    }
}