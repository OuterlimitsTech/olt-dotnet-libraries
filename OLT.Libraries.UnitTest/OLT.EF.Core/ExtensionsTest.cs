using Microsoft.Extensions.Configuration;
using OLT.Core;
using OLT.Libraries.UnitTest.Abstract;
using Xunit;
using Xunit.Abstractions;

namespace OLT.Libraries.UnitTest.OLT.EF.Core
{
    public class ExtensionsTest : BaseTest
    {
        private readonly IConfiguration _configuration;

        public ExtensionsTest(
            IConfiguration configuration,
            ITestOutputHelper output) : base(output)
        {
            _configuration = configuration;
        }



        [Fact]
        public void Get()
        {
            var val = _configuration.GetOltConnectionString("DbConnection");
            Assert.True(val?.Equals("TestConnectionString"));
        }
    }
}