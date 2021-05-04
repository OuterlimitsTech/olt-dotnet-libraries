using System.Collections.Generic;

namespace OLT.Highcharts
{
    public class OltHighchartPieSeries : IOltHighchartSeries
    {
        public string Name { get; set; }
        public bool ColorByPoint { get; set; } = true;
        public List<OltHighchartPieSeriesData> Data { get; set; } = new List<OltHighchartPieSeriesData>();
    }
}