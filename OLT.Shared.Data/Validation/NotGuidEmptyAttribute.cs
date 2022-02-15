using System;
using System.ComponentModel.DataAnnotations;

namespace OLT.Core
{
    /// <summary>
    ///  The not unique identifier empty attribute
    /// </summary>
    /// <seealso cref="System.ComponentModel.DataAnnotations.ValidationAttribute" />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    [Obsolete("Removed in 6.x. Use https://fluentvalidation.net/ instead")]
    public sealed class NotGuidEmptyAttribute : ValidationAttribute
    {
        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// <see cref="bool"/>
        /// </returns>
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            Guid typedValue;

            try
            {
                typedValue = (Guid)value;
            }
            catch (InvalidCastException)
            {
                return true;
            }

            if (typedValue == Guid.Empty)
            {
                return false;
            }

            return true;
        }
    }
}