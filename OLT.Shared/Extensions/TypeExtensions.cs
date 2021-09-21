using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

// ReSharper disable once CheckNamespace
namespace System
{

    public static class TypeExtensions
    {

       

        public static IEnumerable<T> GetAllImplements<T>(this Assembly[] assemblies)
        {
            return GetAllImplements<T>(assemblies.ToList());
        }

        public static IEnumerable<T> GetAllImplements<T>(this List<Assembly> assemblies)
        {

            foreach (var assembly in assemblies)
            {
                Assembly loaded = null;

                try
                {
                    loaded = Assembly.Load(assembly.GetName());
                }
                catch
                {
                    // ignored
                }

                if (loaded != null)
                {
                    foreach (var ti in loaded.DefinedTypes)
                    {
                        if (ti.ImplementedInterfaces.Contains(typeof(T)) && !ti.IsAbstract && !ti.IsInterface && !ti.IsGenericType)
                        {
                            yield return (T)assembly.CreateInstance(ti.FullName);
                        }
                    }

                }


            }
        }



       
     

    }
}