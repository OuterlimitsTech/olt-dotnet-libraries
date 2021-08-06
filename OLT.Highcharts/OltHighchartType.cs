using Newtonsoft.Json;
using OLT.Core;

namespace OLT.Highcharts
{
    public class OltHighchartType : IOltHighchartType
    {
        public OltHighchartType() { }

        public OltHighchartType(OltHighchartTypes type)
        {
            ChartType = type;
        }

        [JsonIgnore]
        public OltHighchartTypes ChartType { get; set; }

        public string Type => ChartType.GetCodeEnum();
    }
}