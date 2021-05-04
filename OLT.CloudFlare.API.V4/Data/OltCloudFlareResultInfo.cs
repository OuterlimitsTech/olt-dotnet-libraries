using Newtonsoft.Json;

namespace OLT.CloudFlare
{
    public class OltCloudFlareResultInfo
    {
        [JsonProperty("page", NullValueHandling = NullValueHandling.Ignore)]
        public long? Page { get; set; }

        [JsonProperty("per_page", NullValueHandling = NullValueHandling.Ignore)]
        public long? PerPage { get; set; }

        [JsonProperty("count", NullValueHandling = NullValueHandling.Ignore)]
        public long? Count { get; set; }

        [JsonProperty("total_count", NullValueHandling = NullValueHandling.Ignore)]
        public long? TotalCount { get; set; }
    }
}