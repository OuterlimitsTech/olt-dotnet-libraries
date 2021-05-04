using System.Collections.Generic;

namespace OLT.Highcharts
{
    public interface IOltHighchartOptions
    {
        //OltHighchartTitle Title { get; set; }
    }

    public interface IOltHighchartOptions<TSeries, TType> : IOltHighchartOptions
        where TSeries : class, IOltHighchartSeries
        where TType : class, IOltHighchartType

    {
        List<TSeries> Series { get; set; }
        TType Chart { get; set; }
    }
}