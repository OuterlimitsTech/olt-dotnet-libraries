namespace OLT.Core
{
    public interface IOltEntitySortable
    {
        /// <summary>
        /// Defines the sort order in which the code value should be displayed
        /// </summary>s
        short SortOrder { get; set; }
    }
}