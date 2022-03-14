////using System;
////using System.Linq;
////using Microsoft.EntityFrameworkCore;
////using Microsoft.EntityFrameworkCore.Migrations;
////using Microsoft.Extensions.Configuration;
////using OLT.Core;
////using OLT.Libraries.UnitTest.Abstract;
////using OLT.Libraries.UnitTest.Assets.Entity;
////using TestSupport.EfHelpers;
////using TestSupport.Helpers;
////using Xunit;
////using Xunit.Abstractions;

////namespace OLT.Libraries.UnitTest.OLT.EF.Core.SqlServer
////{
////    public class SqlExtensionHelpers : BaseTest
////    {

////        private readonly string _connectionString;
////        private readonly SqlDatabaseContext _context;

////        public SqlExtensionHelpers(
////            SqlDatabaseContext context,
////            IConfiguration configuration,
////            ITestOutputHelper output) : base(output)
////        {
////            _context = context;
////            _connectionString = configuration.GetConnectionString("UnitTestConnection");
////        }

////        [Theory]
////        [InlineData(OltConnectionStringTypes.AzureSql)]
////        public void IsProductionDb(OltConnectionStringTypes searchFor)
////        {
////            Assert.False(_context.IsProductionDb(searchFor));
////        }


////    }
////}