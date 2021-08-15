using System;
using System.Linq;
using RestSharp;

namespace OLT.CloudFlare
{
    public class OltCloudFlareDns : OltCloudFlareProxy
    {
        #region " Attributes "

        /// <summary>
        /// The request
        /// </summary>
        public OltCloudFlareDns(OltCloudFlareOptions options) : base(options)
        {
        }


        public OltCloudFlareDnsResult[] GetDnsRecords(string zoneName, string name = "", int page = 1, int pageSize = 50)
        {
            var method = "zones/{0}/dns_records";
            var zoneId = GetZoneId(zoneName);
            if (!string.IsNullOrEmpty(zoneId))
            {
                method = string.Format(method, zoneId);

                var request = new RestRequest(Method.GET);

                request.AddParameter(OltCloudFlareParameters.Page, page);
                request.AddParameter(OltCloudFlareParameters.PageSize, pageSize);

                if (!string.IsNullOrEmpty(name))
                    request.AddParameter(OltCloudFlareParameters.Name, $"{name.ToLower()}.{zoneName}");

                var response = Execute<OltCloudFlareSimpleResponse>(request, method);
                return OltCloudFlareDnsResult.FromJson(response.Result.ToString());
            }
            else
            {
                var proxyException = new ApplicationException("No Zone was found with given name: " + zoneName);
                throw proxyException;
            }
        }

        public OltCloudFlareDnsResult GetDnsRecord(string zoneName, string name)
        {
            var records = GetDnsRecords(zoneName, name);
            return records.Any() ? records.FirstOrDefault() : null;
        }

        public bool DeleteDnsRecord(string zoneName, string name)
        {
            var zoneId = GetZoneId(zoneName);
            if (!string.IsNullOrEmpty(zoneId))
            {
                var record = GetDnsRecord(zoneName, name);
                if (record != null)
                {
                    RestRequest request = new RestRequest(Method.DELETE);
                    var response = Execute<OltCloudFlareSimpleResponse>(request, $"zones/{zoneId}/dns_records/{record.Id}");
                    return response.Success.HasValue && response.Success.Value;
                }
            }
            return false;
        }

        public bool CreateDnsRecord(string zoneName, string name, string content, string type)
        {
            var zoneId = GetZoneId(zoneName);
            if (!string.IsNullOrEmpty(zoneId))
            {
                var record = GetDnsRecord(zoneName, name);
                if (record == null)
                {
                    var newRecord = new OltCloudFlareShortDnsRecord
                    {
                        Name = $"{name}.{zoneName}",
                        Ttl = 1,
                        Type = type,
                        Content = content,
                        Proxied = true
                    };

                    RestRequest request = new RestRequest(Method.POST);
                    request.RequestFormat = DataFormat.Json;
                    request.AddParameter("application/json; charset=utf-8", newRecord.ToJson(), ParameterType.RequestBody);

                    var response = Execute<OltCloudFlareSimpleResponse>(request, $"zones/{zoneId}/dns_records");
                    return response.Success.HasValue && response.Success.Value;
                }
            }
            return false;
        }

        public bool EnableDnsRecord(string zoneName, string name, bool enable = true)
        {
            var method = "zones/{0}/dns_records/{1}";
            var zoneId = GetZoneId(zoneName);
            if (!string.IsNullOrEmpty(zoneId))
            {
                var record = GetDnsRecord(zoneName, name);
                if (record != null)
                {
                    method = string.Format(method, zoneId, record.Id);
                    record.Proxied = enable;

                    RestRequest request = new RestRequest(Method.PUT);
                    OltCloudFlareShortDnsRecord updatedRecord = new OltCloudFlareShortDnsRecord(record);
                    request.RequestFormat = DataFormat.Json;
                    request.AddParameter("application/json; charset=utf-8", updatedRecord.ToJson(), ParameterType.RequestBody);

                    var response = Execute<OltCloudFlareSimpleResponse>(request, method);
                    return response.Success.HasValue && response.Success.Value;
                }
            }
            return false;
        }

        public bool DisableDnsRecord(string zoneName, string name)
        {
            return EnableDnsRecord(zoneName, name, false);
        }

        #endregion

        #region " Private methods "

        private string GetZoneId(string zoneName)
        {
            OltCloudFlareZones zonesProxy = new OltCloudFlareZones(Options);
            var zone = zonesProxy.GetZone(zoneName);
            return (zone != null) ? zone.Id : string.Empty;
        }

        #endregion
    }

}
