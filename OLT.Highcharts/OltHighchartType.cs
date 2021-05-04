using Newtonsoft.Json;
using OLT.Core;

namespace OLT.Highcharts
{
    public interface IOltHighchartType
    {
        OltHighchartTypes ChartType { get; set; }
        string Type { get; }
    }

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

    public class OltHighchartPieType : OltHighchartType
    {
        public OltHighchartPieType() { } 

        public OltHighchartPieType(OltHighchartTypes type) : base(type)
        {
            
        }

        public string PlotBackgroundColor { get; set; }
        public string PlotBorderWidth { get; set; }
        public bool PlotShadow { get; set; } = false;
    }
}