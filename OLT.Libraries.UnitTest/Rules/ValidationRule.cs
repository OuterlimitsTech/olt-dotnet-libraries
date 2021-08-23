using System;
using OLT.Core;
using OLT.Libraries.UnitTest.Abstract;
using OLT.Libraries.UnitTest.Assets.Entity;
using OLT.Libraries.UnitTest.Assets.Rules;
using Xunit;
using Xunit.Abstractions;

namespace OLT.Libraries.UnitTest.Rules
{
    public class ActionRule : BaseTest
    {
        private readonly IOltRuleManager _ruleManager;
        private readonly SqlDatabaseContext _context;

        public ActionRule(
            SqlDatabaseContext context,
            IOltRuleManager ruleManager,
            ITestOutputHelper output) : base(output)
        {
            _ruleManager = ruleManager;
            _context = context;
        }

        [Fact]
        public void GetRules()
        {
            var rules = _ruleManager.GetRules<IDoSomethingRule>();
            Assert.True(rules.Count == 2);
        }

        [Fact]
        public void CheckValidationRule()
        {
            var rule = _ruleManager.GetRule<IDoSomethingRuleDb>();
            Assert.True(rule.Validate(new DoSomethingRuleDbRequest(_context)).Success);
        }

        [Fact]
        public void CheckActionRule()
        {
            var rule = _ruleManager.GetRule<IDoSomethingRuleDb>();
            Assert.True(rule.Execute(new DoSomethingRuleDbRequest(_context)).Success);
        }
    }
}