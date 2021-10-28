using Microsoft.EntityFrameworkCore;
using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity.Configurations;
using OLT.Libraries.UnitTest.Assets.Entity.Models;
using OLT.Libraries.UnitTest.Assets.Entity.Models.GeneralCode;

namespace OLT.Libraries.UnitTest.Assets.Entity
{
    public interface ISqlDatabaseContext : IOltDbContext
    {
        DbSet<PersonEntity> People { get; set; }
        DbSet<UserEntity> Users { get; set; }
        DbSet<ApplicationLogEntity> Logs { get; set; }
        DbSet<PersonTypeCodeEntity> PersonTypes { get; set; }
        DbSet<SecondaryTypeCodeEntity> SecondaryTypes { get; set; }
        DbSet<StatusTypeCodeEntity> StatusTypes { get; set; }
        DbSet<CountryCodeEntity> Countries { get; set; }
        DbSet<NoStringPropertiesEntity> BogusNoString { get; set; }
        DbSet<CodeTableType> CodeTableTypes { get; set; }
        DbSet<Sex> SexTypes { get; set; }
        DbSet<Gender> GenderTypes { get; set; }
    }

    // ReSharper disable once InconsistentNaming
    public class SqlDatabaseContext : OltSqlDbContext<SqlDatabaseContext>, ISqlDatabaseContext
    {

        public SqlDatabaseContext(DbContextOptions<SqlDatabaseContext> options) : base(options)
        {
        }

        public override string DefaultSchema => "Data";
        public override bool DisableCascadeDeleteConvention => true;
        public override DefaultStringTypes DefaultStringType => DefaultStringTypes.Varchar;
        protected override int IdentitySeed => 3100;
        protected override int IdentityIncrement => 1;
        public override bool ApplyGlobalDeleteFilter => true;

        public virtual DbSet<PersonEntity> People { get; set; }
        public virtual DbSet<UserEntity> Users { get; set; }
        public virtual DbSet<ApplicationLogEntity> Logs { get; set; }
        public virtual DbSet<PersonTypeCodeEntity> PersonTypes { get; set; }
        public virtual DbSet<SecondaryTypeCodeEntity> SecondaryTypes { get; set; }
        public virtual DbSet<StatusTypeCodeEntity> StatusTypes { get; set; }
        public virtual DbSet<CountryCodeEntity> Countries { get; set; }
        public virtual DbSet<NoStringPropertiesEntity> BogusNoString { get; set; }
        public virtual DbSet<CodeTableType> CodeTableTypes { get; set; }
        public virtual DbSet<Sex> SexTypes { get; set; }
        public virtual DbSet<Gender> GenderTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OltEntityTypeConfigurationFromEnum<CodeTableType, CodeTableTypes>());
            modelBuilder.ApplyConfiguration(new PersonTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SecondaryTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StatusTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SexConfiguration());
            modelBuilder.ApplyConfiguration(new GenderConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }


    public class SqlDatabaseContext2 : OltSqlDbContext<SqlDatabaseContext2>, ISqlDatabaseContext
    {

        public SqlDatabaseContext2(DbContextOptions<SqlDatabaseContext2> options) : base(options)
        {
        }

        public override string DefaultSchema => null;
        public override bool DisableCascadeDeleteConvention => false;
        public override DefaultStringTypes DefaultStringType => DefaultStringTypes.NVarchar;
        protected override int IdentitySeed => 6780;
        protected override int IdentityIncrement => 5;
        public override bool ApplyGlobalDeleteFilter => false;

        public virtual DbSet<PersonEntity> People { get; set; }
        public virtual DbSet<UserEntity> Users { get; set; }
        public virtual DbSet<ApplicationLogEntity> Logs { get; set; }
        public virtual DbSet<PersonTypeCodeEntity> PersonTypes { get; set; }
        public virtual DbSet<SecondaryTypeCodeEntity> SecondaryTypes { get; set; }
        public virtual DbSet<StatusTypeCodeEntity> StatusTypes { get; set; }
        public virtual DbSet<CountryCodeEntity> Countries { get; set; }
        public virtual DbSet<NoStringPropertiesEntity> BogusNoString { get; set; }
        public virtual DbSet<CodeTableType> CodeTableTypes { get; set; }
        public virtual DbSet<Sex> SexTypes { get; set; }
        public virtual DbSet<Gender> GenderTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OltEntityTypeConfigurationFromEnum<CodeTableType, CodeTableTypes>());
            modelBuilder.ApplyConfiguration(new PersonTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SecondaryTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StatusTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SexConfiguration());
            modelBuilder.ApplyConfiguration(new GenderConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}