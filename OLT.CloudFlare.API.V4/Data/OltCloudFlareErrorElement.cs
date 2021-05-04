using Newtonsoft.Json;

namespace OLT.CloudFlare
{
    public partial class OltCloudFlareErrorElement
    {
        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public long? Code { get; set; }

        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }
    }
}