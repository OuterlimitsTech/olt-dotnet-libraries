namespace OLT.Core
{


    public interface IOltRuleValidate<in TRequest> : IOltRule, IOltInjectableSingleton
        where TRequest : class, IOltRuleRequest
    {
        IOltRuleResult Validate(TRequest request);
    }

    public interface IOltRuleValidate<in TRequest, in TContext> : IOltRule, IOltInjectableSingleton
        where TRequest : class, IOltRuleRequest
        where TContext : class, IOltDbContext
    {
        IOltRuleResult Validate(TRequest request, TContext context);
    }

}