using System;
using System.ComponentModel.DataAnnotations;

namespace OLT.Core
{
    /// <summary>
    /// Defines the NotNullAttribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    [Obsolete("Removed in 6.x. Use https://fluentvalidation.net/ instead")]
    public sealed class NotNullAttribute : ValidationAttribute
    {
        /// <summary>
        /// Determines whether the specified value of the object is valid.
        /// </summary>
        /// <param name="value">The value of the object to validate.</param>
        /// <returns>
        /// true if the specified value is valid; otherwise, false.
        /// </returns>
        public override bool IsValid(object value)
        {
            return value != null;
        }
    }
}