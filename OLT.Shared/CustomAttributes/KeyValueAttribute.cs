using System;

namespace OLT.Core
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class KeyValueAttribute : Attribute
    {
        public KeyValueAttribute(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }

        public string Key { get; private set; }
        public string Value { get; private set; }
    }
}