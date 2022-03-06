using Microsoft.Extensions.DependencyInjection;
using OLT.Constants;

namespace OLT.Core
{
    public class OltAspNetCoreCorsPolicyDisabled : IOltAspNetCoreCorsPolicy   
    {
        public string PolicyName => OltAspNetDefaults.CorsPolicies.Disabled;

        /// <summary>
        /// Sets CORS policy
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceCollection AddCors(IServiceCollection services)
        {

            return services; //Do Nothing
        }
    }
}