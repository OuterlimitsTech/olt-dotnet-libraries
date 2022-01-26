using System;
using System.Diagnostics.CodeAnalysis;

namespace OLT.Core
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class KeyValueAttribute : Attribute
    {
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public KeyValueAttribute(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }

        public string Key { get; private set; }
        public string Value { get; private set; }
    }
}