using System;
using Microsoft.EntityFrameworkCore;

namespace OLT.Core
{
    public static class OltSqlExtensionHelpers
    {
        public static void OltMigrateDb(this DbContext context, bool isDevelopment, bool inDebug, string prodConnectionStringSearchFor = "database.windows.net")
        {
            var migrateDb = true;
            if (context.IsProductionDb(prodConnectionStringSearchFor))
            {
                migrateDb = !isDevelopment && !inDebug;
            }

            if (migrateDb)
            {
                context.Database.Migrate();
            }
        }

        public static bool IsProductionDb(this DbContext context, string searchFor = "database.windows.net")
        {
            return context.Database.GetConnectionString().Contains(searchFor, StringComparison.OrdinalIgnoreCase);
        }

 
    }
}