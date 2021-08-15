using Microsoft.Extensions.DependencyInjection;

namespace OLT.Core
{
    public interface IOltAspNetCoreCorsPolicy
    {
        string PolicyName { get; }

        /// <summary>
        /// Sets CORS policy
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        IServiceCollection AddCors(IServiceCollection services);
    }
}