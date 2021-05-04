namespace OLT.Core
{

    public abstract class OltRuleValidate<TRequest> : OltRule, IOltRuleValidate<TRequest>
        where TRequest : class, IOltRuleRequest
    {
        public abstract IOltRuleResult Validate(TRequest request);
    }

    public abstract class OltRuleValidate<TRequest, TContext> : OltRule, IOltRuleValidate<TRequest, TContext>
        where TRequest : class, IOltRuleRequest
        where TContext : class, IOltDbContext
    {
        public abstract IOltRuleResult Validate(TRequest request, TContext context);
    }
}