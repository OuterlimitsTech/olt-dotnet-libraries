namespace OLT.Core
{
    public interface IOltActionRule : IOltRule, IOltInjectableSingleton
    {
        IOltResult CanExecute(IOltRequest request);
        IOltResult Execute(IOltRequest request);
    }

    public interface IOltActionRule<in TRequest> : IOltRule, IOltInjectableSingleton
        where TRequest : class, IOltRequest
    {
        IOltResult CanExecute(TRequest request);
        IOltResult Execute(TRequest request);
    }

}