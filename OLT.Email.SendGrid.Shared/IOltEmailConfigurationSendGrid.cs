// ReSharper disable once CheckNamespace
namespace OLT.Email.SendGrid
{
    public interface IOltEmailConfigurationSendGrid : IOltEmailConfiguration
    {
        string ApiKey { get; }
        bool ClickTracking { get; }
    }


}