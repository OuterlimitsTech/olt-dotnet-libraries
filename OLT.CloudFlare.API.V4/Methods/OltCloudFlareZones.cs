using System.Linq;
using RestSharp;

namespace OLT.CloudFlare
{
    public class OltCloudFlareZones : OltCloudFlareProxy
    {


        public OltCloudFlareZones(OltCloudFlareOptions options) : base(options)
        {
        }

        public OltCloudFlareZoneResult[] GetZones(string name = "", int page = 1, int pageSize = 50)
        {
            var method = "zones";
            if (string.IsNullOrEmpty(name))
                name = Options.ZoneName;

            var request = new RestRequest(Method.GET);

            request.AddParameter(OltCloudFlareParameters.Page, page);
            request.AddParameter(OltCloudFlareParameters.PageSize, pageSize);


            if (!string.IsNullOrEmpty(name))
            {
                request.AddParameter(OltCloudFlareParameters.Name, name);
            }
                


            var response = Execute<OltCloudFlareSimpleResponse>(request, method);
            return OltCloudFlareZoneResult.FromJson(response.Result.ToString());
        }

        public OltCloudFlareZoneResult GetZone(string name)
        {
            if (string.IsNullOrEmpty(name))
                name = Options.ZoneName;

            var zones = GetZones(name);
            return zones.Any() ? zones.FirstOrDefault() : null;
        }
        
    }
}
