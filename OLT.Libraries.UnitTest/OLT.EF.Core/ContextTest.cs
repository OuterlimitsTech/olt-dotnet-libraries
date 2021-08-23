﻿using System.Linq;
using Microsoft.EntityFrameworkCore;
using OLT.Core;
using OLT.Libraries.UnitTest.Abstract;
using OLT.Libraries.UnitTest.Assets.Entity;
using OLT.Libraries.UnitTest.Assets.Entity.Models;
using Xunit;
using Xunit.Abstractions;

namespace OLT.Libraries.UnitTest.OLT.EF.Core
{
    public class ContextTest : BaseTest
    {
        private readonly IOltIdentity _identity;
        private readonly SqlDatabaseContext _context;

        public ContextTest(
            IOltIdentity identity,
            SqlDatabaseContext context,
            ITestOutputHelper output) : base(output)
        {
            _identity = identity;
            _context = context;
        }



        [Fact]
        public void Get()
        {
            var auditUser = _context.AuditUser;
            Assert.True(_identity.GetDbUsername().Equals(auditUser));
        }


        [Fact]
        public void GetTableName()
        {
            var tableName = _context.GetTableName<PersonEntity>();
            var compareTo = nameof(PersonEntity);
            Assert.Equal(tableName, compareTo);
        }

        [Fact]
        public void GetColumns()
        {
            var columns = _context.GetColumns<PersonEntity>();
            Assert.Collection(columns,
                item => Assert.Equal(item.Name, $"{nameof(PersonEntity)}{nameof(PersonEntity.Id)}"),
                item => Assert.Equal(item.Name, $"{nameof(PersonEntity.CreateDate)}"),
                item => Assert.Equal(item.Name, $"{nameof(PersonEntity.CreateUser)}"),
                item => Assert.Equal(item.Name, $"{nameof(PersonEntity.DeletedBy)}"),
                item => Assert.Equal(item.Name, $"{nameof(PersonEntity.DeletedOn)}"),
                item => Assert.Equal(item.Name, $"{nameof(PersonEntity.ModifyDate)}"),
                item => Assert.Equal(item.Name, $"{nameof(PersonEntity.ModifyUser)}"),
                item => Assert.Equal(item.Name, $"{nameof(PersonEntity.NameFirst)}"),
                item => Assert.Equal(item.Name, $"{nameof(PersonEntity.NameLast)}"),
                item => Assert.Equal(item.Name, $"{nameof(PersonEntity.NameMiddle)}")
            );
        }


        // https://www.meziantou.net/testing-ef-core-in-memory-using-sqlite.htm
        [Fact]
        public void SqlLite()
        {
            using (var factory = new SqlLiteDatabaseContextFactory())
            {
                // Get a context
                using (var context = factory.CreateContext())
                {
                    context.People.Add(new PersonEntity
                    {
                        NameFirst = "Tim",
                        NameLast = "Jones"
                    });
                    context.SaveChanges();
                }
            }

            Assert.True(true);
            
        }

        [Fact]
        public void InitializeQueryable()
        {
            Assert.True(_context.InitializeQueryable<PersonEntity>().Any());
        }

        [Fact]
        public void GetQueryable()
        {
            Assert.True(_context.GetQueryable(new OltSearcherGetAll<PersonEntity>()).Any());
        }
    }
}