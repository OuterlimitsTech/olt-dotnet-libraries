using System;
using System.Linq;
using OLT.Core;
using OLT.Libraries.UnitTest.Abstract;
using OLT.Libraries.UnitTest.Assets.Entity;
using OLT.Libraries.UnitTest.Assets.LocalServices;
using OLT.Libraries.UnitTest.Assets.Models;
using OLT.Libraries.UnitTest.Assets.Rules;
using Xunit;
using Xunit.Abstractions;

namespace OLT.Libraries.UnitTest.Rules
{
    public class ActionRule : BaseTest
    {
        private readonly IPersonService _personService;
        private readonly IOltRuleManager _ruleManager;
        private readonly SqlDatabaseContext _context;
        private readonly PersonAutoMapperModel _dtoAutoMapperModel;

        public ActionRule(
            IPersonService personService,
            SqlDatabaseContext context,
            IOltRuleManager ruleManager,
            ITestOutputHelper output) : base(output)
        {
            _personService = personService;
            _ruleManager = ruleManager;
            _context = context;

            // We need a records for these unit tests
            _dtoAutoMapperModel = UnitTestHelper.AddPerson(_personService, UnitTestHelper.CreateTestAutoMapperModel());
        }

        
        [Fact]
        public void GetRules()
        {
            var rules = _ruleManager.GetRules<IDoSomethingRule>();
            Assert.True(rules.Count == 4);
        }

        [Fact]
        public void ValidationRule()
        {
            var rule = _ruleManager.GetRule<IDoSomethingRuleDb>();
            Assert.True(rule.Validate(new DoSomethingRuleContextRequest(_context)).Success);
        }

        [Fact]
        public void GetByInterface()
        {
            var rule = _ruleManager.GetRule<IDoSomethingRuleDb>();
            Assert.True(rule.Execute(new DoSomethingRuleContextRequest(_context)).Success);
        }

        [Fact]
        public void GetByConcreteClass()
        {
            var rule = _ruleManager.GetRule<DoSomethingRuleOne>();
            Assert.True(rule.Execute(new DoSomethingRuleRequest()).Success);
        }

        [Fact]
        public void NotFound()
        {
            Assert.Throws<OltRuleNotFoundException>(() => _ruleManager.GetRule<INotValidRule>());
        }

        [Fact]
        public void Failure()
        {
            var result = UnitTestHelper.AddPerson(_personService, UnitTestHelper.CreateTestAutoMapperModel());
            var rule = _ruleManager.GetRule<DoSomethingRuleFailure>();
            Assert.Throws<OltRuleException>(() => rule.Execute(new DoSomethingPersonRuleRequest(result)));
        }

        [Fact]
        public void ValidationInvalid()
        {
            var rule = _ruleManager.GetRule<DoSomethingRuleIntValue>();
            Assert.True(rule.Validate(new DoSomethingRuleIntRequest(_context, 5)).Invalid);
        }

        [Fact]
        public void ValidationInvalidMessages()
        {
            var rule = _ruleManager.GetRule<DoSomethingRuleIntValue>();
            var result = rule.Validate(new DoSomethingRuleIntRequest(_context, 5));
            Assert.True(result.Results.Any());
        }
    }
}