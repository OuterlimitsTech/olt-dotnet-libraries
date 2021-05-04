namespace OLT.Highcharts
{
    public class OltHighchart<TOptions>
        where TOptions : class, IOltHighchartOptions, new()
    {
        public TOptions Options { get; set; } = new TOptions();
    }
}