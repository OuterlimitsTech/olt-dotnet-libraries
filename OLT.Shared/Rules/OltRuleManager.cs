using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace OLT.Core
{
    public class OltRuleManager : OltDisposable, IOltRuleManager
    {
        private readonly List<IOltRule> _rules;

        public OltRuleManager(IServiceProvider serviceProvider)
        {
            _rules = serviceProvider.GetServices<IOltRule>().ToList();
        }

        public virtual TRule GetRule<TRule>()
            where TRule : class, IOltRule
        {
            var ruleName = typeof(TRule).FullName;
            var rule = _rules.FirstOrDefault(p => p.RuleName == ruleName) ??
                       _rules.FirstOrDefault(p => p.GetType().Implements<TRule>());
            if (rule == null)
            {
                throw new OltRuleNotFoundException(typeof(TRule));
            }
            return rule as TRule;
        }


        public virtual List<TRule> GetRules<TRule>()
            where TRule : IOltRule
        {
            return _rules.OfType<TRule>().ToList();
        }
    }
}