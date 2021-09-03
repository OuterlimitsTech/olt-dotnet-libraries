////using System;
////using System.Data.Common;
////using Microsoft.Data.SqlClient;
////using Microsoft.EntityFrameworkCore;
////using Microsoft.EntityFrameworkCore.Migrations;
////using OLT.Core;

////namespace OLT.Libraries.UnitTest.Assets.Entity
////{
////    public class SqlDatabaseContextSqlGeneratorFactory : IDisposable
////    {
////        private DbConnection _connection;

////        private DbContextOptions<SqlDatabaseContext> CreateOptions()
////        {
////            return new DbContextOptionsBuilder<SqlDatabaseContext>().UseSqlServer(_connection).ReplaceService<IMigrationsSqlGenerator, OltMigrationsSqlAuditGenerator>().Options;
////        }

////        public SqlDatabaseContext CreateContext()
////        {
////            if (_connection == null)
////            {
////                _connection = new SqlConnection("bogus");
////                _connection.Open();

////                //var options = CreateOptions();
////                //using (var context = new SqlDatabaseContext(options))
////                //{
////                //    context.Database.EnsureCreated();
////                //}
////            }

////            return new SqlDatabaseContext(CreateOptions());
////        }

////        public void Dispose()
////        {
////            if (_connection != null)
////            {
////                _connection.Dispose();
////                _connection = null;
////            }
////        }
////    }
////}