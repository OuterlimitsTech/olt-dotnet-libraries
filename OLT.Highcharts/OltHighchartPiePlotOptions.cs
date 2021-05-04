namespace OLT.Highcharts
{


    public class OltHighchartPiePlotOptions
    {
        public virtual bool AllowPointSelect { get; set; } = true;
        public virtual string Cursor { get; set; } = "pointer";
        public virtual OltHighchartPiePlotLabel DataLabels { get; set; } = new OltHighchartPiePlotLabel();
    }
}