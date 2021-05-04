using Newtonsoft.Json;

namespace OLT.CloudFlare
{
    public class OltCloudFlareShortDnsRecord
    {
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("content", NullValueHandling = NullValueHandling.Ignore)]
        public string Content { get; set; }

        [JsonProperty("proxied", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Proxied { get; set; }

        [JsonProperty("ttl", NullValueHandling = NullValueHandling.Ignore)]
        public long? Ttl { get; set; }

        public OltCloudFlareShortDnsRecord()
        {
        }

        public OltCloudFlareShortDnsRecord(OltCloudFlareDnsResult result)
        {
            Type = result.Type;
            Name = result.Name;
            Content = result.Content;
            Proxied = result.Proxied;
            Ttl = result.Ttl;
        }
    }
}
