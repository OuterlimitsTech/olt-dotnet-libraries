using Microsoft.Extensions.DependencyInjection;

namespace OLT.Core
{
    public interface IOltAspNetCoreCorsPolicy
    {
        OltAspNetCoreCorsPolicyOptions CorsPolicy { get; }
        string PolicyName { get; }

        /// <summary>
        /// Sets CORS policy
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        IServiceCollection AddOltCors(IServiceCollection services);
    }
}