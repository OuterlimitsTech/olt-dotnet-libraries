namespace OLT.Core
{
    public class OltPagedSearchJson<T, TC> : OltPagedJson<T>
        where T : class
        where TC : class
    {
        public virtual string Key { get; set; }
        public virtual TC Criteria { get; set; }
    }
}