namespace OLT.CloudFlare
{
    public class OltCloudFlareOptions
    {
        public OltCloudFlareOptions(string apiKey, string zoneName = null)
        {
            ApiKey = apiKey;
            ZoneName = zoneName;
        }

        public string ApiKey { get; set; }

        public string BaseUrl { get; set; } = "https://api.cloudflare.com/client/v4/";

        public string ZoneName { get; set; }

    }
}
