using Microsoft.Extensions.Hosting;

namespace OLT.Core
{
    public static partial class OltHostEnvironmentExtensions
    {

        /// <summary>
        /// Checks if debugger is currently attached
        /// </summary>
        /// <param name="hostEnvironment">An instance of <see cref="IHostEnvironment"/>.</param>
        /// <returns>Returns true if debugger attached</returns>
        public static bool InDebug(this IHostEnvironment hostEnvironment)
        {
            var inDebug = false;
#if DEBUG
            inDebug = true;
#endif
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            return inDebug;
        }

        /// <summary>
        /// Checks if the current host environment name is <see cref="OltDefaults.OltEnvironments.Test"/>.
        /// </summary>
        /// <param name="hostEnvironment">An instance of <see cref="IHostEnvironment"/>.</param>
        /// <returns>True if the environment name is <see cref="OltDefaults.OltEnvironments.Test"/>, otherwise false.</returns>
        public static bool IsTest(this IHostEnvironment hostEnvironment)
        {
            if (hostEnvironment == null)
            {
                throw new OltException(nameof(hostEnvironment));
            }

            return hostEnvironment.IsEnvironment(OltDefaults.OltEnvironments.Test);
        }
    }
}