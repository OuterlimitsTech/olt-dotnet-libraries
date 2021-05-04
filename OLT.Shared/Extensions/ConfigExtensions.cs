using System.Linq;

// ReSharper disable once CheckNamespace
namespace System.Collections.Specialized
{
    public static class ExtConfig
    {        

        public static T GetValue<T>(this  NameValueCollection nameValuePairs, string configKey, T defaultValue) where T : IConvertible
        {
            T retValue = default(T);

            if (nameValuePairs.AllKeys.Any(key => key == configKey))
            {
                var tmpValue = nameValuePairs[configKey];

                retValue = (T)Convert.ChangeType(tmpValue, typeof(T));
            }
            else
            {
                return defaultValue;
            }

            return retValue;
        }

    }

}

