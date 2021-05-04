namespace OLT.Core
{
    public interface IOltEntityCodeValueDescription : IOltEntityCodeValue
    {

        /// <summary>
        /// Code value description.  This is a long description usually used for help text for the code value.
        /// </summary>
        string Description { get; set; }

    }
}