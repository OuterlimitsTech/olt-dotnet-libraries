namespace OLT.Core
{
    public interface IOltEntityCodeValueAbbrev : IOltEntityCodeValue
    {

        /// <summary>
        /// Code value abbreviation.  This is the shortest possible human-readable text for the value.
        /// </summary>
        string Abbreviation { get; set; }
    }
}