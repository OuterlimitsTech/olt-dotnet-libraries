namespace OLT.Core
{
    public interface IOltPersonName
    {
        string First { get; set; }
        string Middle { get; set; }
        string Last { get; set; }
        string Suffix { get; set; }
        string FullName { get; }
    }
}