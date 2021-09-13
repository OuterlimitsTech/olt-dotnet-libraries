using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity.Models;

namespace OLT.Libraries.UnitTest.Assets.Entity.Configurations
{
    public class PersonTypeCsvModel : IOltCsvSeedModel<PersonTypeCodeEntity>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public void Map(PersonTypeCodeEntity entity)
        {
            entity.Id = Id;
            entity.Code = Code;
            entity.Name = Description;
        }
    }
}