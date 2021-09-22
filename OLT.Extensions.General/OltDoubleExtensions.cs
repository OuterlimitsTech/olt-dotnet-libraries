

// ReSharper disable once CheckNamespace
namespace System
{

    /// <summary>
    /// Extends <see cref="double"/>.
    /// </summary>
    public static class OltDoubleExtensions
    {
        #region [ ToDollars ]

        /// <summary>
        /// Ensures Proper Rounding for Dollar Currency
        /// </summary>
        /// <param name="self">Extends <see cref="double"/>.</param>
        /// <returns>Returns <see cref="double"/> with proper rounded for dollars</returns>
        public static double ToDollars(this double self)
        {
            return self.ToString("N2").ToDouble(0);
        }

        /// <summary>
        /// Ensures Proper Rounding for Dollar Currency
        /// </summary>
        /// <param name="self">Extends <see cref="double"/>.</param>
        /// <returns>Returns <see cref="double"/> with proper rounded for dollars</returns>
        public static double? ToDollars(this double? self)
        {
            return self?.ToString("N2").ToDouble();
        }


        #endregion
    }
}