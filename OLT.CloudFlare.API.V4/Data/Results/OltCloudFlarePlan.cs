using Newtonsoft.Json;

namespace OLT.CloudFlare
{
    public class OltCloudFlarePlan
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
        public long? Price { get; set; }

        [JsonProperty("currency", NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; }

        [JsonProperty("frequency", NullValueHandling = NullValueHandling.Ignore)]
        public string Frequency { get; set; }

        [JsonProperty("legacy_id", NullValueHandling = NullValueHandling.Ignore)]
        public string LegacyId { get; set; }

        [JsonProperty("is_subscribed", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsSubscribed { get; set; }

        [JsonProperty("can_subscribe", NullValueHandling = NullValueHandling.Ignore)]
        public bool? CanSubscribe { get; set; }
    }
}