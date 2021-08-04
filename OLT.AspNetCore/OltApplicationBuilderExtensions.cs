using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace OLT.Core
{
    public static class OltApplicationBuilderExtensions
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
                throw new ArgumentNullException(nameof(hostEnvironment));
            }

            return hostEnvironment.IsEnvironment(OltDefaults.OltEnvironments.Test);
        }


        /// <summary>
        /// Scaffolds Hosting Configuration <seealso cref="IOltAspNetHostingConfiguration"/>
        /// </summary>
        /// <typeparam name="TSettings"></typeparam>
        /// <param name="app"></param>
        /// <param name="settings"></param>
        /// <param name="action">Called to configure middleware like <code>app.UseSerilogRequestLogging()</code></param>
        /// <returns></returns>
        public static IApplicationBuilder UseOltDefaults<TSettings>(this IApplicationBuilder app, TSettings settings, Action action)
            where TSettings: OltAspNetAppSettings
        {

            var assembliesToScan = new List<Assembly>
            {
                Assembly.GetEntryAssembly(),
                Assembly.GetExecutingAssembly()
            };

            var hostingConfig =  assembliesToScan
                .GetAllReferencedAssemblies()
                .GetAllImplements<IOltAspNetHostingConfiguration>()
                .First(p => p.Name == settings.Hosting.ConfigurationName);
            if (hostingConfig == null)
            {
                throw new Exception($"Unable to locate hosting configuration {settings.Hosting.ConfigurationName}");
            }

            hostingConfig.Configure(app, settings, action);


            return app;
        }

        
    }
}