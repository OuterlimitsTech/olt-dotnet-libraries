using System;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;

namespace OLT.Core
{
    public static class OltAzureAppConfigurationOptionsExtensions
    {
        /// <summary>
        /// Configures Connection using a connection or a URL.  For URL, the ManagedIdentityCredential() is used
        /// </summary>
        /// <param name="options"><paramref name="options"/></param>
        /// <param name="config"><paramref name="config"/></param>
        /// <param name="connectionStringKey">Azure Config Key for connection string if applicable</param>
        /// <param name="appSettingEndpoint">App Setting for the endpoint url Default: https://outerlimitstech.azconfig.io</param>
        /// <returns></returns>
        public static AzureAppConfigurationOptions OltConnect(this AzureAppConfigurationOptions options, IConfiguration config, string connectionStringKey = "AzureConfig", string appSettingEndpoint = "AppSettings:AzureConfigEndpoint")
        {
            var configConnString = config.GetConnectionString(connectionStringKey);
            var endpoint = config[appSettingEndpoint] ?? "https://outerlimitstech.azconfig.io";

            return string.IsNullOrEmpty(configConnString)
                ? options.Connect(new Uri(endpoint), new ManagedIdentityCredential())
                : options.Connect(configConnString);
        }


        /// <summary>
        /// Configures the Default Azure Configuration
        /// </summary>
        /// <param name="options"><paramref name="options"/></param>
        /// <param name="keyPrefix">config prefix (i.e., BGCS:)</param>
        /// <param name="environmentName"></param>
        /// <param name="refreshKey">Config Key that will cause an auto refresh</param>
        /// <returns></returns>
        public static AzureAppConfigurationOptions OltAzureConfigDefault(this AzureAppConfigurationOptions options, string keyPrefix, string environmentName, string refreshKey)
        {
            var keyFilter = $"{keyPrefix}*";

            options.TrimKeyPrefix(keyPrefix)
                .Select(keyFilter, LabelFilter.Null)
                .Select(keyFilter, environmentName);


            if (!string.IsNullOrEmpty(refreshKey))
            {
                options.ConfigureRefresh(refreshOptions =>
                    refreshOptions
                        .Register(refreshKey, LabelFilter.Null, true)
                        .Register(refreshKey, environmentName, true));
            }

            return options;
        }
    }
}
