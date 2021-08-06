namespace OLT.Highcharts
{
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