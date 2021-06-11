using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OLT.Core
{
    public abstract class OltSqlDbContext<TContext> : OltDbContext<TContext>
        where TContext : DbContext, IOltDbContext
    {

        protected OltSqlDbContext()
        {

        }

        protected OltSqlDbContext(DbContextOptions<TContext> options) : base(options)
        {

        }

        protected OltSqlDbContext(DbContextOptions<TContext> options, IOltLogService logService, IOltDbAuditUser dbAuditUser) : base(options, logService, dbAuditUser)
        {
        }

        protected abstract int IdentitySeed { get; }
        protected abstract int IdentityIncrement { get; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.EntitiesOfType<IOltEntityId>(builder =>
            {
                var prop = builder.Property<int>(nameof(IOltEntityId.Id));
                if (prop.Metadata.ValueGenerated == ValueGenerated.OnAdd && prop.Metadata.GetIdentitySeed() < IdentitySeed)
                {
                    builder.Property<int>(nameof(IOltEntityId.Id)).UseIdentityColumn(IdentitySeed, IdentityIncrement);
                }
            });

            base.OnModelCreating(modelBuilder);
        }
    }

   
}
