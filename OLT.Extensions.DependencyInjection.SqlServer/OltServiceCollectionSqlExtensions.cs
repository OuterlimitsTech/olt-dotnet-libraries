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
        /// <param name="createContext">Return a new instance of Context</param>
        /// <returns></returns>
        public static IServiceCollection AddOltSqlServer<TContext>(this IServiceCollection services, string connectionString, Func<DbContextOptions<TContext>, IOltLogService, IOltDbAuditUser, TContext> createContext) where TContext : OltDbContext<TContext>, IOltDbContext
        {
            var optionsBuilder = new DbContextOptionsBuilder<TContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return services.AddOltSqlServer(optionsBuilder, createContext);
        }


        /// <summary>
        /// Adds Pooled SQL Server DB context  
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="services"></param>
        /// <param name="optionsBuilder"></param>
        /// <param name="createContext">Return a new instance of Context</param>
        /// <returns></returns>
        public static IServiceCollection AddOltSqlServer<TContext>(this IServiceCollection services, DbContextOptionsBuilder<TContext> optionsBuilder, Func<DbContextOptions<TContext>, IOltLogService, IOltDbAuditUser, TContext> createContext) where TContext : OltDbContext<TContext>, IOltDbContext
        {

            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (optionsBuilder == null)
            {
                throw new ArgumentNullException(nameof(optionsBuilder));
            }


            services.AddDbContextPool<TContext>(optionsBuilder => { optionsBuilder.UseSqlServer("Injected by DI"); });

            services.AddScoped(ctx =>
            {
                var context = createContext(optionsBuilder.Options, ctx.GetService<IOltLogService>(), ctx.GetService<IOltDbAuditUser>());
                return context;
            });

            return services;
        }

    }
}