using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OLT.Core;
using OLT.Libraries.UnitTest.Assests.Entity;
using OLT.Libraries.UnitTest.Assests.Extensions;

namespace OLT.Libraries.UnitTest
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOltUnitTesting(new OltInjectionOptions());
            var optionsBuilder = new DbContextOptionsBuilder<SqlDatabaseContext>().UseInMemoryDatabase(databaseName: "Test");
            services.AddOltSqlServer(optionsBuilder, (options, logService, auditUser) => new SqlDatabaseContext(options, logService, auditUser));

            //services.AddScoped<IPersonService, PersonService>();
            //services.AddTransient<IOltLo, DependencyClass>();
        }

        public void Configure(IServiceProvider provider)
        {
            
        }
    }
}
