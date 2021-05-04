using System.IO;
using System.Reflection;

// ReSharper disable once CheckNamespace
namespace System
{

    public static class TypeExtensions
    {
        /// <summary>
        /// Determines if a type implements the given interface.
        /// this.GetType().Implements<IDisposable>()
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