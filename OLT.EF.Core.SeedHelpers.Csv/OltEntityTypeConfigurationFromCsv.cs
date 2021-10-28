using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using CsvHelper;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// ReSharper disable once CheckNamespace
namespace OLT.Core
{
    public abstract class OltEntityTypeConfigurationFromCsv<TEntity, TCsvModel> : OltEntityTypeConfiguration<TEntity>
        where TEntity : class, IOltEntity, IOltEntityId, new()
        where TCsvModel : class, IOltCsvSeedModel<TEntity>
    {
        
        protected abstract string ResourceName { get; }
        protected abstract Assembly ResourceAssembly { get; }

        public virtual List<TCsvModel> Load()
        {
            using Stream stream = ResourceAssembly.GetEmbeddedResourceStream(ResourceName);
            using StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            CsvReader csvReader = new CsvReader(reader, CultureInfo.CurrentCulture);
            return csvReader.GetRecords<TCsvModel>().ToList();
        }

        protected virtual void Seed(EntityTypeBuilder<TEntity> builder, Action<TEntity, TCsvModel> setProperties = null)
        {
            var list = new List<TEntity>();

            Load().ForEach(csvRecord =>
                {
                    var item = new TEntity
                    {
                        Id = csvRecord.Id
                    };

                    if (item is IOltEntitySortable sortableEntity)
                    {
                        sortableEntity.SortOrder = (short)9999;
                    }

                    if (item is IOltEntityAudit auditEntity)
                    {
                        auditEntity.CreateUser = DefaultUsername;
                        auditEntity.CreateDate = DefaultCreateDate;
                    }

                    csvRecord.Map(item);

                    setProperties?.Invoke(item, csvRecord);

                    list.Add(item);

                });

            if (list.Any())
            {
                builder.HasData(list);
            }

        }

        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            this.Seed(builder);
        }

    }
}