namespace OLT.Email
{
    public class OltEmailAddressResult : OltEmailAddress
    {
        public OltEmailAddressResult(IOltEmailAddress copyFrom, IOltEmailConfiguration configuration)
        {
            Name = copyFrom.Name;
            Email = copyFrom.Email;

            if (!configuration.IsProduction && !configuration.SendEmail(Email))
            {
                Skipped = true;
                SkipReason = "Email not in configuration to send";
            }
        }

        public bool Sent => !Skipped && string.IsNullOrWhiteSpace(Error);

        public bool Skipped { get; set; }
        public string SkipReason { get; set; }
        public string Error { get; set; }
    }
}