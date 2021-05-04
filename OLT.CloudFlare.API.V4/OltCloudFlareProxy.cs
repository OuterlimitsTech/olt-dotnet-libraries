using System;
using System.Net;
using System.Text;
using RestSharp;

namespace OLT.CloudFlare
{
    public class OltCloudFlareProxy
    {

        // Objects to encapsulate API methods
        //public Zones Zones = new Zones();
        //public Methods.Dns Dns = new Methods.Dns();


        // Config values
        //public OltCloudFlareConfig Config = new OltCloudFlareConfig();
        protected OltCloudFlareOptions Options { get; }

        // API Access Configuration
        //private readonly string BaseUrl;
        //private readonly string _apiKey;
        //private readonly string _email;
        //private readonly string _zoneName;
        //private readonly string record;

        public OltCloudFlareProxy(OltCloudFlareOptions options)
        {
            Options = options;
            //BaseUrl = options.BaseUrl;
            //_email = options.Email;
            //_apiKey = options.ApiKey;
            //_zoneName = options.ZoneName;

            //if (options != null)
            //{
            //    BaseUrl = options.BaseUrl ?? Config.BaseUrl;
            //    _apiKey = options.ApiKey ?? Config.ApiKey;
            //    _email = options.Email ?? Config.Email;
            //    zoneName = options.ZoneName;
            //    record = options.Record;
            //}
            //else
            //{
            //    BaseUrl = Config.BaseUrl;
            //    _apiKey = Config.ApiKey;
            //    _email = Config.Email;
            //}
        }


        /// <summary>
        /// Executes the request and tries to deserialize the response to T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public T Execute<T>(RestRequest request, string method) where T : new()
        {
            var client = new RestClient
            {
                BaseUrl = new Uri(Options.BaseUrl + method)
            };

            client.AddDefaultHeader(OltCloudFlareHeaders.Authorization, $"Bearer {Options.ApiKey}");
            //client.AddDefaultHeader(OltCloudFlareHeaders.ApiKey, _apiKey);
            //client.AddDefaultHeader(OltCloudFlareHeaders.Email, _email);
            client.AddDefaultHeader("Content-Type", "application/json");

            var response = client.Execute<T>(request);

            // Checking call success
            HandleRequestError(response);
            HandleResponseError(response.Data as OltCloudFlareResponseBase);

            return response.Data;
        }


        /// <summary>
        /// Handles errors in the request (Network, transport, etc.)
        /// </summary>
        /// <param name="response">The response.</param>
        private void HandleRequestError(IRestResponse response)
        {
            bool success = true;
            string message = "";
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                message =
                    $"Network error. Response status: {response.ResponseStatus}. Error: {response.ErrorMessage}";
                success = false;

            }
            if (response.StatusCode != HttpStatusCode.OK)
            {
                message =
                    $"Server error. HTTP Status code: {response.StatusCode}. Error: {response.Content}";
                success = false;
            }
            if (response.ErrorException != null)
            {
                message = "Error retrieving response. Check inner details for more info.";
                success = false;
            }
            if (success) return;

            var proxyException = new ApplicationException(message, response.ErrorException);
            throw proxyException;
        }

        /// <summary>
        /// Handles an error in the remote service.
        /// </summary>
        /// <param name="response">The response.</param>
        private static void HandleResponseError(OltCloudFlareResponseBase response)
        {
            if (response.Success.HasValue && response.Success.Value) return;

            StringBuilder sb = new StringBuilder();
            foreach (var e in response.Errors)
            {
                sb.AppendLine(e.Code + ": " + e.Message);
            }
            var proxyException = new ApplicationException(sb.ToString());
            throw proxyException;
        }

        

    }
}