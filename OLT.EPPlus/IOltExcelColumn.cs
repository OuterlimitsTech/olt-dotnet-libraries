namespace OLT.EPPlus
{
    public interface IOltExcelColumn : IOltExcelCellWriter
    {
        string Heading { get; set; }
        decimal? Width { get; set; }
        string Format { get; set; }
    }
}