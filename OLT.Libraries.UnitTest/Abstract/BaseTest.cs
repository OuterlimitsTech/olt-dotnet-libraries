using OLT.Core;
using Serilog;
using Serilog.Events;
using Xunit.Abstractions;


namespace OLT.Libraries.UnitTest.Abstract
{
    // ReSharper disable once InconsistentNaming
    public class BaseTest : OltDisposable
    {
        protected ILogger Logger { get; }
        protected ITestOutputHelper TestOutputHelper { get; }

        public BaseTest(ITestOutputHelper output)
        {
            TestOutputHelper = output;

            Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.TestOutput(output, LogEventLevel.Verbose)
                .CreateLogger()
                .ForContext<BaseTest>();

        }


        

    }
}