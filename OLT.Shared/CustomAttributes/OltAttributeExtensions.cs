using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace OLT.Core
{
    public static class OltAttributeExtensions
    {

        public static T GetAttributeInstance<T, TE>(TE item)
            where T : Attribute
            where TE : struct
        {
            var type = item.GetType();
            var field = item.ToString();
            var attribute = type.GetField(field).GetCustomAttributes(typeof(T), false).Cast<T>().FirstOrDefault();
            return attribute;
        }

        public static T GetAttributeInstance<T>(Enum item)
            where T : Attribute
        {
            var type = item.GetType();
            var attribute = type.GetField(item.ToString()).GetCustomAttributes(typeof(T), false).Cast<T>().FirstOrDefault();
            return attribute;
        }


        public static T GetAttributeInstance<T>(object item)
            where T : Attribute
        {
            var type = item.GetType();
            var field = item.ToString();
            var attribute = type.GetField(field).GetCustomAttributes(typeof(T), false).Cast<T>().FirstOrDefault();
            return attribute;
        }

        /// <summary>
        /// Returns first instance of <typeparamref name="T"/> attribute on a class property
        /// </summary>
        /// <typeparam name="T">Type of <see cref="Attribute"/> to search for</typeparam>
        /// <param name="property"><see cref="PropertyInfo"/></param>
        /// <param name="inherit">include inherited attributes</param>
        /// <returns>First instance of <see cref="Attribute"/> to search for or Null</returns>
        public static T GetAttributeInstance<T>(this PropertyInfo property, bool inherit = false) where T : Attribute
        {
            return property.GetAttributeInstances<T>().FirstOrDefault();
        }

        /// <summary>
        /// Returns all instances of <typeparamref name="T"/> attribute on a class property
        /// </summary>
        /// <typeparam name="T">Type of <see cref="Attribute"/> to search for</typeparam>
        /// <param name="property"><see cref="PropertyInfo"/></param>
        /// <param name="inherit">include inherited attributes</param>
        /// <returns>Returns all instances of <see cref="Attribute"/> to search for or Null</returns>
        public static List<T> GetAttributeInstances<T>(this PropertyInfo property, bool inherit = false) where T : Attribute
        {
            return property.GetCustomAttributes(typeof(T), inherit).Cast<T>().ToList();
        }

        public static string GetDescription(this Enum value)
        {
            var attribute = GetAttributeInstance<DescriptionAttribute>(value);
            return attribute?.Description ?? value.ToString();
        }

        public static T FromDescription<T>(this string source) where T : struct, IConvertible
        {

            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            var type = typeof(T);

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
                            return (T)en;
                        }
                    }

                }

            }

            return (T)Enum.Parse(type, source, true);



        }

        public static List<KeyValueAttribute> GetKeyValueAttributes(this Enum value)
        {
            return value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(KeyValueAttribute), false)
                .ToList()
                .Cast<KeyValueAttribute>()
                .ToList();
        }

        public static string GetCodeEnum(this Enum value)
        {
            var attribute = value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(CodeAttribute), false)
                .Cast<CodeAttribute>()
                .SingleOrDefault();
            return attribute?.Code;
        }

        public static string GetEnumCode<TEnum>(TEnum item)
        {
            var type = item.GetType();
            var attribute = type.GetField(item.ToString()).GetCustomAttributes(typeof(CodeAttribute), false).Cast<CodeAttribute>().SingleOrDefault();
            return attribute?.Code;
        }

        public static short? GetCodeEnumSort(this Enum value)
        {
            var attribute = value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(CodeAttribute), false)
                .Cast<CodeAttribute>()
                .SingleOrDefault();
            return attribute?.DefaultSort;
        }

        public static T FromCodeEnum<T>(this string source) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            var type = typeof(T);

            foreach (var en in Enum.GetValues(type))
            {
                var memInfo = type.GetMember(en.ToString());

                if (memInfo.Length > 0)
                {
                    var attrs = memInfo[0].GetCustomAttributes(typeof(CodeAttribute), false);

                    if (attrs.Length > 0)
                    {
                        var code = ((CodeAttribute)attrs[0]).Code;
                        if (string.Equals(source, code, StringComparison.OrdinalIgnoreCase))
                        {
                            return (T)en;
                        }
                    }

                }

            }

            return (T)Enum.Parse(type, source, true);
        }

    }
}
