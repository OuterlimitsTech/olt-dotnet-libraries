namespace OLT.Core
{
    public abstract class OltRuleAction<TRequest> : OltRule, IOltRuleAction<TRequest>
        where TRequest : class, IOltRuleRequest
    {
        public abstract IOltRuleResult Execute(TRequest request);
    }

    public abstract class OltRuleAction<TRequest, TContext> : OltRuleValidate<TRequest, TContext>, IOltRuleAction<TRequest, TContext>
        where TRequest : class, IOltRuleRequest
        where TContext : class, IOltDbContext
    {
        public abstract IOltRuleResult Execute(TRequest request, TContext context);
    }

    public abstract class OltRuleAction<TRequest, TContext, TResult> : OltRuleValidate<TRequest, TContext>, IOltRuleAction<TRequest, TContext, TResult>
      where TRequest : class, IOltRuleRequest
      where TContext : class, IOltDbContext
      where TResult : class, IOltRuleResult
    {
        public abstract TResult Execute(TRequest request, TContext context);
    }
}