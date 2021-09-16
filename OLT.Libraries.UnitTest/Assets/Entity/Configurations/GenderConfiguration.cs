using System.Collections.Generic;
using OLT.Libraries.UnitTest.Assets.Entity.Models.GeneralCode;

namespace OLT.Libraries.UnitTest.Assets.Entity.Configurations
{
    public class GenderConfiguration : BaseCodeTableEntityConfiguration<Gender>
    {

        protected override List<Gender> SeedData => new List<Gender>
        {
            new Gender
            {
                Id = 3611,
                Name = "Female",
            },
            new Gender
            {
                Id = 3613,
                Name = "Male",
            },
            new Gender
            {
                Id = 3615,
                Name = "Transgender",
            },
        };
    }
}