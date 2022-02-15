namespace OLT.Core
{
    public abstract class OltRequestContext<TContext> : IOltRequest
        where TContext : class, IOltDbContext
    {
        protected OltRequestContext(TContext context)
        {
            Context = context;
        }

        public TContext Context { get; }
    }

    public abstract class OltRequestContext<TContext, TValue> : OltRequest<TValue>
        where TContext : class, IOltDbContext
    {
        protected OltRequestContext(TContext context, TValue value) : base(value)
        {
            Context = context;
        }

        public TContext Context { get; }


    }
}