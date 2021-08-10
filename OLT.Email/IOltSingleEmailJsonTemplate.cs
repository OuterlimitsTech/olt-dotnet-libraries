namespace OLT.Email
{
    public interface IOltSingleEmailJsonTemplate<out TEmailAddress, out TModel> : IOltSingleEmailTemplate<TEmailAddress>
        where TEmailAddress : class, IOltEmailAddress
        where TModel : class
    {
        TModel TemplateData { get; }
    }
}