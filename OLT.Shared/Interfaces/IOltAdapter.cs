using System.Collections.Generic;

namespace OLT.Core
{
    public interface IOltAdapter : IOltInjectableSingleton
    {
        string Name { get; }
    }

    public interface IOltAdapter<TObj1, TObj2> : IOltAdapter
    {
        void Map(TObj1 source, TObj2 destination);
        void Map(TObj2 source, TObj1 destination);
        IEnumerable<TObj2> Map(IEnumerable<TObj1> sourceItems);
        IEnumerable<TObj1> Map(IEnumerable<TObj2> sourceItems);
    }
}