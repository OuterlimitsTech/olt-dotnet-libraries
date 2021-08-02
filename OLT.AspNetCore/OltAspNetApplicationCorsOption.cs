namespace OLT.Core
{
    public class OltAspNetApplicationCorsOption
    {

        public OltAspNetApplicationCorsOption(IOltAspNetCoreCorsPolicy policy)
        {
            Policy = policy;
        }

        /// <summary>
        /// Default of true and applies default OltAspNetCoreCorsPolicyDisabled Policy
        /// </summary>
        public bool UseCors { get; set; } = true;

        /// <summary>
        /// The Cross-Origin Resource Sharing (CORS) policy to apply
        /// </summary>
        /// <value>OltAspNetCoreCorsPolicyDisabled</value>
        public IOltAspNetCoreCorsPolicy Policy { get; } 
    }
}