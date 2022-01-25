namespace OLT.Core
{
    public interface IOltEntityCodeValue
    {
        
        /// <summary>
        /// Code value code.  This is a text alternate key for the code entry.
        /// </summary>
        string Code { get; set; }


        /// <summary>
        /// Code value caption.  This is a short description usually used for picklist values.
        /// </summary>
        string Name { get; set; }

    }
}