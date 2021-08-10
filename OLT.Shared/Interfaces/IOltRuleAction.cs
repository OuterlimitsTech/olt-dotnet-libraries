namespace OLT.Core
{
    public interface IOltRuleAction : IOltRule, IOltInjectableSingleton
    {
        IOltResultValidation CanExecute(IOltRequest request);
        IOltResult Execute(IOltRequest request);
    }

    public interface IOltRuleAction<in TRequest> : IOltRule, IOltInjectableSingleton
        where TRequest : class, IOltRequest
    {
        IOltResultValidation CanExecute(TRequest request);
        IOltResult Execute(TRequest request);
    }

}