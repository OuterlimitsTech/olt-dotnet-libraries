namespace OLT.Core
{
    public abstract class OltAdapterCore : OltDisposable
    {
        protected virtual string BuildName<TSource, TModel>()
        {
            return $"{typeof(TSource).FullName}->{typeof(TModel).FullName}";
        }
    }
}