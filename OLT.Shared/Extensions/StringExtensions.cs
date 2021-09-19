using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace System
{

    /// <summary>
    /// Extends <see cref="string"/>.
    /// </summary>
    public static partial class OltStringExtensions
    {



        #region [ Memory Stream ]


        public static System.IO.MemoryStream ToMemoryStream(this byte[] stream)
        {

            var memStream = new System.IO.MemoryStream();
            memStream.Write(stream, 0, stream.Length);
            return memStream;
        }

        public static bool ToFile(this System.IO.MemoryStream stream, string saveToFileName)
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


        public static bool ToFile(this byte[] stream, string saveToFileName)
        {

            if (System.IO.File.Exists(saveToFileName))
            {
                System.IO.File.Delete(saveToFileName);
            }

            var fileOutput = System.IO.File.Create(saveToFileName, stream.Length - 1);
            fileOutput.Write(stream, 0, stream.Length - 1);
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

        public static byte[] ToBytes(this string value)
        {
            var encoding = new System.Text.ASCIIEncoding();
            return encoding.GetBytes(value);
        }


        #endregion


    }
}
