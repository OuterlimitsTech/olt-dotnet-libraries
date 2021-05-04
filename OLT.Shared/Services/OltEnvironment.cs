using System;

namespace OLT.Core
{
    public class OltEnvironment : OltDisposable, IOltEnvironment
    {
        private readonly string _environmentName;

        public OltEnvironment(string environmentName)
        {
            _environmentName = environmentName;
        }

        public virtual bool IsDevelopment => string.Equals(_environmentName, OltDefaults.OltEnvironments.Development, StringComparison.OrdinalIgnoreCase);
        public virtual bool IsTest => string.Equals(_environmentName, OltDefaults.OltEnvironments.Test, StringComparison.OrdinalIgnoreCase);
        public virtual bool IsStaging => string.Equals(_environmentName, OltDefaults.OltEnvironments.Staging, StringComparison.OrdinalIgnoreCase);
        public virtual bool IsProduction => string.Equals(_environmentName, OltDefaults.OltEnvironments.Production, StringComparison.OrdinalIgnoreCase);
    }
}