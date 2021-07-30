using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OLT.Core;
using OLT.Libraries.UnitTest.Extensions;

namespace OLT.Libraries.UnitTest
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOltUnitTesting(new OltInjectionOptions());

            //services.AddTransient<IOltLo, DependencyClass>();
        }

        public void Configure(IServiceProvider provider)
        {
            
        }
    }
}
