using System.Collections.Generic;
using System.Diagnostics;
using OLT.Logging.Serilog.Enricher;
using Serilog;
using Serilog.Configuration;


namespace OLT.Logging.Serilog
{
    public static class OltSerilogExtensions
    {

        /// <summary>
        /// Enrich log unique identifier using <see cref="OltEventTypeEnricher"/>.
        /// </summary>
        /// <returns>Configuration object allowing method chaining.</returns>
        public static LoggerConfiguration WithOltEventType(this LoggerEnrichmentConfiguration loggerConfiguration)
        {
            return loggerConfiguration
                .With(new OltEventTypeEnricher());
        }


        /// <summary>
        /// Enrich log the Environment Name <see cref="OltDefaultsSerilog.Properties.Environment"/> and <see cref="OltDefaultsSerilog.Properties.DebuggerAttached"/>
        /// </summary>
        /// <returns>Configuration object allowing method chaining.</returns>
        public static LoggerConfiguration WithOltEnvironment(this LoggerEnrichmentConfiguration loggerConfiguration, string environmentName)
        {
            var config = loggerConfiguration.WithProperty(OltDefaultsSerilog.Properties.Environment, environmentName);

#if DEBUG
            // Used to filter out potentially bad data due debugging.
            // Very useful when doing Seq dashboards and want to remove logs under debugging session.
            config.Enrich.WithProperty(OltDefaultsSerilog.Properties.DebuggerAttached, Debugger.IsAttached);
#endif

            return config;
        }

    }
}
