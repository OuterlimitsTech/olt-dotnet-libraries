namespace OLT.Core
{
    public interface IOltPagingParams
    {
        int PageNumber { get; set; }
        int PageSize { get; set; }
    }
}