// ReSharper disable once CheckNamespace

namespace OLT.Core
{
    public abstract class OltHostServiceBase : OltDisposable, IOltHostService
    {
        private IOltEnvironment _environment;

        protected OltHostServiceBase(
            IOltLogService loggingService)
        {
            LoggingService = loggingService;
        }

        public abstract string ResolveRelativePath(string filePath);
        public abstract string EnvironmentName { get; }
        public virtual IOltEnvironment Environment => _environment ?? (_environment = new OltEnvironment(EnvironmentName));
        public virtual IOltLogService LoggingService { get; }

    }
}