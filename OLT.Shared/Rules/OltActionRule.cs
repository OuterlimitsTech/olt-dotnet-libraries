namespace OLT.Core
{
    public abstract class OltRuleAction : OltRuleValidation, IOltRuleAction
    {
        public abstract IOltResult Execute(IOltRequest request);
    }

    public abstract class OltRuleAction<TRequest> : OltRuleValidation<TRequest>, IOltRuleAction<TRequest>
        where TRequest : class, IOltRequest
    {
        public abstract IOltResult Execute(TRequest request);
    }

    public abstract class OltRuleAction<TRequest, TResult> : OltRuleValidation<TRequest>, IOltRuleAction<TRequest, TResult>
        where TRequest : IOltRequest
        where TResult : IOltResult
    {
        public abstract TResult Execute(TRequest request);
    }

    public abstract class OltRuleAction<TRequest, TResult, TValidationResult> : OltRuleValidation<TRequest, TValidationResult>, IOltRuleAction<TRequest, TResult, TValidationResult>
        where TRequest : IOltRequest
        where TResult : IOltResult
        where TValidationResult: IOltResultValidation
    {
        public abstract TResult Execute(TRequest request);
    }
}
