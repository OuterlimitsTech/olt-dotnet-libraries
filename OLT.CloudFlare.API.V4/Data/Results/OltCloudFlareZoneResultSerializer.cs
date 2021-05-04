using Newtonsoft.Json;

namespace OLT.CloudFlare
{
    public static class OltCloudFlareZoneResultSerializer
    {
        public static string ToJson(this OltCloudFlareZoneResult[] self) => JsonConvert.SerializeObject(self, OltCloudFlareConverter.Settings);
    }
}