namespace OLT.Core
{
    public interface IOltOptionsAspNetSwaggerUI : IOltOptionsAspNetSwagger
    {
        string Description { get; }
        string Title { get; }
    }
}