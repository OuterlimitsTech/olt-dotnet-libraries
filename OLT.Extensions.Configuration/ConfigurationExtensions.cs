using System;
using Microsoft.Extensions.Configuration;

namespace OLT.Core
{

    /// <summary>
    /// Extends <see cref="IConfiguration"/>.
    /// </summary>
    public static class OltConfigurationExtensions
    {

        /// <summary>
        /// Looks for the connection string
        /// 1) connection-strings:{name}
        /// 2) GetEnvironmentVariable(name)
        /// 3) GetConnectionString(name)
        /// </summary>
        /// <param name="config"><see cref="IConfiguration"/></param>
        /// <param name="name">Connection String Key Name</param>
        /// <returns>Connection String or <see langword="null"/></returns>
        public static string GetOltConnectionString(this IConfiguration config, string name)
        {
            return config.GetValue<string>($"connection-strings:{name}") ??
                   Environment.GetEnvironmentVariable(name) ??
                   config.GetConnectionString(name);
        }
    }
}