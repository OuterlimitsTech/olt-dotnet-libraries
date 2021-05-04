namespace OLT.Email
{
    public class OltEmailAddress : IOltEmailAddress
    {
        public OltEmailAddress()
        {
        }

        public OltEmailAddress(string email)
        {
            Email = email;
        }

        public OltEmailAddress(string email, string name) : this(email)
        {
            Name = name;
        }

        public virtual string Email { get; set; }
        public virtual string Name { get; set; }
    }
}