using System.Collections.Generic;

namespace OLT.Core
{
    public interface IOltRuleManager : IOltInjectableScoped
    {
        TRule GetRule<TRule>() where TRule : class, IOltRule;
        List<TRule> GetRules<TRule>() where TRule : IOltRule;
    }
}