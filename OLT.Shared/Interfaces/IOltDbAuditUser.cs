namespace OLT.Core
{
    public interface IOltDbAuditUser : IOltInjectableScoped
    {
        /// <summary>
        /// Gets value to put in the DB Create/Update Audit Field
        /// </summary>
        string GetDbUsername();
    }
}