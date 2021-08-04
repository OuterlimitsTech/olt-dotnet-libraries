using OLT.Core;
using Serilog;
using Serilog.Events;
using Xunit.Abstractions;


namespace OLT.Libraries.UnitTest.Abstract
{
    // ReSharper disable once InconsistentNaming
    public class BaseTest : OltDisposable
    {
        //protected static bool SerilogSetup;
        protected ILogger Logger { get; }
        //protected readonly ILog Logger;

        public BaseTest(ITestOutputHelper output)
        {

            Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.TestOutput(output, LogEventLevel.Verbose)
                .CreateLogger()
                .ForContext<BaseTest>();

            ////if (SerilogSetup == false)
            ////{
            ////    ////Log.Logger = new LoggerConfiguration()
            ////    ////    .Enrich.FromLogContext()
            ////    ////    .MinimumLevel.Verbose()
            ////    ////    .WriteTo.Console()
            ////    ////    //.WriteTo.Console(new RenderedCompactJsonFormatter())
            ////    ////    //.WriteTo.Debug(outputTemplate: DateTime.Now.ToString())
            ////    ////    //.WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
            ////    ////    .CreateLogger();
            ////}

            //var logger = LogProvider.GetLogger(GetType().ToString());
        }


        

    }
}