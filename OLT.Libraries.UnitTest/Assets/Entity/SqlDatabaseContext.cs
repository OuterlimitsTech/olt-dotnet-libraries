using Microsoft.EntityFrameworkCore;
using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity.Configurations;
using OLT.Libraries.UnitTest.Assets.Entity.Models;

namespace OLT.Libraries.UnitTest.Assets.Entity
{
    // ReSharper disable once InconsistentNaming
    public class SqlDatabaseContext : OltSqlDbContext<SqlDatabaseContext>
    {

        public SqlDatabaseContext(DbContextOptions<SqlDatabaseContext> options) : base(options)
        {
        }

        public override string DefaultSchema => "Data";
        public override bool DisableCascadeDeleteConvention => true;
        public override DefaultStringTypes DefaultStringType => DefaultStringTypes.Varchar;
        protected override int IdentitySeed => 3100;
        protected override int IdentityIncrement => 1;


        public virtual DbSet<PersonEntity> People { get; set; }
        public virtual DbSet<UserEntity> Users { get; set; }
        public virtual DbSet<ApplicationLogEntity> Logs { get; set; }
        public virtual DbSet<PersonTypeCodeEntity> PersonTypes { get; set; }
        public virtual DbSet<StatusTypeCodeEntity> StatusTypes { get; set; }
        public virtual DbSet<CountryCodeEntity> Countries { get; set; }
        public virtual DbSet<NoStringPropertiesEntity> BogusNoString { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StatusTypeConfiguration());

            //modelBuilder.SetSoftDeleteFilter<PersonEntity>();
            //modelBuilder.ApplyGlobalFilters<IOltEntityDeletable>(p => p.DeletedOn == null);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}