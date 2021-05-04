namespace OLT.Highcharts
{
    public class OltHighchartStyle
    {
        public virtual string Color { get; set; }
    }


    public abstract class OltHighchartPiePlotLabelBase
    {
        public virtual bool Enabled { get; set; }
        public virtual string Format { get; set; }
        public virtual OltHighchartStyle Style { get; set; } = new OltHighchartStyle { Color = "black" };
    }


    public class OltHighchartPiePlotLabel : OltHighchartPiePlotLabelBase
    {
        
    }
}