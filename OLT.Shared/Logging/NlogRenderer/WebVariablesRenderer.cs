////using System.Globalization;
////using System.Text;
////using System.Web;
////using System.Xml;
////using NLog;
////using NLog.Config;
////using NLog.LayoutRenderers;

////namespace OLT.Core.Services.NlogRenderer
////{
////    [LayoutRenderer("web_variables")]
////    public class WebVariablesRenderer : LayoutRenderer
////    {
////        public WebVariablesRenderer()
////        {
////            this.Format = "";
////            this.Culture = CultureInfo.InvariantCulture;
////        }

////        public CultureInfo Culture { get; set; }

////        [DefaultParameter]
////        public string Format { get; set; }

////        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
////        {
////            StringBuilder sb = new StringBuilder();
////            XmlWriter writer = XmlWriter.Create(sb);

////            writer.WriteStartElement("error");

////            // -----------------------------------------
////            // Server Variables
////            // -----------------------------------------
////            writer.WriteStartElement("serverVariables");

////            foreach (string key in HttpContext.Current.Request.ServerVariables.AllKeys)
////            {
////                writer.WriteStartElement("item");
////                writer.WriteAttributeString("name", key);

////                writer.WriteStartElement("value");
////                writer.WriteAttributeString("string", HttpContext.Current.Request.ServerVariables[key].ToString());
////                writer.WriteEndElement();

////                writer.WriteEndElement();
////            }

////            writer.WriteEndElement();

////            // -----------------------------------------
////            // Cookies
////            // -----------------------------------------
////            writer.WriteStartElement("cookies");

////            foreach (string key in HttpContext.Current.Request.Cookies.AllKeys)
////            {
////                writer.WriteStartElement("item");
////                writer.WriteAttributeString("name", key);

////                writer.WriteStartElement("value");
////                writer.WriteAttributeString("string", HttpContext.Current.Request.Cookies[key].Value.ToString());
////                writer.WriteEndElement();

////                writer.WriteEndElement();
////            }

////            writer.WriteEndElement();
////            // -----------------------------------------

////            writer.WriteEndElement();
////            // -----------------------------------------

////            writer.Flush();
////            writer.Close();

////            string xml = sb.ToString();

////            builder.Append(xml);
////        }

////    }
////}
