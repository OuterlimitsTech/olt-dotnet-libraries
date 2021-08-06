using System.Collections.Generic;

namespace OLT.Highcharts
{
    public class OltHighchartPieOptions : OltHighchartOptionsBase<OltHighchartPieSeries, OltHighchartPieType>
    {
        public OltHighchartPieOptions()
        {
            //Tooltip.PointFormat = "{series.name}: <b>{point.percentage:.1f}%</b>";
        }

        public override List<OltHighchartPieSeries> Series { get; set; } = new List<OltHighchartPieSeries>();

        public override OltHighchartPieType Chart { get; set; } = new OltHighchartPieType(OltHighchartTypes.Pie);
        //public OltHighchartPiePlot PlotOptions { get; set; } = new OltHighchartPiePlot();
        //public override OltHighchartPieSeries Series { get; set; } = new OltHighchartPieSeries();
    }
}