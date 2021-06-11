// ReSharper disable once CheckNamespace
namespace System
{

    /// <summary>
    /// Extends <see cref="int"/>.
    /// </summary>
    public static class IntExtensions
    {
        public static string AddOrdinal(this int num)
        {
            if (num <= 0) return num.ToString();

            switch (num % 100)
            {
                case 11:
                case 12:
                case 13:
                    return num + "th";
            }

            switch (num % 10)
            {
                case 1:
                    return num + "st";
                case 2:
                    return num + "nd";
                case 3:
                    return num + "rd";
                default:
                    return num + "th";
            }
        }

        #region [ ToDollars ]

        /// <summary>
        /// Ensures Proper Rounding for Dollar Currency
        /// </summary>
        /// <param name="self">Extends <see cref="double"/>.</param>
        /// <returns>Returns <see cref="double"/> with proper rounded for dollars</returns>
        public static double ToDollars(this int self)
        {
            return self.ToString("N2").ToDouble(0);
        }

        /// <summary>
        /// Ensures Proper Rounding for Dollar Currency
        /// </summary>
        /// <param name="self">Extends <see cref="double"/>.</param>
        /// <returns>Returns <see cref="double"/> with proper rounded for dollars</returns>
        public static double? ToDollars(this int? self)
        {
            if (self.HasValue)
            {
                return self.GetValueOrDefault(0).ToString("N2").ToDouble();
            }
            return null;
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
            if (self.HasValue)
            {
                return self.GetValueOrDefault(0).ToDouble();
            }
            return null;
        }


        #endregion

    }

}
