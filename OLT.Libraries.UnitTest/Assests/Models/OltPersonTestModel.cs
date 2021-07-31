namespace OLT.Libraries.UnitTest.Assests.Models
{
    public class OltPersonTestModel 
    {
        public int? PersonId { get; set; }
        public OltNameTestModel Name { get; set; } = new OltNameTestModel();
    }
}