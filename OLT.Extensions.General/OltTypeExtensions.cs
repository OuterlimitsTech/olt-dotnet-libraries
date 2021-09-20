using System.IO;

// ReSharper disable once CheckNamespace
namespace System.Reflection
{
    public static class OltReflectionTypeExtensions
    {

        /// <summary>
        /// Searches Assembly for embedded resource. This method does not require the Fully Qualified Name
        /// </summary>
        /// <remarks>
        /// Example: MyNamespace.SomeDirectory.TheFile.csv -> this.GetType().Assembly.GetEmbeddedResourceStream("TheFile.csv")
        /// </remarks>
        /// <param name="assembly"><see cref="Assembly"/></param>
        /// <param name="resourceName"></param>
        /// <exception cref="ArgumentNullException"><param name="resourceName"></param> name null</exception>
        /// <exception cref="ArgumentException"><param name="resourceName"></param> empty string</exception>
        /// <exception cref="FileNotFoundException"><param name="resourceName"></param> is not found</exception>
        /// <returns>return stream of embedded resource or null if the <param name="resourceName"></param> is not found</returns>
        public static Stream GetEmbeddedResourceStream(this Assembly assembly, string resourceName)
        {
            if (resourceName == null)
            {
                throw new ArgumentNullException(nameof(resourceName));
            }

            if (string.IsNullOrWhiteSpace(resourceName))
            {
                throw new ArgumentException($"{resourceName} cannot be null or whitespace");
            }

            // Get all embedded resources
            string[] arrResources = assembly.GetManifestResourceNames();

            foreach (string name in arrResources)
            {
                if (name.Contains(resourceName))
                {
                    return assembly.GetManifestResourceStream(name);
                }
            }

            throw new FileNotFoundException("Cannot find embedded resource.", resourceName);
        }

        /// <summary>
        /// Searches Assembly for embedded resource and save it to <param name="fileName"></param> rile. This method does not require the Fully Qualified Name
        /// </summary>
        /// <remarks>
        /// Example: MyNamespace.SomeDirectory.TheFile.csv -> this.GetType().Assembly.EmbeddedResourceToFile("TheFile.csv")
        /// </remarks>
        /// <param name="assembly"><see cref="Assembly"/></param>
        /// <param name="resourceName"></param>
        /// <param name="fileName"></param>
        /// <exception cref="ArgumentNullException"><param name="resourceName"></param> is null</exception>
        /// <exception cref="ArgumentException"><param name="resourceName"></param> is empty string</exception>
        /// <exception cref="FileNotFoundException"><param name="resourceName"></param> is not found</exception>
        /// <returns>return stream of embedded resource or null if the <param name="resourceName"></param> is not found</returns>
        public static void EmbeddedResourceToFile(this Assembly assembly, string resourceName, string fileName)
        {
            if (resourceName == null)
            {
                throw new ArgumentNullException(nameof(resourceName));
            }

            if (string.IsNullOrWhiteSpace(resourceName))
            {
                throw new ArgumentException($"{resourceName} cannot be null or whitespace");
            }

            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException($"{fileName} cannot be null or whitespace");
            }

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            using (var stream = GetEmbeddedResourceStream(assembly, resourceName))
            {
                using (FileStream output = new FileStream(fileName, FileMode.Create))
                {
                    stream.CopyTo(output);
                }
            }

        }


        /// <summary>
        /// return string for embedded resource
        /// </summary>
        /// <remarks>
        /// Example: MyNamespace.SomeDirectory.TheFile.csv -> this.GetType().Assembly.GetEmbeddedResourceString("TheFile.csv")
        /// </remarks>
        /// <param name="assembly"><see cref="Assembly"/></param>
        /// <param name="resourceName"></param>
        /// <exception cref="ArgumentNullException"><param name="resourceName"></param> is null</exception>
        /// <exception cref="ArgumentException"><param name="resourceName"></param> is empty string</exception>
        /// <exception cref="FileNotFoundException"><param name="resourceName"></param> is not found</exception>
        /// <returns>return stream of embedded resource or null if the <param name="resourceName"></param> is not found</returns>
        public static string GetEmbeddedResourceString(this Assembly assembly, string resourceName)
        {
            if (resourceName == null)
            {
                throw new ArgumentNullException(nameof(resourceName));
            }

            if (string.IsNullOrWhiteSpace(resourceName))
            {
                throw new ArgumentException($"{resourceName} cannot be null or whitespace");
            }

            using (StreamReader sr = new StreamReader(GetEmbeddedResourceStream(assembly, resourceName)))
            {
                string data = sr.ReadToEnd();
                sr.Close();
                return data;
            }

        }
    }
}