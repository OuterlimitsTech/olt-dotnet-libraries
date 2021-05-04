using System;
using System.Collections.Generic;
using System.Linq;

namespace OLT.Core
{
    public class OltGenericParameters : IOltGenericParameter
    {
        private readonly Dictionary<string, string> _values;

        public OltGenericParameters(Dictionary<string, string> values)
        {
            _values = values;
        }

        public T GetValue<T>(string key, T defaultValue) where T : IConvertible
        {
            var entry = _values.FirstOrDefault(p => p.Key.Equals(key, StringComparison.OrdinalIgnoreCase)).Value;
            if (entry != null)
            {
                return (T)Convert.ChangeType(entry, typeof(T));
            }
            return defaultValue;
        }
    }
}