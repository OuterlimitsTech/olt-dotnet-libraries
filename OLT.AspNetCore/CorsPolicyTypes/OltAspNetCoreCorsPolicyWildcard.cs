using Microsoft.Extensions.DependencyInjection;
using OLT.Constants;

namespace OLT.Core
{
    public class OltAspNetCoreCorsPolicyWildcard : IOltAspNetCoreCorsPolicy
    {

        public string PolicyName => OltAspNetDefaults.CorsPolicies.Wildcard;

        /// <summary>
        /// Sets CORS policy
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceCollection AddCors(IServiceCollection services)
        {

            return services.AddCors(
                o => o.AddPolicy(PolicyName, builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                }));
        }
    }
}