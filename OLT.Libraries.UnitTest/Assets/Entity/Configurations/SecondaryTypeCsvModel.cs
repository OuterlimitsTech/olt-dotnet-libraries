using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity.Models;

namespace OLT.Libraries.UnitTest.Assets.Entity.Configurations
{
    public class SecondaryTypeCsvModel : IOltCsvSeedModel<SecondaryTypeCodeEntity>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public void Map(SecondaryTypeCodeEntity entity)
        {
            entity.Id = Id;
            entity.Code = Code;
            entity.Name = Description;
        }
    }
}