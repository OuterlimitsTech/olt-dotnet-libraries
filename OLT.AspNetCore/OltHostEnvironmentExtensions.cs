using Microsoft.Extensions.Hosting;
using System;

namespace OLT.Core
{
    public static class OltHostEnvironmentExtensions
    {
        /// <summary>
        /// Checks if the current host environment name is <see cref="OltDefaults.OltEnvironments.Test"/>.
        /// </summary>
        /// <param name="hostEnvironment">An instance of <see cref="IHostEnvironment"/>.</param>
        /// <returns>True if the environment name is <see cref="OltDefaults.OltEnvironments.Test"/>, otherwise false.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool IsTest(this IHostEnvironment hostEnvironment)
        {
            if (hostEnvironment == null)
            {
                throw new ArgumentNullException(nameof(hostEnvironment));
            }
            return hostEnvironment.IsEnvironment(OltDefaults.OltEnvironments.Test);
        }
    }
}