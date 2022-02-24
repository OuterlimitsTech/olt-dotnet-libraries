using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace OLT.Core
{    
    public static class OltKeyGenerator
    {

        /// <summary>
        /// Generates a random password
        /// </summary>
        /// <param name="length">The length.</param>
        /// <param name="useNumbers">The use numbers.</param>
        /// <param name="useLowerCaseLetters">The use lower case letters.</param>
        /// <param name="useUpperCaseLetters">The use upper case letters.</param>
        /// <param name="useSymbols">The use symbols.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string GeneratePassword(int length = 8, bool useNumbers = true, bool useLowerCaseLetters = true, bool useUpperCaseLetters = true, bool useSymbols = true)
        {
            var total = (new char[0])
                            .Concat(useUpperCaseLetters ? OltDefaults.Characters.UpperCase : new char[0])
                            .Concat(useLowerCaseLetters ? OltDefaults.Characters.LowerCase : new char[0])
                            .Concat(useNumbers ? OltDefaults.Characters.Numerals : new char[0])
                            .Concat(useSymbols ? OltDefaults.Characters.Symbols : new char[0])
                            .ToArray();

            var random = new Random(GetCryptographicRandomNumber());

            var chars = Enumerable
                .Repeat(0, length)
                .Select(i => total[random.Next(1, total.Length)])
                .ToArray();

            return new string(chars);
        }

        /// <summary>
        /// The get cryptographic random number.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        private static int GetCryptographicRandomNumber()
        {
            var lowerBound = 1;
            var upperBound = 16;
            uint uintRandonNumber;
            var randomNumber = new Byte[4];
            var randomNumberGenerator = RandomNumberGenerator.Create();

            var xcludeRndBase = (uint.MaxValue - (uint.MaxValue % (uint)(upperBound - lowerBound)));

            do
            {
                randomNumberGenerator.GetBytes(randomNumber);
                uintRandonNumber = System.BitConverter.ToUInt32(randomNumber, 0);
            } while (uintRandonNumber >= xcludeRndBase);

            return (int)(uintRandonNumber % (upperBound - lowerBound)) + lowerBound;
        }

        public static string GetUniqueKey(int size)
        {
            var chars = (new char[0])
                .Concat(OltDefaults.Characters.UpperCase)
                .Concat(OltDefaults.Characters.LowerCase)
                .Concat(OltDefaults.Characters.Numerals)
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
            var chars = (new char[0])
                .Concat(OltDefaults.Characters.UpperCase)
                .Concat(OltDefaults.Characters.LowerCase)
                .Concat(OltDefaults.Characters.Numerals)
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
