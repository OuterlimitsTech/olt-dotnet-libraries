namespace OLT.Core
{
   
    public interface IOltRuleAction<in TRequest> : IOltRule, IOltInjectableSingleton
        where TRequest : class, IOltRequest
    {
        IOltResult Execute(TRequest request);
    }


    public interface IOltRuleAction<in TRequest, out TResult> : IOltRuleValidate<TRequest>
        where TRequest : class, IOltRequest
        where TResult : class, IOltResult
    {
        TResult Execute(TRequest request);
    }
}