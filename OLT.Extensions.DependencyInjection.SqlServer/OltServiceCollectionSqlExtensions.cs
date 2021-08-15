using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace OLT.Core
{
    public static class OltServiceCollectionSqlExtensions
    {

        /// <summary>
        /// Adds Pooled SQL Server DB context  
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        /// <param name="createContext">WARNING!  This method is no longer called</param>
        /// <returns></returns>
        [Obsolete("This extension is being removed in a future release.  USe services.AddDbContextPool<TContext>(optionsBuilder => optionsBuilder.UseSqlServer(connectionString)); ")]
        public static IServiceCollection AddOltSqlServer<TContext>(this IServiceCollection services, string connectionString, Func<DbContextOptions<TContext>, IOltLogService, IOltDbAuditUser, TContext> createContext) where TContext : OltDbContext<TContext>, IOltDbContext
        {
            return services.AddDbContext<TContext>(optionsBuilder => optionsBuilder.UseSqlServer(connectionString));
        }


    }
}