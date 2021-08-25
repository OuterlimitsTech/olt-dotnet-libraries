using Microsoft.EntityFrameworkCore;
using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity.Models;
using OLT.Libraries.UnitTest.Assets.LocalServices;

namespace OLT.Libraries.UnitTest.Assets.Entity
{
    public class SqlLiteDatabaseContext : OltSqlDbContext<SqlLiteDatabaseContext>
    {
        public SqlLiteDatabaseContext(DbContextOptions<SqlLiteDatabaseContext> options) : base(options)
        {
            DbAuditUser = new OltUnitTestAppIdentity();
        }


        protected override IOltDbAuditUser DbAuditUser { get; set; }

        public override string DefaultSchema => "dbo";
        public override bool DisableCascadeDeleteConvention => true;
        public override DefaultStringTypes DefaultStringType => DefaultStringTypes.NVarchar;
        protected override int IdentitySeed => 3100;
        protected override int IdentityIncrement => 1;


        public virtual DbSet<PersonEntity> People { get; set; }
        public virtual DbSet<UserEntity> Users { get; set; }
        public virtual DbSet<ApplicationLogEntity> Logs { get; set; }
    }
}