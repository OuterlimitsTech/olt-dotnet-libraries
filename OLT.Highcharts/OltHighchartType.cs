namespace OLT.Highcharts
{
    public class OltHighchartType : IOltHighchartType
    {
        public OltHighchartType() { }

        public OltHighchartType(OltHighchartTypes type)
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            ChartType = type;
        }

        public virtual OltHighchartTypes ChartType { get; set; }

    }
}