using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

// ReSharper disable once CheckNamespace
namespace System
{

    public static class TypeExtensions
    {

        public static List<Assembly> GetAllReferencedAssemblies(this Assembly assembly)
        {
            return GetAllReferencedAssemblies(new List<Assembly> {assembly});
        }

        public static List<Assembly> GetAllReferencedAssemblies(this Assembly[] assembliesToScan)
        {
            return GetAllReferencedAssemblies(assembliesToScan.ToList());
        }

        public static List<Assembly> GetAllReferencedAssemblies(this List<Assembly> assembliesToScan)
        {
            var results = new List<Assembly>();
            var referencedAssemblies = new List<Assembly>();

            referencedAssemblies.AddRange(assembliesToScan);

            if (assembliesToScan.Any(p => p.FullName != Assembly.GetCallingAssembly().FullName))
            {
                referencedAssemblies.Add(Assembly.GetCallingAssembly());
            }

            assembliesToScan.ForEach(assembly =>
            {
                referencedAssemblies.AddRange(assembly.GetReferencedAssemblies().Select(Assembly.Load));
            });

            AppDomain.CurrentDomain
                .GetAssemblies()
                .ToList()
                .ForEach(assembly =>
                {
                    referencedAssemblies.Add(assembly);
                });


            referencedAssemblies
                .GroupBy(g => g.FullName)
                .Select(s => s.Key)
                .OrderBy(o => o)
                .ToList()
                .ForEach(name =>
                {
                    var assembly = results.FirstOrDefault(p => string.Equals(p.FullName, name, StringComparison.OrdinalIgnoreCase));
                    if (assembly == null)
                    {
                        results.Add(referencedAssemblies.FirstOrDefault(p => p.FullName == name));
                    }
                });


            return results;
        }

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
        /// <returns>True if type implements interface</returns>
        public static bool Implements<TInterface>(this Type type, TInterface @interface) where TInterface : class
        {
            if (((@interface as Type) == null) || !(@interface as Type).IsInterface)
                throw new ArgumentException("Only interfaces can be 'implemented'.");

            //return (@interface as Type).IsAssignableFrom(type);

            return type.GetInterfaces()
                .Where(i => i.IsGenericType)
                .Any(i => i.GetGenericTypeDefinition() == (@interface as Type));
        }

        public static bool EmbeddedResourceToFile(this Assembly assembly, string resourceName, string fileName)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            if (resourceName == null)
            {
                throw new ArgumentNullException(nameof(resourceName));
            }

            if (resourceName.IsEmpty())
            {
                throw new ArgumentException($"{resourceName} cannot be null or whitespace");
            }

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            using (var stream = GetEmbeddedResourceStream(assembly, resourceName))
            {
                using (System.IO.FileStream output = new System.IO.FileStream(fileName, FileMode.Create))
                {
                    stream.CopyTo(output);
                }
            }

            return true;
        }

        public static Stream GetEmbeddedResourceStream(this Assembly assembly, string resourceName)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            if (resourceName == null)
            {
                throw new ArgumentNullException(nameof(resourceName));
            }

            if (resourceName.IsEmpty())
            {
                throw new ArgumentException($"{resourceName} cannot be null or whitespace");
            }

            //Assembly currentAssembly = Assembly.GetExecutingAssembly();
            // Get all embedded resources
            string[] arrResources = assembly.GetManifestResourceNames();

            foreach (string name in arrResources)
            {
                if (name.Contains(resourceName))
                {
                    return assembly.GetManifestResourceStream(name);
                }
            }

            return null;
        }

        //public static StreamReader GetEmbeddedResourceStream(this System.Reflection.Assembly assembly, string name)
        //{
        //    foreach (string resName in assembly.GetManifestResourceNames())
        //    {
        //        if (resName.EndsWith(name))
        //        {
        //            return new StreamReader(assembly.GetManifestResourceStream(resName));
        //        }
        //    }
        //    return null;
        //}

        public static string GetEmbeddedResourceString(this System.Reflection.Assembly assembly, string name)
        {
            using (StreamReader sr = new StreamReader(GetEmbeddedResourceStream(assembly, name)))
            {
                string data = sr.ReadToEnd();
                sr.Close();
                return data;
            }

        }

    }
}