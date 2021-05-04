using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OLT.Core
{
    public static class ReflectionExtensions
    {
        public static List<Attribute> GetInheritedCustomAttributes(this MemberInfo member)
        {
            List<Attribute> result = new List<Attribute>();

            if (member == null) return result;

            var attrs = Attribute.GetCustomAttributes(member, true);
            if (attrs == null) return result;
            if (attrs.Length == 0) return result;

            return attrs.ToList();
        }

        public static List<T> GetInheritedCustomAttributes<T>(this MemberInfo member)
            where T : Attribute
        {
            List<T> result = new List<T>();

            if (member == null) return result;

            var attrs = Attribute.GetCustomAttributes(member, true);
            if (attrs == null) return result;
            if (attrs.Length == 0) return result;

            return attrs.Where(p => p.GetType() == typeof(T)).Cast<T>().ToList();
        }
    }

////public class CodeAttribute : Attribute
////{
////    [SuppressMessage("ReSharper", "InconsistentNaming")]
////    public CodeAttribute(string Code)
////    {
////        this.Code = Code;
////        this.DefaultSort = 9999;
////    }

////    [SuppressMessage("ReSharper", "InconsistentNaming")]
////    public CodeAttribute(string Code, short defaultSort)
////    {
////        this.Code = Code;
////        this.DefaultSort = defaultSort;
////    }

////    public string Code { get; private set; }
////    public short DefaultSort { get; set; }

////}
}