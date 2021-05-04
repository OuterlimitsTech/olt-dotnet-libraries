namespace OLT.Core
{
    public class OltPagedSearchData<T, TC> : OltPagedData<T>
        where T : class
        where TC : class
    {
        public virtual string Key { get; set; }
        public virtual TC Criteria { get; set; }
    }
}