using System.Collections.Generic;

namespace OLT.Highcharts
{
    public abstract class OltHighchartOptionsBase<TSeries, TType> : IOltHighchartOptions<TSeries, TType>
        where TSeries : class, IOltHighchartSeries
        where TType : class, IOltHighchartType
    {
        public abstract List<TSeries> Series { get; set; }
        public abstract TType Chart { get; set; }
        //public virtual OltHighchartTooltip Tooltip { get; set; } = new OltHighchartTooltip();
        //public virtual OltHighchartTitle Title { get; set; } = new OltHighchartTitle();
        public virtual OltHighchartsCredits Credits { get; set; } = new OltHighchartsCredits();
        
    }
}