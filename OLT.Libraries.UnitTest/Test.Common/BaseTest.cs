using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetCore.AutoRegisterDi;
using OLT.Core;

namespace OLT.Libraries.UnitTest.Test.Common
{
    //[TestClass]
    //public class TestStartup
    //{
    //    [ClassInitialize]
    //    public void Setup
    //    {

    //    }

    //}

    [TestClass]
    public class Initialize
    {

        
        public static IServiceCollection Services { get; private set; }
        public static ServiceProvider Provider { get; private set; }

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            Services = new ServiceCollection();

            var assembliesToScan = new List<Assembly>
            {
                Assembly.GetExecutingAssembly()
            };

            assembliesToScan.AddRange(Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(Assembly.Load).ToList());


            //var all = assembliesToScan.Where(type => typeof(IOltInjectable).IsAssignableFrom(type)).ToArray();

            //var all = Assembly.GetEntryAssembly()
            //    .GetReferencedAssemblies()
            //    .Select(Assembly.Load)
            //    //.SelectMany(x => x.GetTypes())
            //    //.Where(type => typeof(IOltInjectable).IsAssignableFrom(type))
            //    .ToArray();

            Services.RegisterAssemblyPublicNonGenericClasses(assembliesToScan.ToArray())
                .Where(type => typeof(IOltInjectableScoped).IsAssignableFrom(type))
                .AsPublicImplementedInterfaces();

            //assembliesToScan
            //    .SelectMany(x => x.GetTypes())
            //    .Where(type => typeof(IOltAdapter).IsAssignableFrom(type))
            //    .Where(y => y.IsClass && !y.IsAbstract && !y.IsGenericType && !y.IsNested)
            //    .ToList()
            //    .ForEach(type =>
            //    {
            //        Console.WriteLine(type.FullName);
            //        Services.AddTransient(typeof(IOltAdapter), type);
            //    });
                //.ForEach(type => Services.AddTransient(typeof(IOltAdapter), type));


            //Services.RegisterAssemblyPublicNonGenericClasses(all).AsPublicImplementedInterfaces();

            //Services.AddTransient<IOltLogService, OltLogService>();
            



            Provider = Services.BuildServiceProvider();
            var tt = Provider.GetService<IOltLogService>();

            Console.WriteLine("AssemblyInitialize");
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            Provider.Dispose();
            Console.WriteLine("AssemblyCleanup");
        }
    }

    public static class ServiceCollectionExtensions
    {
        public static void RegisterAllTypes<T>(this IServiceCollection services, Assembly[] assemblies,
            ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(T))));
            foreach (var type in typesFromAssemblies)
                services.Add(new ServiceDescriptor(typeof(T), type, lifetime));
        }
    }
}