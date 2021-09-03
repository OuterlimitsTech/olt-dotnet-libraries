using Microsoft.EntityFrameworkCore;
using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity.Models;

namespace OLT.Libraries.UnitTest.Assets.Entity
{
    // ReSharper disable once InconsistentNaming
    public class SqlDatabaseContext : OltSqlDbContext<SqlDatabaseContext>
    {

        public SqlDatabaseContext(DbContextOptions<SqlDatabaseContext> options) : base(options)
        {
        }

        public override string DefaultSchema => "dbo";
        public override bool DisableCascadeDeleteConvention => true;
        public override DefaultStringTypes DefaultStringType => DefaultStringTypes.NVarchar;
        protected override int IdentitySeed => 3100;
        protected override int IdentityIncrement => 1;


        public virtual DbSet<PersonEntity> People { get; set; }
        public virtual DbSet<UserEntity> Users { get; set; }
        public virtual DbSet<ApplicationLogEntity> Logs { get; set; }

        ////protected override void OnModelCreating(ModelBuilder modelBuilder)
        ////{
        ////    modelBuilder.SetSoftDeleteFilter<PersonEntity>();

        ////    modelBuilder.ApplyGlobalFilters<IOltEntityDeletable>(p => p.DeletedOn == null);

        ////    base.OnModelCreating(modelBuilder);
        ////}
    }
}