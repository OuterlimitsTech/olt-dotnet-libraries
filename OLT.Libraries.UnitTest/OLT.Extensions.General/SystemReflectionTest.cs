using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using OLT.Core;
using Xunit;

namespace OLT.Libraries.UnitTest.OLT.Extensions.General
{
    public class SystemReflectionTest
    {
        private readonly IOltRuleManager _ruleManager;
        public SystemReflectionTest(IOltRuleManager ruleManager)
        {
            _ruleManager = ruleManager;
        }

        [Fact]
        public void GetAllImplements()
        {
            var baseAssemblies = new List<Assembly>
            {
                Assembly.GetEntryAssembly(), 
                Assembly.GetExecutingAssembly()
            };
            var assembliesToScan = baseAssemblies.GetAllReferencedAssemblies();
            Assert.Equal(_ruleManager.GetRules<IOltRule>().Count, assembliesToScan.GetAllImplements<IOltRule>().Count());
            Assert.Equal(_ruleManager.GetRules<IOltRule>().Count, assembliesToScan.ToArray().GetAllImplements<IOltRule>().Count());
        }
    }
}