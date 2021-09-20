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



        /// <summary>
        /// Determines if a type implements the <typeparamref name="TInterface"/> interface.
        /// this.GetType().Implements<IMyInterface>()
        /// </summary>
        /// <typeparam name="TInterface">Interface</typeparam>
        /// <param name="type">Extends <see cref="Type"/>.</param>
        /// <returns>True if type implements interface</returns>
        public static bool Implements<TInterface>(this Type type) where TInterface : class
        {
            var interfaceType = typeof(TInterface);

            if (!interfaceType.IsInterface)
                throw new InvalidOperationException("Only interfaces can be implemented.");

            return (interfaceType.IsAssignableFrom(type));
        }

        /// <summary>
        /// Determines if a type implements the <typeparamref name="TInterface"/> interface.
        /// this.GetType().Implements(typeof(IMyInterface<>))
        /// </summary>
        /// <typeparam name="TInterface">Interface</typeparam>
        /// <param name="type">Extends <see cref="Type"/>.</param>
        /// <param name="@interface"><see cref="TInterface"/>.</param>
        /// <returns>True if type implements interface</returns>
        public static bool Implements<TInterface>(this Type type, TInterface @interface) where TInterface : class
        {
            if (!(@interface is Type))
            {
                throw new ArgumentException($"{nameof(@interface)} not of type {type.FullName}");
            }

            if (!(@interface as Type).IsInterface)
            {
                throw new ArgumentException("Only interfaces can be 'implemented'.");
            }

            return type.GetInterfaces()
                .Where(i => i.IsGenericType)
                .Any(i => i.GetGenericTypeDefinition() == (@interface as Type));
        }

     

    }
}