namespace OLT.Core
{

    public class OltFileBuilderNotFoundException : OltException
    {
        public OltFileBuilderNotFoundException(string builderName) : base($"FileBuilder {builderName} not found")
        {

        }
    }
}
