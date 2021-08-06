namespace OLT.Core
{
    public abstract class OltRequest<TContext> : OltRequest
        where TContext : class, IOltDbContext<TContext>
    {
        protected OltRequest(TContext context)
        {
            Context = context;
        }

        public TContext Context { get; }
    }

}