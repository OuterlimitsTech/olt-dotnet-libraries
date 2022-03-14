namespace OLT.Core
{
    public interface IOltEntityMaintainable : IOltEntity
    {
        bool? MaintAdd { get; set; }
        bool? MaintUpdate { get; set; }
        bool? MaintDelete { get; set; }
    }
}