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

        protected abstract int IdentitySeed { get; }
        protected abstract int IdentityIncrement { get; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.EntitiesOfType<IOltEntityId>(builder =>
            {
                var prop = builder.Property<int>(nameof(IOltEntityId.Id));
                if (prop.Metadata.ValueGenerated == ValueGenerated.OnAdd && !prop.Metadata.GetIdentitySeed().HasValue)
                {
                    prop.UseIdentityColumn(IdentitySeed, IdentityIncrement);
                }
                //Console.WriteLine($"{builder.Metadata.GetTableName()} of type {builder.Metadata.ClrType.FullName} -> GetIdentitySeed: {prop.Metadata.GetIdentitySeed()} -> IdentitySeed: {IdentitySeed}{Environment.NewLine}");
            });

            base.OnModelCreating(modelBuilder);
        }
    }

   
}
