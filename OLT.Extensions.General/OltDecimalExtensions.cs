

// ReSharper disable once CheckNamespace
namespace System
{

    /// <summary>
    /// Extends <see cref="decimal"/>.
    /// </summary>
    public static class OltDecimalExtensions
    {


        #region [ ToDollars ]

        /// <summary>
        /// Ensures Proper Rounding for Dollar Currency
        /// </summary>
        /// <param name="self">Extends <see cref="decimal"/>.</param>
        /// <returns>Returns <see cref="decimal"/> with proper rounded for dollars</returns>
        public static decimal ToDollars(this decimal self)
        {
            return self.ToString("N2").ToDecimal(0);
        }

        /// <summary>
        /// Ensures Proper Rounding for Dollar Currency
        /// </summary>
        /// <param name="self">Extends <see cref="decimal"/>.</param>
        /// <returns>Returns <see cref="decimal"/> with proper rounded for dollars</returns>
        public static decimal? ToDollars(this decimal? self)
        {
            return self?.ToString("N2").ToDecimal();
        }


        #endregion

        #region [ ToDouble ]

        /// <summary>
        /// Converts <see cref="decimal"/> to <see cref="double"/>
        /// </summary>
        /// <param name="self">Extends <see cref="decimal"/>.</param>
        /// <returns>Returns converted value to <see cref="double"/>, if cast fails, null <see cref="double"/></returns>
        public static double? ToDouble(this decimal self)
        {
            return Convert.ToDouble(self);
        }

        /// <summary>
        /// Converts <see cref="decimal"/> to <see cref="double"/>
        /// </summary>
        /// <param name="self">Extends <see cref="decimal"/>.</param>
        /// <returns>Returns converted value to <see cref="double"/>, if cast fails, null <see cref="double"/></returns>
        public static double? ToDouble(this decimal? self)
        {
            return self.HasValue ? Convert.ToDouble(self.Value) : new double?();
        }

        #endregion

    }
}