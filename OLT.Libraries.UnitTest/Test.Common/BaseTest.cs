using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OLT.Core;
using OLT.Extensions.DependencyInjection.AutoMapper;
using OLT.Libraries.UnitTest.Extensions;

namespace OLT.Libraries.UnitTest.Test.Common
{
    [TestClass]
    public class Initialize
    {
        // https://www.michalbialecki.com/2020/11/28/unit-tests-in-entity-framework-core-5/
        // https://www.c-sharpcorner.com/article/unit-testing-using-xunit-and-moq-in-asp-net-core/

        public static IServiceCollection Services { get; private set; }
        public static ServiceProvider Provider { get; private set; }

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {

            Services = new ServiceCollection();
            Services.AddOltUnitTesting(new OltInjectionOptions());

            Provider = Services.BuildServiceProvider();

            Console.WriteLine("AssemblyInitialize");
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            Provider.Dispose();
            Console.WriteLine("AssemblyCleanup");
        }
    }

}