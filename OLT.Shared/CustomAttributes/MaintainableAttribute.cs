using System;

namespace OLT.Core
{
    public class MaintainableAttribute : Attribute
    {

        public OltEntityMaintainableEnum Create { get; set; } = OltEntityMaintainableEnum.NotSet;
        public OltEntityMaintainableEnum Update { get; set; } = OltEntityMaintainableEnum.NotSet;
        public OltEntityMaintainableEnum Delete { get; set; } = OltEntityMaintainableEnum.NotSet;

        public void SetEntityValues(IOltEntityMaintainable entity)
        {
            entity.MaintAdd = ToBool(Create);
            entity.MaintUpdate = ToBool(Update);
            entity.MaintDelete = ToBool(Delete);
        }

        public bool? ToBool(OltEntityMaintainableEnum value)
        {
            return value == OltEntityMaintainableEnum.NotSet ? new bool?() : value == OltEntityMaintainableEnum.Yes;
        }
    }
}