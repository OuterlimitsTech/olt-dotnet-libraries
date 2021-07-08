using Microsoft.Extensions.DependencyInjection;

namespace OLT.Core
{
    public class OltAspNetCoreCorsPolicyWildcard : IOltAspNetCoreCorsPolicy
    {
        public OltAspNetCoreCorsPolicyOptions CorsPolicy => OltAspNetCoreCorsPolicyOptions.Wildcard;
        public string PolicyName => "Olt_CorsPolicy_Wildcard";

        /// <summary>
        /// Sets CORS policy
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceCollection AddOltCors(IServiceCollection services)
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