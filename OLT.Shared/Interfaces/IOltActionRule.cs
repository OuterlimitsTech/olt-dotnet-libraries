namespace OLT.Core
{
    public interface IOltActionRule : IOltRule, IOltInjectableSingleton
    {
        IOltRuleResult CanExecute(IOltRuleRequest request);
        IOltRuleResult Execute(IOltRuleRequest request);
    }

    public interface IOltActionRule<in TRequest> : IOltRule, IOltInjectableSingleton
        where TRequest : class, IOltRuleRequest
    {
        IOltRuleResult CanExecute(TRequest request);
        IOltRuleResult Execute(TRequest request);
    }

}