namespace OLT.Core
{

    public interface IOltRuleValidate<in TRequest> : IOltRule, IOltInjectableSingleton
        where TRequest : class, IOltRequest
    {
        IOltResult Validate(TRequest request);
    }

}