// ReSharper disable once CheckNamespace
namespace System
{

    /// <summary>
    /// Extends <see cref="decimal"/>.
    /// </summary>
    public static class DecimalExtensions
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
            if (self.HasValue)
            {
                return self.GetValueOrDefault(0).ToString("N2").ToDecimal();
            }
            return null;
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
            double value;
            if (!double.TryParse(self.ToString(), out value))
                return null;
            return value;
        }

        /// <summary>
        /// Converts <see cref="decimal"/> to <see cref="double"/>
        /// </summary>
        /// <param name="self">Extends <see cref="decimal"/>.</param>
        /// <returns>Returns converted value to <see cref="double"/>, if cast fails, null <see cref="double"/></returns>
        public static double? ToDouble(this decimal? self)
        {
            if (!self.HasValue)
            {
                return null;
            }

            double value;
            if (!double.TryParse(self.ToString(), out value))
                return null;
            return value;
        }

        /// <summary>
        /// Converts <see cref="decimal"/> to <see cref="int"/>
        /// </summary>
        /// <param name="self">Extends <see cref="decimal"/>.</param>
        /// <param name="defaultValue">value returned if cast fails.</param>
        /// <returns>Returns converted value to <see cref="double"/>. if cast fails, return defaultValue</returns>
        public static double ToDouble(this decimal self, double defaultValue)
        {
            double value;
            if (!double.TryParse(self.ToString(), out value))
                return defaultValue;
            return value;
        }

        /// <summary>
        /// Converts <see cref="decimal"/> to <see cref="int"/>
        /// </summary>
        /// <param name="self">Extends <see cref="decimal"/>.</param>
        /// <param name="defaultValue">value returned if cast fails.</param>
        /// <returns>Returns converted value to <see cref="double"/>. if cast fails, return defaultValue</returns>
        public static double ToDouble(this decimal? self, double defaultValue)
        {
            if (!self.HasValue)
            {
                return defaultValue;
            }

            double value;
            if (!double.TryParse(self.ToString(), out value))
                return defaultValue;
            return value;
        }


        #endregion

    }
}
