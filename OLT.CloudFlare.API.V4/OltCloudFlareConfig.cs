////using System;
////using System.Collections.Specialized;

////namespace OLT.CloudFlare
////{

  
////    /// <summary>
////    /// Class for reading the library configuration
////    /// </summary>
////    public class OltCloudFlareConfig
////    {
////        /// <summary>
////        /// The value of the configuration element
////        /// </summary>
////        /// 9/5/2013 by Sergi
////        private string value = string.Empty;

////        /// <summary>
////        /// The CloudFlare API configuration section
////        /// </summary>
////        /// 9/5/2013 by Sergi
////        //private NameValueCollection cloudFlareAPI = GetSection("CloudFlareAPI");
////        private NameValueCollection cloudFlareAPI = new NameValueCollection();

////        /// <summary>
////        /// Gets the configuration to check if the CloudFlare support is enabled or not in the application.
////        /// </summary>
////        /// <value>
////        /// {true} if it's enabled. False, otherwise.
////        /// </value>
////        /// 9/5/2013 by Sergi
////        public bool Enabled
////        {
////            get
////            {
////                bool result = false;
////                if (cloudFlareAPI != null)
////                {
////                    value = cloudFlareAPI["Enabled"];
////                    Boolean.TryParse(cloudFlareAPI["Enabled"], out result);
////                }
////                return result;
////            }
////        }

////        /// <summary>
////        /// URL for the client gateway interface.
////        /// </summary>
////        /// <value>
////        /// The base URL.
////        /// </value>
////        /// 9/5/2013 by Sergi
////        public string BaseUrl
////        {
////            get
////            {
////                return cloudFlareAPI != null ? cloudFlareAPI["BaseUrl"] : string.Empty;
////            }
////        }

////        /// <summary>
////        /// Gets the API key made available on the Account page
////        /// </summary>
////        /// <value>
////        /// The API key.
////        /// </value>
////        /// 9/5/2013 by Sergi
////        public string ApiKey
////        {
////            get
////            {
////                return cloudFlareAPI != null ? cloudFlareAPI["ApiKey"] : string.Empty;
////            }
////        }

////        /// <summary>
////        /// Gets the e-mail address associated with the API key.
////        /// </summary>
////        /// <value>
////        /// The email.
////        /// </value>
////        /// 9/5/2013 by Sergi
////        public string Email
////        {
////            get
////            {
////                return cloudFlareAPI != null ? cloudFlareAPI["Email"] : string.Empty;
////            }
////        }

////        //private static NameValueCollection GetSection(string name)
////        //{
////        //    return ConfigurationManager.GetSection("CloudFlareAPI") as NameValueCollection;
////        //}
////    }
////}