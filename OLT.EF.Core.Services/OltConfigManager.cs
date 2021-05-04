using System;
using Microsoft.Extensions.Configuration;

namespace OLT.Core
{
    public class OltConfigManager : OltDisposable, IOltConfigManager
    {
        private readonly IConfiguration _configuration;

        public OltConfigManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public T GetValue<T>(string key, T defaultValue) where T : IConvertible
        {
            var value = _configuration.GetValue<T>(key);
            if (value == null)
            {
                return defaultValue;
            }
            return value;
        }

        public T GetValue<T>(string key) where T : IConvertible
        {
            return GetValue<T>(key, default(T));
        }

        public T GetSection<T>(string sectionName)
        {
            return _configuration.GetSection(sectionName).Get<T>();
        }
    }
}