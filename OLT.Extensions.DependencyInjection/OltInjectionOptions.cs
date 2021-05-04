namespace OLT.Core
{
    public interface IOltInjectionOptions
    {
        int CacheExpirationMinutes { get; }
    }

    public class OltInjectionOptions : IOltInjectionOptions
    {
        public int CacheExpirationMinutes { get; set; } = 30;
    }
}