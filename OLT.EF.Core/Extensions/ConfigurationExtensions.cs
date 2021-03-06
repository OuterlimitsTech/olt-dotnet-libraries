using System;
using Microsoft.Extensions.Configuration;

namespace OLT.Core
{
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Looks for the connection string
        /// 1) connection-strings:{name}
        /// 2) GetEnvironmentVariable(name)
        /// 3) GetConnectionString(name)
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetOltConnectionString(this IConfiguration settings, string name)
        {
            return settings.GetValue<string>($"connection-strings:{name}") ??
                   Environment.GetEnvironmentVariable(name) ??
                   settings.GetConnectionString(name);
        }
    }
}