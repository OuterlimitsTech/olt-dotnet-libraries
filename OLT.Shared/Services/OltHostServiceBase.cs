using Microsoft.Extensions.Logging;

namespace OLT.Core
{
    public abstract class OltHostServiceBase : OltDisposable, IOltHostService
    {
        public abstract string ResolveRelativePath(string filePath);
        public abstract string EnvironmentName { get; }
        public abstract string ApplicationName { get; }

    }
}