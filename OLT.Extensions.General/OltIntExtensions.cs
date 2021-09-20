
// ReSharper disable once CheckNamespace
namespace System
{
    /// <summary>
    /// Extends <see cref="int"/>.
    /// </summary>
    public static class OltIntExtensions
    {

        /// <summary>
        /// Adds Ordinal 1 = 1st, 2 = 2nd, 3 = 3rd
        /// </summary>
        /// <param name="self">Extends <see cref="int"/>.</param>
        /// <returns></returns>
        public static string AddOrdinal(this int self)
        {
            if (self <= 0) return self.ToString();

            switch (self % 100)
            {
                case 11:
                case 12:
                case 13:
                    return $"{self}th";
            }

            switch (self % 10)
            {
                case 1:
                    return $"{self}st";
                case 2:
                    return $"{self}nd";
                case 3:
                    return $"{self}rd";
                default:
                    return $"{self}th";
            }
        }


        #region [ ToDollars ]

        /// <summary>
        /// Ensures Proper Rounding for Dollar Currency
        /// </summary>
        /// <param name="self">Extends <see cref="int"/>.</param>
        /// <returns>Returns <see cref="double"/> with proper rounded for dollars</returns>
        public static double ToDollars(this int self)
        {
            return self.ToString("N2").ToDouble(0);
        }

        /// <summary>
        /// Ensures Proper Rounding for Dollar Currency
        /// </summary>
        /// <param name="self">Extends <see cref="int"/>.</param>
        /// <returns>Returns <see cref="double"/> with proper rounded for dollars</returns>
        public static double? ToDollars(this int? self)
        {
            return self?.ToString("N2").ToDouble();
        }

        #endregion

        #region [ ToDouble ]

        /// <summary>
        /// Ensures Proper Rounding for Dollar Currency
        /// </summary>
        /// <param name="self">Extends <see cref="double"/>.</param>
        /// <returns>Returns <see cref="double"/> with proper rounded for dollars</returns>
        public static double ToDouble(this int self)
        {
            return self.ToString().ToDouble(0);
        }

        /// <summary>
        /// Ensures Proper Rounding for Dollar Currency
        /// </summary>
        /// <param name="self">Extends <see cref="double"/>.</param>
        /// <returns>Returns <see cref="double"/> with proper rounded for dollars</returns>
        public static double? ToDouble(this int? self)
        {
            return self?.ToDouble();
        }


        #endregion
    }
}
