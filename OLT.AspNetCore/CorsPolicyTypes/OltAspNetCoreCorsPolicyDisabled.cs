using Microsoft.Extensions.DependencyInjection;

namespace OLT.Core
{
    public class OltAspNetCoreCorsPolicyDisabled : IOltAspNetCoreCorsPolicy   
    {
        public OltAspNetCoreCorsPolicyOptions CorsPolicy => OltAspNetCoreCorsPolicyOptions.Disabled;
        public string PolicyName => "Olt_CorsPolicy_Disabled";

        /// <summary>
        /// Sets CORS policy
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceCollection AddOltCors(IServiceCollection services)
        {

            return services; //Do Nothing
        }
    }
}