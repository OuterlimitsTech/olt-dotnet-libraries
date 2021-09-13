namespace OLT.Libraries.UnitTest.Assets.Models
{

    public class ProfileModel
    {
        public int? ProfileId { get; set; }
        public NameAutoMapperModel Name { get; set; } = new NameAutoMapperModel();
    }
}