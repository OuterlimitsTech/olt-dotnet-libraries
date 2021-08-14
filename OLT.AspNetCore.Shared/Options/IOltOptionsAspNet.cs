namespace OLT.Core
{
    public interface IOltOptionsAspNet
    {
        IOltOptionsAspNetHosting Hosting { get; }
        IOltOptionsAspNetSwaggerUI Swagger { get; }
    }

}