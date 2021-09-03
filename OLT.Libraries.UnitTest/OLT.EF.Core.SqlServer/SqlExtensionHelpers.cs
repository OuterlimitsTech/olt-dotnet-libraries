using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using OLT.Core;
using OLT.Libraries.UnitTest.Abstract;
using OLT.Libraries.UnitTest.Assets.Entity;
using TestSupport.EfHelpers;
using TestSupport.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace OLT.Libraries.UnitTest.OLT.EF.Core.SqlServer
{
    public class SqlExtensionHelpers : BaseTest
    {

        private readonly string _connectionString;

        public SqlExtensionHelpers(
            IConfiguration configuration,
            ITestOutputHelper output) : base(output)
        {
            _connectionString = configuration.GetConnectionString("UnitTestConnection");
        }

        [Theory]
        [InlineData(OltConnectionStringTypes.AzureSql)]
        public void IsProductionDb(OltConnectionStringTypes searchFor)
        {
            using var factory = new SqlLiteDatabaseContextFactory();
            // Get a context
            using var context = factory.CreateContext();
            Assert.False(context.IsProductionDb(searchFor));
        }


    }
}