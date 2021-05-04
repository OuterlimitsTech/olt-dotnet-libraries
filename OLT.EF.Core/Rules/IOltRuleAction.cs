namespace OLT.Core
{
    public interface IOltRuleAction<in TRequest> : IOltRule, IOltInjectableSingleton
        where TRequest : class, IOltRuleRequest
    {
        IOltRuleResult Execute(TRequest request);
    }

    public interface IOltRuleAction<in TRequest, in TContext> : IOltRuleValidate<TRequest, TContext>
        where TRequest : class, IOltRuleRequest
        where TContext : class, IOltDbContext
    {
        IOltRuleResult Execute(TRequest request, TContext context);
    }

    public interface IOltRuleAction<in TRequest, in TContext, out TResult> : IOltRuleValidate<TRequest, TContext>
        where TRequest : class, IOltRuleRequest
        where TContext : class, IOltDbContext
        where TResult : class, IOltRuleResult
    {
        TResult Execute(TRequest request, TContext context);
    }
}