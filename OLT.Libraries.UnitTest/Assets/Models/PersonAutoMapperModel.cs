using System;

namespace OLT.Libraries.UnitTest.Assets.Models
{
    // ReSharper disable once InconsistentNaming
    public class PersonAutoMapperModel 
    {
        public int? PersonId { get; set; }
        public NameAutoMapperModel Name { get; set; } = new NameAutoMapperModel();
    }

    public class PersonAddressModel : PersonAutoMapperModel
    {
        public DateTime Created { get; set; }
        public string Street1 { get; set; }
    }


    public class PersonAddressInvalidPagedModel : PersonAddressModel
    {
    }
}