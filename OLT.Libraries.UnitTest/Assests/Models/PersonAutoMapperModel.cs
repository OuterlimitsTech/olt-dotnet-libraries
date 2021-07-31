namespace OLT.Libraries.UnitTest.Assests.Models
{
    // ReSharper disable once InconsistentNaming
    public class PersonAutoMapperModel 
    {
        public int? PersonId { get; set; }
        public NameAutoMapperModel Name { get; set; } = new NameAutoMapperModel();
    }
}