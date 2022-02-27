using Microsoft.Extensions.Logging;

namespace OLT.Core
{
    public abstract class OltHostServiceBase : OltDisposable, IOltHostService
    {
        private IOltEnvironment _environment;

        public abstract string ResolveRelativePath(string filePath);
        public abstract string EnvironmentName { get; }
        public virtual IOltEnvironment Environment => _environment ?? (_environment = new OltEnvironment(EnvironmentName));

    }
}