

// ReSharper disable once CheckNamespace
namespace System.Xml
{
    public static class XmlExtensions
    {

        public static string EmptyProcessIfHasValue(this XmlNode self, Func<XmlNode, string> Process)
        {
            if (self != null)
                return Process(self);

            return string.Empty;
        }

        public static string NullProcessIfHasValue(this XmlNode self, Func<XmlNode, string> Process)
        {
            if (self != null)
                return Process(self);

            return null;
        }

        public static string DefaultProcessIfHasValue(this XmlNode self, string Default, Func<XmlNode, string> Process)
        {
            if (self != null)
                return Process(self);

            return Default;
        }
    }
}