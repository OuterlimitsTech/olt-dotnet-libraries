namespace OLT.Core
{
    public interface IOltRequest<out TContext>
        where TContext : class, IOltDbContext
    {
        TContext Context { get; }
    }
}