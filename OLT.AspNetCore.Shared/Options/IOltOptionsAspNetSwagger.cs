namespace OLT.Core
{
    public interface IOltOptionsAspNetSwagger
    {
        bool Enabled { get; }        
        IOltOptionsAspNetSwaggerXml XmlSettings { get; }
    }
}