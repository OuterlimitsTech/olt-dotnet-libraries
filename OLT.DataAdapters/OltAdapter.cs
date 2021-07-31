using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLT.Core
{
    public abstract class OltAdapter<TObj1, TObj2> : OltAdapterCore, IOltAdapter<TObj1, TObj2>
        //where TObj2 : new()
    {
        public virtual string Name => BuildName<TObj1, TObj2>();

        public abstract void Map(TObj1 source, TObj2 destination);
        public abstract void Map(TObj2 source, TObj1 destination);

        public virtual IEnumerable<TObj2> Map(IEnumerable<TObj1> sourceItems)
        {
            ConcurrentBag<TObj2> result = new ConcurrentBag<TObj2>();
            Parallel.ForEach(sourceItems, obj1 =>
            {
                //var obj2 = new TObj2();
                var obj2 = (TObj2)Activator.CreateInstance(typeof(TObj2));
                Map(obj1, obj2);
                result.Add(obj2);
            });
            return result.ToList();
        }

        public virtual IEnumerable<TObj1> Map(IEnumerable<TObj2> sourceItems)
        {
            ConcurrentBag<TObj1> result = new ConcurrentBag<TObj1>();
            Parallel.ForEach(sourceItems, obj2 =>
            {
                var obj1 = (TObj1)Activator.CreateInstance(typeof(TObj1));
                Map(obj2, obj1);
                result.Add(obj1);
            });
            return result.ToList();
        }


    }

}