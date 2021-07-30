namespace OLT.Libraries.UnitTest.Models
{
    public class OltPersonTestModel 
    {
        public int? PersonId { get; set; }
        public OltNameTestModel Name { get; set; } = new OltNameTestModel();
    }
}