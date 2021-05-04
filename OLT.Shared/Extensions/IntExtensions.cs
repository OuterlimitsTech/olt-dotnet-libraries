// ReSharper disable once CheckNamespace
namespace System
{

    /// <summary>
    /// Extends <see cref="int"/>.
    /// </summary>
    public static class IntExtensions
    {

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
