namespace OLT.Core
{
    public interface IOltRuleAction : IOltRuleValidation
    {
        IOltResult Execute(IOltRequest request);
    }

    public interface IOltRuleAction<in TRequest> : IOltRuleValidation<TRequest>
        where TRequest : IOltRequest
    {
        IOltResult Execute(TRequest request);
    }

    public interface IOltRuleAction<in TRequest, out TResult> : IOltRuleValidation<TRequest>
        where TRequest : IOltRequest
        where TResult : IOltResult
    {
        TResult Execute(TRequest request);
    }

    public interface IOltRuleAction<in TRequest, out TResult, out TValidationResult> : IOltRuleValidation<TRequest, TValidationResult>
        where TRequest : IOltRequest
        where TResult : IOltResult
        where TValidationResult : IOltResultValidation
    {
        TResult Execute(TRequest request);
    }
}