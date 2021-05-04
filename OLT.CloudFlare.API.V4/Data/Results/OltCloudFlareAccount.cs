using Newtonsoft.Json;

namespace OLT.CloudFlare
{
    public class OltCloudFlareAccount
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
    }
}