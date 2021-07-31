using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace OLT.Core
{
    /// <summary>
    /// Usage:  options.UseSqlServer(Configuration.GetConnectionString("DBContextConnectionString")).ReplaceService<IMigrationsSqlGenerator, OltMigrationsSqlGenerator>()
    /// Adds SQL Temporal table Audit tables for every table
    /// </summary>
    public class OltMigrationsSqlAuditGenerator : SqlServerMigrationsSqlGenerator
    {
        public OltMigrationsSqlAuditGenerator(MigrationsSqlGeneratorDependencies dependencies, IRelationalAnnotationProvider migrationsAnnotations) : base(dependencies, migrationsAnnotations)
        {
        }

        protected override void Generate(
            MigrationOperation operation,
            IModel model,
            Microsoft.EntityFrameworkCore.Migrations.MigrationCommandListBuilder builder)
        {
            if (operation is AddColumnOperation addColumnOperation)
            {
                Generate(addColumnOperation, builder);
            }
            else if (operation is CreateTableOperation createTableOperation)
            {
                Generate(createTableOperation, builder);

            }
            else
            {
                base.Generate(operation, model, builder);
            }
        }

        private void Generate(
            CreateTableOperation operation,
            MigrationCommandListBuilder builder)
        {


            string schemaString = "";
            if (operation.Schema != null)
            {
                schemaString = $"[{operation.Schema}].";
            }

            builder
                .Append($@"
                CREATE TABLE {schemaString}[{operation.Name}] (")
                .AppendLine();

            foreach (AddColumnOperation col in operation.Columns)
            {
                builder.Append($"{GetColumnGenString(col)},").AppendLine();
            }
            builder.Append($@"
                    [AuditUser]        NVARCHAR (MAX)                              NULL,
                    [AuditModifiedDate] DATETIME2 (7) GENERATED ALWAYS AS ROW START NOT NULL,
                    [AuditEndDate]     DATETIME2 (7) GENERATED ALWAYS AS ROW END   NOT NULL,
                 
                    PERIOD FOR SYSTEM_TIME ([AuditModifedDate], [AuditEndDate]),
                    PRIMARY KEY( {string.Join(",", operation.PrimaryKey.Columns)})
                )
            WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE=[Audit].[{operation.Name}], DATA_CONSISTENCY_CHECK=ON));     
            ").EndCommand();
        }


        private void Generate(AddColumnOperation operation, MigrationCommandListBuilder builder)
        {
            var sqlHelper = Dependencies.SqlGenerationHelper;
            var stringMapping = Dependencies.TypeMappingSource.FindMapping(typeof(string));

            var columnDefString = GetColumnGenString(operation);

            string schemaString = "";
            if (operation.Schema != null)
            {
                schemaString = $"[{operation.Schema}].";
            }

            builder
                .Append($@"
                  IF NOT EXISTS 
                (
                    SELECT * 
                    FROM INFORMATION_SCHEMA.COLUMNS 
                    WHERE table_name = '{operation.Table}'
                    AND column_name = '{operation.Name}'
                )
                BEGIN
                    ALTER TABLE {schemaString}[{operation.Table}]
                                ADD {columnDefString}
                END
            ELSE
                BEGIN

                 print 'Column [{operation.Name}] already exists in Table {schemaString}[{operation.Table}]'

                END
            ")
                .EndCommand();

        }
        private string GetColumnGenString(AddColumnOperation operation)
        {
            var type = "";
            //var nullable = "";

            if (operation.ClrType == typeof(string))  // Add more for other native types
                type = "NVARCHAR(MAX)";
            else if (operation.ClrType == typeof(int))
                type = "INT";
            else if (operation.ClrType == typeof(bool))
                type = "BIT";
            else if (operation.ClrType == typeof(DateTime))
                type = "DATETIME2 (7)";
            else if (operation.ClrType == typeof(Guid))
                type = "UNIQUEIDENTIFIER";

            string nullString = "NULL";  // Check for other properties.

            if (operation.IsNullable == false)
                nullString = "NOT NULL";

            //string schemaString = "";
            //if (operation.Schema != null)
            //{
            //    schemaString = $"[{operation.Schema}].";
            //}

            string retColString = $"[{operation.Name}] {type} {nullString}";

            return retColString;
        }
    }
}