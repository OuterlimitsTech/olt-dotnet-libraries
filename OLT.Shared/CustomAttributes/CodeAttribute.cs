//using System;
//using System.Linq;
//using System.Reflection;

//namespace OLT.Core
//{
//    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
//    public class CodeAttribute : Attribute
//    {
//        public string Code { get; private set; }
//        public short DefaultSort { get; set; } = 9999;

//        private CodeAttribute() { }
//        public CodeAttribute(string code)
//        {
//            this.Code = code;
//        }
//        public CodeAttribute(string code, short defaultSort)
//        {
//            this.Code = code;
//            this.DefaultSort = defaultSort;
//        }

//        public static string FromType(Type type)
//        {
//            if (type == null)
//            {
//                throw new ArgumentNullException(nameof(type));
//            }

//            var possibles = type.GetCustomAttributes<CodeAttribute>().ToList();
//            if (possibles == null || possibles.Count == 0) return null;
//            return possibles[0].Code;
//        }


//        public static string From<T>()
//        {
//            return FromType(typeof(T));
//        }
//    }
//}