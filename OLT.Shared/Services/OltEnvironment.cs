using OLT.Constants;
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

        public virtual bool IsDevelopment => string.Equals(_environmentName, OltEnvironments.Development, StringComparison.OrdinalIgnoreCase);
        public virtual bool IsTest => string.Equals(_environmentName, OltEnvironments.Test, StringComparison.OrdinalIgnoreCase);
        public virtual bool IsStaging => string.Equals(_environmentName, OltEnvironments.Staging, StringComparison.OrdinalIgnoreCase);
        public virtual bool IsProduction => string.Equals(_environmentName, OltEnvironments.Production, StringComparison.OrdinalIgnoreCase);
    }
}