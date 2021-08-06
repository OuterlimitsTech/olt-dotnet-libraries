namespace OLT.Highcharts
{
    public interface IOltHighchartType
    {
        OltHighchartTypes ChartType { get; set; }
        string Type { get; }
    }
}