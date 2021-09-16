using System.Collections.Generic;
using OLT.Libraries.UnitTest.Assets.Entity.Models.GeneralCode;

namespace OLT.Libraries.UnitTest.Assets.Entity.Configurations
{
    public class SexConfiguration : BaseCodeTableEntityConfiguration<Sex>
    {
        //https://nief.org/attribute-registry/codesets/NCICSexCode/
        protected override List<Sex> SeedData => new List<Sex>
        {
            new Sex
            {
                Id = 3803,
                Code = "F",
                Name = "Female",
            },
            new Sex
            {
                Id = 3805,
                Code = "M",
                Name = "Male",
            },
            new Sex
            {
                Id = 3807,
                Code = "U",
                Name = "Unknown",
            },
        };


    }
}