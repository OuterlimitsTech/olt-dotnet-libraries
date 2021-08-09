namespace OLT.Core
{
    public abstract class OltRuleAction<TRequest> : OltRule, IOltRuleAction<TRequest>
        where TRequest : class, IOltRequest
    {
        public abstract IOltResult Execute(TRequest request);
    }


    public abstract class OltRuleAction<TRequest, TResult> : OltRuleValidate<TRequest>, IOltRuleAction<TRequest, TResult>
      where TRequest : class, IOltRequest
      where TResult : class, IOltResult
    {
        public abstract TResult Execute(TRequest request);
    }
}