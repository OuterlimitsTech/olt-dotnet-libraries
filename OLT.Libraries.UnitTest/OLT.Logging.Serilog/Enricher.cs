using System.Linq;
using FluentAssertions;
using OLT.Libraries.UnitTest.Assets.Rules;
using OLT.Logging.Serilog;
using OLT.Logging.Serilog.Enricher;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.TestCorrelator;
using Xunit;
using Xunit.Abstractions;

namespace OLT.Libraries.UnitTest.OLT.Logging.Serilog
{
    public class OltEnricherTests
    {
        private readonly ITestOutputHelper _output;

        public OltEnricherTests(ITestOutputHelper output)
        {
            _output = output;
            Log.Logger = new LoggerConfiguration()
                .WriteTo.TestCorrelator()
                .CreateLogger();
        }


        [Fact]
        public void WithOltEventType()
        {
            var val = 1234;

            using (TestCorrelator.CreateContext())
            using (var logger = new LoggerConfiguration()
                .Enrich.WithOltEventType()
                .WriteTo.TestOutput(_output, outputTemplate: OltDefaultsSerilog.Templates.DefaultOutput)
                .WriteTo.Sink(new TestCorrelatorSink())
                .CreateLogger())
            {
                logger.Information("Test Log Value: {value1}", val);

                TestCorrelator.GetLogEventsFromCurrentContext()
                    .SelectMany(s => s.Properties.Select(t => t.Key))
                    .Where(key => key == OltDefaultsSerilog.Properties.EventType)
                    .Should()
                    .ContainSingle();

                //TestCorrelator.GetLogEventsFromCurrentContext().Should().ContainSingle().Which.MessageTemplate.Text.Should().Be("Test Log Value: {value1}");
            }
        }


        [Fact]
        public void WithOltEnvironment()
        {
            var val = 1234;

            using (TestCorrelator.CreateContext())
            using (var logger = new LoggerConfiguration()
                .Enrich.WithOltEnvironment("foobar")
                .WriteTo.TestOutput(_output, outputTemplate: OltDefaultsSerilog.Templates.DefaultOutput)
                .WriteTo.Sink(new TestCorrelatorSink())
                .CreateLogger())
            {
                logger.Information("Test Log Value: {value1}", val);

                TestCorrelator.GetLogEventsFromCurrentContext()
                    .SelectMany(s => s.Properties.Select(t => t.Key))
                    .Where(key => key == OltDefaultsSerilog.Properties.Environment)
                    .Should()
                    .ContainSingle();

            }
        }
    }
}