namespace OLT.Core
{
    public interface IOltRuleValidation : IOltRule, IOltInjectableSingleton
    {
        IOltResultValidation Validate(IOltRequest request);
    }

    public interface IOltRuleValidation<in TRequest> : IOltRule, IOltInjectableSingleton
        where TRequest : IOltRequest
    {
        IOltResultValidation Validate(TRequest request);
    }

    public interface IOltRuleValidation<in TRequest, out TResult> : IOltRule, IOltInjectableSingleton
        where TRequest : IOltRequest
        where TResult: IOltResultValidation
    {
        TResult Validate(TRequest request);
    }
}