//using System;
//using System.ComponentModel;
//using System.Linq;
//using System.Reflection;

//namespace OLT.Core
//{

//    public static class OltCodeAttributeExtensions
//    {


//        /// <summary>
//        /// Returns Code value from <see cref="CodeAttribute"/> attribute
//        /// </summary>
//        /// <param name="value"></param>
//        /// <returns><see cref="string"/> Code from <see cref="CodeAttribute"/> or <see langword="null"/></returns>
//        public static string GetCodeEnum(this Enum value)
//        {
//            var attribute = value?.GetType()
//                .GetField(value.ToString())
//                .GetCustomAttributes(typeof(CodeAttribute), false)
//                .Cast<CodeAttribute>()
//                .SingleOrDefault();
//            return attribute?.Code;
//        }

//        /// <summary>
//        /// Returns DefaultSort value from <see cref="CodeAttribute"/> attribute
//        /// </summary>
//        /// <param name="value"></param>
//        /// <returns><see cref="short"/> DefaultSort from <see cref="CodeAttribute"/> or <see langword="null"/></returns>
//        public static short? GetCodeEnumSort(this Enum value)
//        {
//            var attribute = value?.GetType()
//                .GetField(value.ToString())
//                .GetCustomAttributes(typeof(CodeAttribute), false)
//                .Cast<CodeAttribute>()
//                .SingleOrDefault();
//            return attribute?.DefaultSort;
//        }

//        /// <summary>
//        /// Searches <typeparamref name="TEnum"/> for <see cref="CodeAttribute"/> or Name matching string
//        /// </summary>
//        /// <typeparam name="TEnum"></typeparam>
//        /// <param name="source"></param>
//        /// <returns>First instance of <typeparamref name="TEnum"/></returns>
//        /// <exception cref="ArgumentException"></exception>
//        /// <exception cref="ArgumentNullException"></exception>
//        public static TEnum FromCodeEnum<TEnum>(this string source) where TEnum : System.Enum, IConvertible
//        {
//            var type = typeof(TEnum);

//            foreach (var en in Enum.GetValues(type))
//            {
//                var memInfo = type.GetMember(en.ToString());

//                if (memInfo.Length > 0)
//                {
//                    var attrs = memInfo[0].GetCustomAttributes(typeof(CodeAttribute), false);

//                    if (attrs.Length > 0)
//                    {
//                        var code = ((CodeAttribute)attrs[0]).Code;
//                        if (string.Equals(source, code, StringComparison.OrdinalIgnoreCase))
//                        {
//                            return (TEnum)en;
//                        }
//                    }

//                }

//            }

//            return (TEnum)Enum.Parse(type, source, true);
//        }

//    }
//}
