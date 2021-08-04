namespace OLT.Core
{
    public class OltAspNetApplicationCorsOption
    {

        public OltAspNetApplicationCorsOption(IOltAspNetCoreCorsPolicy policy)
        {
            Policy = policy;
        }


        /// <summary>
        /// The Cross-Origin Resource Sharing (CORS) policy to apply
        /// </summary>
        /// <value>OltAspNetCoreCorsPolicyDisabled</value>
        public IOltAspNetCoreCorsPolicy Policy { get; } 
    }
}