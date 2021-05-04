using Newtonsoft.Json;

namespace OLT.CloudFlare
{
    public static class ShortDnsResultSerializer
    {
        public static string ToJson(this OltCloudFlareShortDnsRecord self) => JsonConvert.SerializeObject(self, OltCloudFlareConverter.Settings);
    }
}