namespace OLT.Email
{
    public interface IOltEmailAddress
    {
        string Email { get; }

        /// <summary>
        /// Name is optional
        /// </summary>
        string Name { get; }
    }
}