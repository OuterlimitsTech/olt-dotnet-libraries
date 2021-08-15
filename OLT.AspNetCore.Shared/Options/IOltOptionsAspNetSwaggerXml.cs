namespace OLT.Core
{
    public interface IOltOptionsAspNetSwaggerXml
    {
        string CommentsFilePath { get; }
        bool IncludeControllerXmlComments { get; }
    }
}