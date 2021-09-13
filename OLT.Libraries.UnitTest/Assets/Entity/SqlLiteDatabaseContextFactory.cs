////using System;
////using System.Data.Common;
////using Microsoft.Data.Sqlite;
////using Microsoft.EntityFrameworkCore;
////using OLT.Libraries.UnitTest.Assets.LocalServices;

////namespace OLT.Libraries.UnitTest.Assets.Entity
////{
////    public class SqlLiteDatabaseContextFactory : IDisposable
////    {
////        private DbConnection _connection;

////        private DbContextOptions<SqlLiteDatabaseContext> CreateOptions()
////        {
////            return new DbContextOptionsBuilder<SqlLiteDatabaseContext>()
////                .UseSqlite(_connection).Options;
////        }

////        public SqlLiteDatabaseContext CreateContext()
////        {
////            if (_connection == null)
////            {
////                _connection = new SqliteConnection("DataSource=:memory:");
////                _connection.Open();

////                var options = CreateOptions();
////                using (var context = new SqlLiteDatabaseContext(options))
////                {
////                    context.Database.EnsureCreated();
////                }
////            }

////            return new SqlLiteDatabaseContext(CreateOptions());
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