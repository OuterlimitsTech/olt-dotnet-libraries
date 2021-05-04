using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace OLT.Core
{
    public class OltKeyGenerator
    {

        internal static readonly char[] UpperCase = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        internal static readonly char[] LowerCase = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

        internal static readonly char[] Numerals = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        internal static readonly char[] Symbols = { '~', '`', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '{', '[', '}', ']', '-', '_', '=', '+', ':', ';', '|', '/', '?', ',', '<', '.', '>' };

        internal static readonly char[] SpecialCharacters = { '!', '@', '#', '$', '%', '&', '*', '+' };

        //internal static readonly char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

        public static string GeneratePassword(int length = 8, bool useNumbers = true, bool useLowerCaseLetters = true, bool useUpperCaseLetters = true, bool useSpecialCharacters = true)
        {

            var chars = (new char[0])
                .Concat(useUpperCaseLetters ? UpperCase : new char[0])
                .Concat(useLowerCaseLetters ? LowerCase : new char[0])
                .Concat(useNumbers ? Numerals : new char[0])
                .ToArray();

            var specialChars = SpecialCharacters.ToArray();
            int? specialCharIdx = null;
            if (useSpecialCharacters)
            {
                var rng = new Random(DateTimeOffset.Now.Millisecond);
                specialCharIdx = rng.Next(length);
            }

            byte[] data = new byte[4 * length];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {

                var rnd = BitConverter.ToUInt32(data, i * 4);
                var idx = rnd % chars.Length;

                if (specialCharIdx.HasValue && i == specialCharIdx)
                {
                    var rand = new Random(Environment.TickCount);
                    result.Append(specialChars[rand.Next(1, specialChars.Length)]);
                }
                else
                {
                    result.Append(chars[idx]);
                }

            }

            var password = result.ToString();


            return password;
        }

        public static string GetUniqueKey(int size)
        {
            var chars = (new char[0])
                .Concat(UpperCase)
                .Concat(LowerCase)
                .Concat(Numerals)
                .ToArray();


            byte[] data = new byte[4 * size];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(size);
            for (int i = 0; i < size; i++)
            {
                var rnd = BitConverter.ToUInt32(data, i * 4);
                var idx = rnd % chars.Length;

                result.Append(chars[idx]);
            }

            return result.ToString();
        }

        public static string GetUniqueKeyOriginal_BIASED(int size)
        {
            //char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            var chars = (new char[0])
                .Concat(UpperCase)
                .Concat(LowerCase)
                .Concat(Numerals)
                .ToArray();

            byte[] data = new byte[size];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
    }
}
