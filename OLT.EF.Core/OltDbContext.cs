using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OLT.Core
{
    public abstract class OltDbContext<TContext> : DbContext, IOltDbContext
        where TContext: DbContext, IOltDbContext
    {
        private IOltDbAuditUser _dbAuditUser;

        protected OltDbContext(DbContextOptions<TContext> options) : base(options)
        {

        }


        public enum DefaultStringTypes
        {
            NVarchar,
            Varchar
        }

        protected virtual IOltDbAuditUser DbAuditUser
        {
            get => _dbAuditUser ??= this.GetService<IOltDbAuditUser>();
            set => _dbAuditUser = value;
        }


        public abstract string DefaultSchema { get; }
        public abstract bool DisableCascadeDeleteConvention { get; }
        public virtual string DefaultAnonymousUser => "GUEST USER";
        public abstract DefaultStringTypes DefaultStringType { get; }

        public virtual string AuditUser
        {
            get
            {
                if (DbAuditUser != null)
                {
                    var userName = DbAuditUser.GetDbUsername();
                    if (!string.IsNullOrWhiteSpace(userName))
                    {
                        return userName;
                    }

                }

                return DefaultAnonymousUser;
            }
        }

        public virtual Task<int> SaveChangesAsync()
        {
            return this.SaveChangesAsync(CancellationToken.None);
        }

        public override int SaveChanges()
        {

            var entries = this.ChangeTracker.Entries().ToList();
            var changed = entries.Where(p => p.State == EntityState.Added || p.State == EntityState.Modified).ToList();

            //Set any empty string to null
            foreach (var entityEntry in changed)
            {
                if (entityEntry.Entity is IOltEntityAudit createModel)
                {
                    if (entityEntry.State == EntityState.Added)
                    {
                        createModel.CreateUser ??= AuditUser;
                        createModel.CreateDate = createModel.CreateDate == DateTimeOffset.MinValue ? DateTimeOffset.UtcNow : createModel.CreateDate;

                    }

                    createModel.ModifyUser = AuditUser;
                    createModel.ModifyDate = DateTimeOffset.UtcNow;
                }

                if (entityEntry.Entity is IOltEntityUniqueId uniqueModel && uniqueModel.UniqueId == Guid.Empty)
                {
                    uniqueModel.UniqueId = Guid.NewGuid();
                }

                if (entityEntry.Entity is IOltEntitySortable sortOrder && sortOrder.SortOrder <= 0)
                {
                    sortOrder.SortOrder = 9999;
                }

                if (entityEntry.State == EntityState.Added)
                {
                    (entityEntry.Entity as IOltInsertingRecord)?.InsertingRecord(this, entityEntry);
                }

                if (entityEntry.State == EntityState.Modified)
                {
                    (entityEntry.Entity as IOltUpdatingRecord)?.UpdatingRecord(this, entityEntry);
                }

                if (entityEntry.State == EntityState.Deleted)
                {
                    (entityEntry.Entity as IOltDeletingRecord)?.DeletingRecord(this, entityEntry);
                }

                var str = nameof(String);
                var properties = from p in entityEntry.Entity.GetType().GetProperties()
                                 where p.PropertyType.Name == str
                                 select p;

                foreach (var item in properties)
                {
                    var value = (string)item.GetValue(entityEntry.Entity, null);
                    if (string.IsNullOrWhiteSpace(value) && item.CanWrite)
                    {
                        item.SetValue(entityEntry.Entity, null, null);
                    }
                }
            }

            return base.SaveChanges();
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            if (DisableCascadeDeleteConvention)
            {
                var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                    .SelectMany(t => t.GetForeignKeys())
                    .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

                foreach (var fk in cascadeFKs)
                {
                    fk.DeleteBehavior = DeleteBehavior.Restrict;
                }
            }

            if (DefaultStringType == DefaultStringTypes.Varchar)
            {
                foreach (var property in modelBuilder.Model.GetEntityTypes()
                    .SelectMany(t => t.GetProperties())
                    .Where(p => p.ClrType == typeof(string)))
                {
                    if (property.IsUnicode().GetValueOrDefault(true))
                    {
                        property.SetIsUnicode(false);
                    }
                }
            }


            if (string.IsNullOrWhiteSpace(DefaultSchema))
            {
                modelBuilder.HasDefaultSchema(DefaultSchema);  //Sets Schema for all tables, unless overridden
            }


            modelBuilder.EntitiesOfType<IOltEntityId>(builder =>
            {
                var prop = builder.Property<int>(nameof(IOltEntityId.Id));
                if (prop.Metadata.GetColumnName(StoreObjectIdentifier.Table(builder.Metadata.GetTableName(), builder.Metadata.GetSchema())).Equals("Id", StringComparison.OrdinalIgnoreCase))
                {
                    var columnName = $"{builder.Metadata.GetTableName()}Id";
                    builder.Property<int>(nameof(IOltEntityId.Id)).HasColumnName(columnName);
                }
            });


            base.OnModelCreating(modelBuilder);
        }

        
    }
}