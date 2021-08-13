namespace OLT.Core
{
    public interface IOltOptionsAspNetSwaggerUI : IOltOptionsAspNetSwagger
    {
        string Description { get; }
        string Title { get; }
    }

    public interface IOltOptionsAspNetSwagger
    {
        bool Enabled { get; }        
        IOltOptionsAspNetSwaggerXml XmlSettings { get; }
    }
}