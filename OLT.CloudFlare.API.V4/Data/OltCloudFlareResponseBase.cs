using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OLT.CloudFlare
{
    /// <summary>
    /// Class for mapping the common result messages for all the requests
    /// </summary>
    public abstract class OltCloudFlareResponseBase
    {
        //public static OltCloudFlareResponseBase FromJson(string json) => JsonConvert.DeserializeObject<OltCloudFlareResponseBase>(json, OltCloudFlareConverter.Settings);

        [JsonProperty("result", NullValueHandling = NullValueHandling.Ignore)]
        public Object Result { get; set; }

        [JsonProperty("success", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Success { get; set; }

        [JsonProperty("result_info", NullValueHandling = NullValueHandling.Ignore)]
        public OltCloudFlareResultInfo ResultInfo { get; set; }

        [JsonProperty("errors", NullValueHandling = NullValueHandling.Ignore)]
        public List<OltCloudFlareErrorElement> Errors { get; set; }

        [JsonProperty("messages", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Messages { get; set; }

    }
}
