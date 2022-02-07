using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OLT.Core
{
    public static class OltAttributeExtensions
    {
        /// <summary>
        /// Returns first instance of <typeparamref name="T"/> attribute on <typeparamref name="TEnum"/>
        /// </summary>
        /// <typeparam name="T">Type of <see cref="Attribute"/> to search for</typeparam>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="item"></param>
        /// <returns>First instance of <see cref="Attribute"/></returns>
        /// <exception cref="InvalidOperationException">Sequence contains more than one element</exception>
        public static T GetAttributeInstance<T, TEnum>(TEnum item)
            where T : Attribute
            where TEnum : System.Enum
        {            
            var type = item.GetType();
            var field = item.ToString();
            var attribute = type.GetField(field).GetCustomAttributes(typeof(T), false).Cast<T>().SingleOrDefault();
            return attribute;
        }

        /// <summary>
        /// Returns first instance of <typeparamref name="T"/> attribute on <typeparamref name="TEnum"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns>First instance of <see cref="Attribute"/> to search for or <see langword="null"/></returns>
        /// <exception cref="InvalidOperationException">Sequence contains more than one element</exception>
        public static T GetAttributeInstance<T>(Enum item)
            where T : Attribute
        {
            if (item == null) return null;
            var type = item.GetType();
            var attribute = type.GetField(item.ToString()).GetCustomAttributes(typeof(T), false).Cast<T>().SingleOrDefault();
            return attribute;
        }

        /// <summary>
        /// Returns first instance of <typeparamref name="T"/> attribute on a class property
        /// </summary>
        /// <typeparam name="T">Type of <see cref="Attribute"/> to search for</typeparam>
        /// <param name="property"><see cref="PropertyInfo"/></param>
        /// <param name="inherit">include inherited attributes</param>
        /// <returns>First instance of <see cref="Attribute"/> to search for or <see langword="null"/></returns>
        /// <exception cref="InvalidOperationException">Sequence contains more than one element</exception>
        public static T GetAttributeInstance<T>(this PropertyInfo property, bool inherit = false) where T : Attribute
        {
            return property?.GetAttributeInstances<T>(inherit).SingleOrDefault();
        }

        /// <summary>
        /// Returns all instances of <typeparamref name="T"/> attribute on a class property
        /// </summary>
        /// <typeparam name="T">Type of <see cref="Attribute"/> to search for</typeparam>
        /// <param name="property"><see cref="PropertyInfo"/></param>
        /// <param name="inherit">include inherited attributes</param>
        /// <returns>Returns all instances of <see cref="Attribute"/></returns>
        public static List<T> GetAttributeInstances<T>(this PropertyInfo property, bool inherit = false) where T : Attribute
        {
            return property?.GetCustomAttributes(typeof(T), inherit).Cast<T>().ToList();
        }

        /// <summary>
        /// Gets <see cref="DescriptionAttribute"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns><see cref="DescriptionAttribute.Description"/> or <typeparamref name="TEnum"/> ToString() or <see langword="null"/></returns>
        public static string GetDescription(this Enum value)
        {
            var attribute = GetAttributeInstance<DescriptionAttribute>(value);
            return attribute?.Description ?? value?.ToString();
        }


        /// <summary>
        /// Searches <typeparamref name="TEnum"/> for <see cref="DescriptionAttribute"/> or Name matching string
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="source"></param>
        /// <returns>First instance of <typeparamref name="TEnum"/></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static TEnum FromDescription<TEnum>(this string source) where TEnum : System.Enum, IConvertible
        {
            var type = typeof(TEnum);

            foreach (var en in Enum.GetValues(type))
            {
                var memInfo = type.GetMember(en.ToString());

                if (memInfo != null && memInfo.Length > 0)
                {

                    var attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                    if (attrs != null && attrs.Length > 0)
                    {
                        var description = ((DescriptionAttribute)attrs[0]).Description;
                        if (string.Equals(source, description, StringComparison.OrdinalIgnoreCase))
                        {
                            return (TEnum)en;
                        }
                    }
                }
            }            

            return (TEnum)Enum.Parse(type, source, true);
        }
    }
}
